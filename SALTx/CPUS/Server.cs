using SALT;

namespace SALTx.CPUS
{
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

        public override Event[] Execute()
        {
            return new Event[0];
        }

        public override Stat[] FinalizeStats()
        {
            return new Stat[0];
        }

        public override Event[] Init()
        {
            return new Event[0];
        }

        internal double GetBusyTime(int classLevel)
        {
            return busyTimeByClass[classLevel];
        }

        internal bool IsCurrent(Job job)
        {
            return CurrentJob == job;
        }

        internal void Start(Job job, double time, Scheduler scheduler)
        {
            CurrentJob = job;
            currentServiceStartedAt = time;
            job.ServiceStartTime = time;
            pendingCompletion = scheduler.Schedule(time + job.RemainingServiceTime, FacilityEventType.EndService, job);
        }

        internal Job CompleteCurrent(double time)
        {
            AccumulateBusyTime(time);

            Job completedJob = CurrentJob;
            completedJob.RemainingServiceTime = 0.0;
            CurrentJob = null;
            pendingCompletion = null;
            return completedJob;
        }

        internal Job PreemptCurrent(double time, Scheduler scheduler)
        {
            scheduler.Cancel(pendingCompletion);
            AccumulateBusyTime(time);

            Job preemptedJob = CurrentJob;
            CurrentJob = null;
            pendingCompletion = null;
            return preemptedJob;
        }

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
