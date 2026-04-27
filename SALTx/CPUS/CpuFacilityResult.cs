using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SALTx.CPUS
{
    public sealed class CpuFacilityClassResult
    {
        internal CpuFacilityClassResult(
            int classLevel,
            int completed,
            double averageNumberInQueue,
            double averageDelayInQueue,
            double cpuBusyShare)
        {
            ClassLevel = classLevel;
            Completed = completed;
            AverageNumberInQueue = averageNumberInQueue;
            AverageDelayInQueue = averageDelayInQueue;
            CpuBusyShare = cpuBusyShare;
        }

        public double AverageDelayInQueue { get; }

        public double AverageNumberInQueue { get; }

        public int ClassLevel { get; }

        public int Completed { get; }

        public double CpuBusyShare { get; }
    }

    public sealed class CpuFacilityResult
    {
        internal CpuFacilityResult(
            SimulationMode mode,
            CpuFacilityConfiguration configuration,
            int seed,
            double endTime,
            int admitted,
            int completed,
            double cpuUtilization,
            IList<CpuFacilityClassResult> classResults)
        {
            Mode = mode;
            Configuration = configuration;
            Seed = seed;
            EndTime = endTime;
            Admitted = admitted;
            Completed = completed;
            CpuUtilization = cpuUtilization;
            ClassResults = new ReadOnlyCollection<CpuFacilityClassResult>(classResults);
        }

        public int Admitted { get; }

        public ReadOnlyCollection<CpuFacilityClassResult> ClassResults { get; }

        public int Completed { get; }

        public CpuFacilityConfiguration Configuration { get; }

        public double CpuUtilization { get; }

        public double EndTime { get; }

        public SimulationMode Mode { get; }

        public string ModeName
        {
            get
            {
                return Mode == SimulationMode.NonPreemptive
                    ? "Non-Preemptive"
                    : "Preemptive-Resume";
            }
        }

        public int Seed { get; }
    }
}
