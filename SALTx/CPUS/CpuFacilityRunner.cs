using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SALTx.CPUS
{
    public static class CpuFacilityRunner
    {
        public const int DefaultSeed = 4182026;

        private const double ClosingTime = 1020.0;
        private const double MeanInterarrivalTime = 1.91;

        private static readonly double[] ServiceMeansByClass = new double[]
        {
            0.0,
            3.00,
            1.50,
            1.00,
            0.25
        };

        public static CpuFacilityResult Run(SimulationMode mode, int seed)
        {
            RngStreams streams = new RngStreams(seed);
            Scheduler scheduler = new Scheduler();
            Server server = new Server();
            Queue[] queues = CreateQueues();

            int[] admittedByClass = new int[5];
            int[] completedByClass = new int[5];
            double[] queueDelayByClass = new double[5];

            int nextJobId = 0;
            double time = 0.0;
            bool doorOpen = true;

            ScheduleNextArrival(0.0, streams, scheduler);
            scheduler.Schedule(ClosingTime, FacilityEventType.CloseDoor);

            ScheduledEvent scheduledEvent;
            while (scheduler.TryPopNext(out scheduledEvent))
            {
                time = scheduledEvent.Time;

                if (scheduledEvent.Type == FacilityEventType.Arrival)
                {
                    if (doorOpen && time < ClosingTime)
                    {
                        Job job = CreateJob(++nextJobId, time, streams);
                        admittedByClass[job.ClassLevel]++;
                        Admit(job, mode, time, server, queues, scheduler);
                        ScheduleNextArrival(time, streams, scheduler);
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

            return BuildResult(mode, seed, time, queues, server, admittedByClass, completedByClass, queueDelayByClass);
        }

        public static CpuFacilityResult[] RunBoth(int seed)
        {
            return new CpuFacilityResult[]
            {
                Run(SimulationMode.NonPreemptive, seed),
                Run(SimulationMode.PreemptiveResume, seed)
            };
        }

        public static string FormatReport(params CpuFacilityResult[] results)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("CPU Facility Simulation");
            builder.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "Admission window: {0:0.##} min | Interarrival mean: {1:0.##} min | Seed: {2}",
                ClosingTime,
                MeanInterarrivalTime,
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

        private static void ScheduleNextArrival(double time, RngStreams streams, Scheduler scheduler)
        {
            double nextArrivalTime = time + streams.NextInterarrival();
            if (nextArrivalTime < ClosingTime)
                scheduler.Schedule(nextArrivalTime, FacilityEventType.Arrival);
        }

        private sealed class RngStreams
        {
            private readonly Random classStream;
            private readonly Random interarrivalStream;
            private readonly Random[] serviceStreams = new Random[5];

            internal RngStreams(int seed)
            {
                interarrivalStream = new Random(MixSeed(seed, 1));
                classStream = new Random(MixSeed(seed, 2));

                for (int classLevel = 1; classLevel <= 4; classLevel++)
                    serviceStreams[classLevel] = new Random(MixSeed(seed, 10 + classLevel));
            }

            internal int NextClassLevel()
            {
                double u = classStream.NextDouble();

                if (u < 0.05)
                    return 4;
                if (u < 0.55)
                    return 3;
                if (u < 0.85)
                    return 2;
                return 1;
            }

            internal double NextInterarrival()
            {
                return Exponential(interarrivalStream, MeanInterarrivalTime);
            }

            internal double NextServiceTime(int classLevel)
            {
                double stageMean = ServiceMeansByClass[classLevel] / 3.0;
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
