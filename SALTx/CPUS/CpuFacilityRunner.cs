using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SALTx.CPUS
{
    public static class CpuFacilityRunner
    {
        public const int DefaultSeed = 4182026;

        private static readonly CpuFacilityConfiguration DefaultConfiguration = CpuFacilityConfiguration.Baseline();

        public static CpuFacilityResult Run(SimulationMode mode, int seed)
        {
            return Run(mode, seed, DefaultConfiguration);
        }

        public static CpuFacilityResult Run(SimulationMode mode, int seed, CpuFacilityConfiguration configuration)
        {
            RngStreams streams = new RngStreams(seed, configuration);
            Scheduler scheduler = new Scheduler();
            Server server = new Server();
            Queue[] queues = CreateQueues();

            int[] admittedByClass = new int[5];
            int[] completedByClass = new int[5];
            double[] queueDelayByClass = new double[5];

            int nextJobId = 0;
            double time = 0.0;
            bool doorOpen = true;

            ScheduleNextArrival(0.0, streams, scheduler, configuration);
            scheduler.Schedule(configuration.AdmissionWindow, FacilityEventType.CloseDoor);

            ScheduledEvent scheduledEvent;
            while (scheduler.TryPopNext(out scheduledEvent))
            {
                time = scheduledEvent.Time;

                if (scheduledEvent.Type == FacilityEventType.Arrival)
                {
                    if (doorOpen && time < configuration.AdmissionWindow)
                    {
                        Job job = CreateJob(++nextJobId, time, streams);
                        admittedByClass[job.ClassLevel]++;
                        Admit(job, mode, time, server, queues, scheduler);
                        ScheduleNextArrival(time, streams, scheduler, configuration);
                    }
                }
                else if (scheduledEvent.Type == FacilityEventType.CloseDoor)
                {
                    doorOpen = false;
                }
                else if (scheduledEvent.Type == FacilityEventType.EndService && server.IsCurrent(scheduledEvent.Job))
                {
                    Job completedJob = server.CompleteCurrent(time);
                    completedByClass[completedJob.ClassLevel]++;
                    queueDelayByClass[completedJob.ClassLevel] += completedJob.TotalQueueDelay;
                    DispatchNext(time, server, queues, scheduler);
                }
            }

            for (int classLevel = 1; classLevel <= 4; classLevel++)
                queues[classLevel].FinalizeArea(time);

            return BuildResult(mode, configuration, seed, time, queues, server, admittedByClass, completedByClass, queueDelayByClass);
        }

        public static CpuFacilityResult[] RunBoth(int seed)
        {
            return RunBoth(seed, DefaultConfiguration);
        }

        public static CpuFacilityResult[] RunBoth(int seed, CpuFacilityConfiguration configuration)
        {
            return new CpuFacilityResult[]
            {
                Run(SimulationMode.NonPreemptive, seed, configuration),
                Run(SimulationMode.PreemptiveResume, seed, configuration)
            };
        }

        public static string FormatReport(params CpuFacilityResult[] results)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("CPU Facility Simulation");
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "Configuration: {0} | Admission window: {1:0.##} min | Interarrival mean: {2:0.##} min | Seed: {3}",
                results.Length > 0 ? results[0].Configuration.Name : DefaultConfiguration.Name,
                results.Length > 0 ? results[0].Configuration.AdmissionWindow : DefaultConfiguration.AdmissionWindow,
                results.Length > 0 ? results[0].Configuration.MeanInterarrivalTime : DefaultConfiguration.MeanInterarrivalTime,
                results.Length > 0 ? results[0].Seed : DefaultSeed));
            builder.AppendLine();
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "{0,-20} {1,5} {2,10} {3,20} {4,25} {5,16} {6,16}",
                "Case",
                "Class",
                "Completed",
                "Avg Number in Queue",
                "Avg Delay in Queue (min)",
                "CPU Busy Share",
                "CPU Utilization"));
            builder.AppendLine(new string('-', 124));

            foreach (CpuFacilityResult result in results)
            {
                foreach (CpuFacilityClassResult classResult in result.ClassResults)
                {
                    builder.AppendLine(string.Format(
                        CultureInfo.InvariantCulture,
                        "{0,-20} {1,5} {2,10} {3,20:0.0000} {4,25:0.0000} {5,16:0.0000} {6,16:0.0000}",
                        result.ModeName,
                        classResult.ClassLevel,
                        classResult.Completed,
                        classResult.AverageNumberInQueue,
                        classResult.AverageDelayInQueue,
                        classResult.CpuBusyShare,
                        result.CpuUtilization));
                }

                builder.AppendLine(string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}: admitted {1}, completed {2}, ended at {3:0.0000} min",
                    result.ModeName,
                    result.Admitted,
                    result.Completed,
                    result.EndTime));
                builder.AppendLine();
            }

            return builder.ToString();
        }

        public static string FormatSectionFiveReport(int seed)
        {
            return FormatSectionFiveReport(seed, true);
        }

        public static string FormatSectionFiveTableReport(int seed)
        {
            return FormatSectionFiveReport(seed, false);
        }

        public static CpuFacilityResult[] RunDifferentInputCases(int seed)
        {
            List<CpuFacilityResult> results = new List<CpuFacilityResult>();
            CpuFacilityConfiguration[] configurations = CreateSectionFiveConfigurations();

            foreach (CpuFacilityConfiguration configuration in configurations)
                results.AddRange(RunBoth(seed, configuration));

            return results.ToArray();
        }

        private static string FormatSectionFiveReport(int seed, bool includeDiscussionAndOptimization)
        {
            CpuFacilityResult[] results = RunDifferentInputCases(seed);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(includeDiscussionAndOptimization
                ? "5) Simulation Output and Discussion"
                : "Simulation Output Tables");
            builder.AppendLine();
            builder.AppendLine(includeDiscussionAndOptimization
                ? "i) Simulation Output Table"
                : "Simulation Output Table");
            builder.AppendLine("Each row is one simulation run. Input parameters are summarized by arrival rate, class mix, and service means.");
            builder.AppendLine();
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "{0,-18} {1,-18} {2,8} {3,-17} {4,-19} {5,8} {6,9} {7,8} {8,10} {9,11} {10,9} {11,9}",
                "Config",
                "Case",
                "Mean IA",
                "P(C4/C3/C2/C1)",
                "Svc Mean C4/C3/C2/C1",
                "Admit",
                "Complete",
                "Util",
                "Avg Q",
                "Avg Delay",
                "C4 Delay",
                "C1 Delay"));
            builder.AppendLine(new string('-', 160));

            foreach (CpuFacilityResult result in results)
            {
                AggregateMetrics metrics = Aggregate(result);
                builder.AppendLine(string.Format(
                    CultureInfo.InvariantCulture,
                    "{0,-18} {1,-18} {2,8:0.00} {3,-17} {4,-19} {5,8} {6,9} {7,8:0.000} {8,10:0.000} {9,11:0.000} {10,9:0.000} {11,9:0.000}",
                    result.Configuration.Name,
                    result.ModeName,
                    result.Configuration.MeanInterarrivalTime,
                    FormatClassProbabilities(result.Configuration),
                    FormatServiceMeans(result.Configuration),
                    result.Admitted,
                    result.Completed,
                    result.CpuUtilization,
                    metrics.AverageQueueLength,
                    metrics.AverageDelay,
                    metrics.Class4Delay,
                    metrics.Class1Delay));
            }

            AppendBaselinePolicyComparisonTable(builder, results);

            if (includeDiscussionAndOptimization)
            {
                AppendDiscussion(builder, results);
                AppendOptimization(builder, results);
            }

            return builder.ToString();
        }

        private static void AppendBaselinePolicyComparisonTable(
            StringBuilder builder,
            IList<CpuFacilityResult> results)
        {
            CpuFacilityResult nonPreemptive = FindResult(
                results,
                "Baseline",
                SimulationMode.NonPreemptive);
            CpuFacilityResult preemptive = FindResult(
                results,
                "Baseline",
                SimulationMode.PreemptiveResume);

            builder.AppendLine();
            builder.AppendLine("Preemptive and Non-Preemptive Detailed Table");
            builder.AppendLine("This table compares the two CPU scheduling rules for the baseline input parameters, broken down by priority class.");
            builder.AppendLine();
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "{0,-20} {1,5} {2,10} {3,20} {4,25} {5,16} {6,16}",
                "Case",
                "Class",
                "Completed",
                "Avg Number in Queue",
                "Avg Delay in Queue (min)",
                "CPU Busy Share",
                "CPU Utilization"));
            builder.AppendLine(new string('-', 124));

            AppendPolicyRows(builder, nonPreemptive);
            AppendPolicyRows(builder, preemptive);
        }

        private static void AppendPolicyRows(StringBuilder builder, CpuFacilityResult result)
        {
            if (result == null)
                return;

            foreach (CpuFacilityClassResult classResult in result.ClassResults)
            {
                builder.AppendLine(string.Format(
                    CultureInfo.InvariantCulture,
                    "{0,-20} {1,5} {2,10} {3,20:0.0000} {4,25:0.0000} {5,16:0.0000} {6,16:0.0000}",
                    result.ModeName,
                    classResult.ClassLevel,
                    classResult.Completed,
                    classResult.AverageNumberInQueue,
                    classResult.AverageDelayInQueue,
                    classResult.CpuBusyShare,
                    result.CpuUtilization));
            }
        }

        private static void AppendDiscussion(StringBuilder builder, IList<CpuFacilityResult> results)
        {
            CpuFacilityResult worstDelay = FindWorstDelay(results);
            CpuFacilityResult bestDelay = FindBestDelay(results);
            int class4ImprovementCount = CountClass4PreemptiveImprovements(results);

            builder.AppendLine();
            builder.AppendLine("ii) Discussion");
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "The highest overall average queue delay occurred in the {0} configuration under {1}. The lowest overall average queue delay occurred in the {2} configuration under {3}.",
                worstDelay.Configuration.Name,
                worstDelay.ModeName,
                bestDelay.Configuration.Name,
                bestDelay.ModeName));
            builder.AppendLine("When the mean interarrival time is reduced, jobs enter the CPU facility more often, utilization rises, and waiting grows because more jobs compete for the same single processor. When demand is reduced or service times are shortened, the queue clears more quickly and average delays fall.");
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "Preemptive-resume improved class 4 delay in {0} of the tested configurations. That matches the real-world priority CPU facility: urgent/high-priority jobs benefit from interruption, while lower-priority work can wait longer.",
                class4ImprovementCount));
            builder.AppendLine("CPU utilization is mostly driven by total workload, not by the scheduling rule. For the same input configuration, non-preemptive and preemptive-resume process the same admitted jobs after the system drains, but they distribute waiting time differently across priority classes.");
        }

        private static void AppendOptimization(StringBuilder builder, IList<CpuFacilityResult> results)
        {
            CpuFacilityResult bestClass4 = FindBestClass4Delay(results);

            builder.AppendLine();
            builder.AppendLine("iii) Optimization");
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "If the goal is to protect high-priority jobs, the best tested choice is {0} under {1}, because it gives the lowest class 4 delay in the study.",
                bestClass4.Configuration.Name,
                bestClass4.ModeName));
            builder.AppendLine("The most important input parameters are mean interarrival time and service-time means. Lower interarrival times increase load sharply, and longer service means increase the CPU time required per job. Both push utilization upward and make queues grow.");
            builder.AppendLine("Practical improvements include using preemptive-resume scheduling for high-priority service targets, reducing service time through faster processing or job preparation, adding capacity when utilization is consistently high, and smoothing admissions so large bursts of low-priority batch jobs do not block urgent work.");
            builder.AppendLine("If fairness for class 1 jobs is also important, use preemptive-resume with monitoring or limits, because priority protection can shift delay from class 4 to lower-priority classes.");
        }

        private static AggregateMetrics Aggregate(CpuFacilityResult result)
        {
            double weightedDelay = 0.0;
            double averageQueueLength = 0.0;
            double class1Delay = 0.0;
            double class4Delay = 0.0;

            foreach (CpuFacilityClassResult classResult in result.ClassResults)
            {
                weightedDelay += classResult.AverageDelayInQueue * classResult.Completed;
                averageQueueLength += classResult.AverageNumberInQueue;

                if (classResult.ClassLevel == 1)
                    class1Delay = classResult.AverageDelayInQueue;
                else if (classResult.ClassLevel == 4)
                    class4Delay = classResult.AverageDelayInQueue;
            }

            return new AggregateMetrics(
                averageQueueLength,
                result.Completed > 0 ? weightedDelay / result.Completed : 0.0,
                class4Delay,
                class1Delay);
        }

        private static int CountClass4PreemptiveImprovements(IList<CpuFacilityResult> results)
        {
            int count = 0;

            for (int i = 0; i < results.Count; i++)
            {
                CpuFacilityResult nonPreemptive = results[i];
                if (nonPreemptive.Mode != SimulationMode.NonPreemptive)
                    continue;

                CpuFacilityResult preemptive = FindResult(
                    results,
                    nonPreemptive.Configuration.Name,
                    SimulationMode.PreemptiveResume);

                if (preemptive != null && Aggregate(preemptive).Class4Delay < Aggregate(nonPreemptive).Class4Delay)
                    count++;
            }

            return count;
        }

        private static CpuFacilityConfiguration[] CreateSectionFiveConfigurations()
        {
            return new CpuFacilityConfiguration[]
            {
                CpuFacilityConfiguration.Baseline(),
                new CpuFacilityConfiguration("High Demand", 1020.0, 1.50, 0.15, 0.30, 0.50, 0.05, 3.00, 1.50, 1.00, 0.25),
                new CpuFacilityConfiguration("Low Demand", 1020.0, 2.30, 0.15, 0.30, 0.50, 0.05, 3.00, 1.50, 1.00, 0.25),
                new CpuFacilityConfiguration("Faster CPU", 1020.0, 1.91, 0.15, 0.30, 0.50, 0.05, 2.40, 1.20, 0.80, 0.20),
                new CpuFacilityConfiguration("More Class 4", 1020.0, 1.91, 0.15, 0.25, 0.45, 0.15, 3.00, 1.50, 1.00, 0.25)
            };
        }

        private static CpuFacilityResult FindBestClass4Delay(IList<CpuFacilityResult> results)
        {
            CpuFacilityResult best = results[0];

            foreach (CpuFacilityResult result in results)
            {
                if (Aggregate(result).Class4Delay < Aggregate(best).Class4Delay)
                    best = result;
            }

            return best;
        }

        private static CpuFacilityResult FindBestDelay(IList<CpuFacilityResult> results)
        {
            CpuFacilityResult best = results[0];

            foreach (CpuFacilityResult result in results)
            {
                if (Aggregate(result).AverageDelay < Aggregate(best).AverageDelay)
                    best = result;
            }

            return best;
        }

        private static CpuFacilityResult FindWorstDelay(IList<CpuFacilityResult> results)
        {
            CpuFacilityResult worst = results[0];

            foreach (CpuFacilityResult result in results)
            {
                if (Aggregate(result).AverageDelay > Aggregate(worst).AverageDelay)
                    worst = result;
            }

            return worst;
        }

        private static CpuFacilityResult FindResult(
            IList<CpuFacilityResult> results,
            string configurationName,
            SimulationMode mode)
        {
            foreach (CpuFacilityResult result in results)
            {
                if (result.Configuration.Name == configurationName && result.Mode == mode)
                    return result;
            }

            return null;
        }

        private static string FormatClassProbabilities(CpuFacilityConfiguration configuration)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.00}/{1:0.00}/{2:0.00}/{3:0.00}",
                configuration.Class4Probability,
                configuration.Class3Probability,
                configuration.Class2Probability,
                configuration.Class1Probability);
        }

        private static string FormatServiceMeans(CpuFacilityConfiguration configuration)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.00}/{1:0.00}/{2:0.00}/{3:0.00}",
                configuration.GetServiceMean(4),
                configuration.GetServiceMean(3),
                configuration.GetServiceMean(2),
                configuration.GetServiceMean(1));
        }

        private static void Admit(Job job, SimulationMode mode, double time, Server server, Queue[] queues, Scheduler scheduler)
        {
            if (!server.IsBusy)
            {
                server.Start(job, time, scheduler);
                return;
            }

            if (mode == SimulationMode.PreemptiveResume && job.ClassLevel > server.CurrentJob.ClassLevel)
            {
                Job preemptedJob = server.PreemptCurrent(time, scheduler);
                queues[preemptedJob.ClassLevel].EnqueueHead(preemptedJob, time);
                server.Start(job, time, scheduler);
                return;
            }

            queues[job.ClassLevel].EnqueueTail(job, time);
        }

        private static CpuFacilityResult BuildResult(
            SimulationMode mode,
            CpuFacilityConfiguration configuration,
            int seed,
            double endTime,
            Queue[] queues,
            Server server,
            int[] admittedByClass,
            int[] completedByClass,
            double[] queueDelayByClass)
        {
            List<CpuFacilityClassResult> classResults = new List<CpuFacilityClassResult>();
            double totalBusyTime = server.BusyTime;
            int admitted = 0;
            int completed = 0;

            for (int classLevel = 1; classLevel <= 4; classLevel++)
            {
                admitted += admittedByClass[classLevel];
                completed += completedByClass[classLevel];
            }

            for (int classLevel = 4; classLevel >= 1; classLevel--)
            {
                int classCompleted = completedByClass[classLevel];
                double averageDelay = classCompleted > 0
                    ? queueDelayByClass[classLevel] / classCompleted
                    : 0.0;
                double busyShare = totalBusyTime > 0.0
                    ? server.GetBusyTime(classLevel) / totalBusyTime
                    : 0.0;

                classResults.Add(new CpuFacilityClassResult(
                    classLevel,
                    classCompleted,
                    queues[classLevel].TimeAverageCount(endTime),
                    averageDelay,
                    busyShare));
            }

            return new CpuFacilityResult(
                mode,
                configuration,
                seed,
                endTime,
                admitted,
                completed,
                endTime > 0.0 ? server.BusyTime / endTime : 0.0,
                classResults);
        }

        private static Queue[] CreateQueues()
        {
            Queue[] queues = new Queue[5];
            for (int classLevel = 1; classLevel <= 4; classLevel++)
                queues[classLevel] = new Queue(classLevel);
            return queues;
        }

        private static Job CreateJob(int id, double time, RngStreams streams)
        {
            int classLevel = streams.NextClassLevel();
            double serviceTime = streams.NextServiceTime(classLevel);
            return new Job(id, classLevel, time, serviceTime);
        }

        private static void DispatchNext(double time, Server server, Queue[] queues, Scheduler scheduler)
        {
            for (int classLevel = 4; classLevel >= 1; classLevel--)
            {
                if (queues[classLevel].Count > 0)
                {
                    server.Start(queues[classLevel].Dequeue(time), time, scheduler);
                    return;
                }
            }
        }

        private static void ScheduleNextArrival(
            double time,
            RngStreams streams,
            Scheduler scheduler,
            CpuFacilityConfiguration configuration)
        {
            double nextArrivalTime = time + streams.NextInterarrival();
            if (nextArrivalTime < configuration.AdmissionWindow)
                scheduler.Schedule(nextArrivalTime, FacilityEventType.Arrival);
        }

        private sealed class AggregateMetrics
        {
            internal AggregateMetrics(
                double averageQueueLength,
                double averageDelay,
                double class4Delay,
                double class1Delay)
            {
                AverageQueueLength = averageQueueLength;
                AverageDelay = averageDelay;
                Class4Delay = class4Delay;
                Class1Delay = class1Delay;
            }

            internal double AverageDelay { get; }

            internal double AverageQueueLength { get; }

            internal double Class1Delay { get; }

            internal double Class4Delay { get; }
        }

        private sealed class RngStreams
        {
            private readonly Random classStream;
            private readonly CpuFacilityConfiguration configuration;
            private readonly Random interarrivalStream;
            private readonly Random[] serviceStreams = new Random[5];

            internal RngStreams(int seed, CpuFacilityConfiguration configuration)
            {
                this.configuration = configuration;
                interarrivalStream = new Random(MixSeed(seed, 1));
                classStream = new Random(MixSeed(seed, 2));

                for (int classLevel = 1; classLevel <= 4; classLevel++)
                    serviceStreams[classLevel] = new Random(MixSeed(seed, 10 + classLevel));
            }

            internal int NextClassLevel()
            {
                double u = classStream.NextDouble();
                double cumulative = 0.0;

                for (int classLevel = 4; classLevel >= 1; classLevel--)
                {
                    cumulative += configuration.GetClassProbability(classLevel);
                    if (u < cumulative)
                        return classLevel;
                }

                return 1;
            }

            internal double NextInterarrival()
            {
                return Exponential(interarrivalStream, configuration.MeanInterarrivalTime);
            }

            internal double NextServiceTime(int classLevel)
            {
                double stageMean = configuration.GetServiceMean(classLevel) / 3.0;
                return Exponential(serviceStreams[classLevel], stageMean)
                    + Exponential(serviceStreams[classLevel], stageMean)
                    + Exponential(serviceStreams[classLevel], stageMean);
            }

            private static double Exponential(Random random, double mean)
            {
                return -mean * Math.Log(1.0 - random.NextDouble());
            }

            private static int MixSeed(int seed, int stream)
            {
                unchecked
                {
                    long value = seed;
                    value ^= stream * 0x9E3779B9L;
                    value = (value ^ (value >> 16)) * 0x45D9F3BL;
                    value = (value ^ (value >> 16)) * 0x45D9F3BL;
                    value = value ^ (value >> 16);

                    int mixed = (int)(value & 0x7fffffff);
                    if (mixed == 0)
                        return stream + 1;
                    return mixed;
                }
            }
        }
    }
}
