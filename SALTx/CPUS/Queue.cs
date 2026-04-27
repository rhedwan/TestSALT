using System.Collections.Generic;

namespace SALTx.CPUS
{
    public class Queue
    {
        private readonly LinkedList<Job> jobs = new LinkedList<Job>();
        private double areaUnderCount;
        private double lastUpdateTime;

        public Queue(int classLevel)
        {
            ClassLevel = classLevel;
        }

        public int ClassLevel { get; }

        public int Count
        {
            get { return jobs.Count; }
        }

        internal void EnqueueTail(Job job, double time)
        {
            UpdateArea(time);
            job.QueueEntryTime = time;
            jobs.AddLast(job);
        }

        internal void EnqueueHead(Job job, double time)
        {
            UpdateArea(time);
            job.QueueEntryTime = time;
            jobs.AddFirst(job);
        }

        internal Job Dequeue(double time)
        {
            UpdateArea(time);
            Job job = jobs.First.Value;
            jobs.RemoveFirst();
            job.TotalQueueDelay += time - job.QueueEntryTime;
            return job;
        }

        internal void FinalizeArea(double time)
        {
            UpdateArea(time);
        }

        public double TimeAverageCount(double horizon)
        {
            if (horizon <= 0.0)
                return 0.0;
            return areaUnderCount / horizon;
        }

        private void UpdateArea(double time)
        {
            if (time < lastUpdateTime)
                throw new System.InvalidOperationException("Queue time cannot move backward.");

            areaUnderCount += jobs.Count * (time - lastUpdateTime);
            lastUpdateTime = time;
        }
    }
}
