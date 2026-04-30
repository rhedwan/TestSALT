using SALT;

namespace SALTx.CPUS
{
    // Represents the single CPU server and records busy-time statistics by job class.
    public class Server : SALT.Node
    {
        private readonly double[] busyTimeByClass = new double[5];
        private double currentServiceStartedAt;
        private ScheduledEvent pendingCompletion;

        public Server()
            : this(0, "CPU Facility Server", new SALT.Statx.Ct[0], new SALT.Statx.Dt[0])
        {
        }

        public Server(int id, string name, SALT.Statx.Ct[] ctStats, SALT.Statx.Dt[] dtStats)
            : base(id, name, ctStats, dtStats)
        {
        }

        internal double BusyTime { get; private set; }

        internal Job CurrentJob { get; private set; }

        internal bool IsBusy
        {
            get { return CurrentJob != null; }
        }

        // SALT node execution is not used because this runner manages events externally.
        public override Event[] Execute()
        {
            return new Event[0];
        }

        // Returns no SALT statistics because this class records its own simulation totals.
        public override Stat[] FinalizeStats()
        {
            return new Stat[0];
        }

        // Returns no initial SALT events because the custom scheduler initializes the run.
        public override Event[] Init()
        {
            return new Event[0];
        }

        // Returns the total CPU time spent serving jobs from the specified class.
        internal double GetBusyTime(int classLevel)
        {
            return busyTimeByClass[classLevel];
        }

        // Confirms that an end-service event still belongs to the current in-service job.
        internal bool IsCurrent(Job job)
        {
            return CurrentJob == job;
        }

        // Starts service for a job and schedules its future completion event.
        internal void Start(Job job, double time, Scheduler scheduler)
        {
            CurrentJob = job;
            currentServiceStartedAt = time;
            job.ServiceStartTime = time;
            pendingCompletion = scheduler.Schedule(time + job.RemainingServiceTime, FacilityEventType.EndService, job);
        }

        // Completes the current job and updates CPU busy-time totals.
        internal Job CompleteCurrent(double time)
        {
            AccumulateBusyTime(time);

            Job completedJob = CurrentJob;
            completedJob.RemainingServiceTime = 0.0;
            CurrentJob = null;
            pendingCompletion = null;
            return completedJob;
        }

        // Interrupts the current job, cancels its completion event, and preserves remaining service time.
        internal Job PreemptCurrent(double time, Scheduler scheduler)
        {
            scheduler.Cancel(pendingCompletion);
            AccumulateBusyTime(time);

            Job preemptedJob = CurrentJob;
            CurrentJob = null;
            pendingCompletion = null;
            return preemptedJob;
        }

        // Adds elapsed service time to utilization and class busy-share counters.
        private void AccumulateBusyTime(double time)
        {
            double elapsed = time - currentServiceStartedAt;
            if (elapsed < 0.0)
                throw new System.InvalidOperationException("Server time cannot move backward.");

            CurrentJob.RemainingServiceTime -= elapsed;
            if (CurrentJob.RemainingServiceTime < 0.000000001)
                CurrentJob.RemainingServiceTime = 0.0;

            BusyTime += elapsed;
            busyTimeByClass[CurrentJob.ClassLevel] += elapsed;
        }
    }
}
