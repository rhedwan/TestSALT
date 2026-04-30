namespace SALTx.CPUS
{
    // Represents one admitted batch job and the timing data needed for service and queue statistics.
    internal sealed class Job
    {
        internal Job(int id, int classLevel, double arrivalTime, double serviceTime)
        {
            Id = id;
            ClassLevel = classLevel;
            ArrivalTime = arrivalTime;
            RequiredServiceTime = serviceTime;
            RemainingServiceTime = serviceTime;
            QueueEntryTime = arrivalTime;
        }

        internal double ArrivalTime { get; }

        internal int ClassLevel { get; }

        internal int Id { get; }

        internal double QueueEntryTime { get; set; }

        internal double RemainingServiceTime { get; set; }

        internal double RequiredServiceTime { get; }

        internal double ServiceStartTime { get; set; }

        internal double TotalQueueDelay { get; set; }
    }
}
