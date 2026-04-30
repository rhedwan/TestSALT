using System.Collections.Generic;

namespace SALTx.CPUS
{
    // Represents one FIFO class queue and tracks its time-average queue length.
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

        // Adds a normal waiting job to the back of its class queue.
        internal void EnqueueTail(Job job, double time)
        {
            UpdateArea(time);
            job.QueueEntryTime = time;
            jobs.AddLast(job);
        }

        // Adds a preempted job to the front of its class queue.
        internal void EnqueueHead(Job job, double time)
        {
            UpdateArea(time);
            job.QueueEntryTime = time;
            jobs.AddFirst(job);
        }

        // Removes the next waiting job and records its accumulated queue delay.
        internal Job Dequeue(double time)
        {
            UpdateArea(time);
            Job job = jobs.First.Value;
            jobs.RemoveFirst();
            job.TotalQueueDelay += time - job.QueueEntryTime;
            return job;
        }

        // Updates the area-under-queue-length statistic at the end of the run.
        internal void FinalizeArea(double time)
        {
            UpdateArea(time);
        }

        // Returns the time-average number of jobs waiting in this queue.
        public double TimeAverageCount(double horizon)
        {
            if (horizon <= 0.0)
                return 0.0;
            return areaUnderCount / horizon;
        }

        // Accumulates queue-length area over the elapsed simulated time interval.
        private void UpdateArea(double time)
        {
            if (time < lastUpdateTime)
                throw new System.InvalidOperationException("Queue time cannot move backward.");

            areaUnderCount += jobs.Count * (time - lastUpdateTime);
            lastUpdateTime = time;
        }
    }
}
