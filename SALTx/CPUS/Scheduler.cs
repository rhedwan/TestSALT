using System.Collections.Generic;

namespace SALTx.CPUS
{
    internal enum FacilityEventType
    {
        Arrival,
        CloseDoor,
        EndService
    }

    internal sealed class ScheduledEvent
    {
        internal ScheduledEvent(long id, double time, FacilityEventType type, Job job)
        {
            Id = id;
            Time = time;
            Type = type;
            Job = job;
        }

        internal bool Canceled { get; set; }

        internal long Id { get; }

        internal Job Job { get; }

        internal double Time { get; }

        internal FacilityEventType Type { get; }
    }

    internal sealed class Scheduler
    {
        private readonly List<ScheduledEvent> events = new List<ScheduledEvent>();
        private long nextId;

        internal ScheduledEvent Schedule(double time, FacilityEventType type, Job job = null)
        {
            ScheduledEvent scheduledEvent = new ScheduledEvent(nextId++, time, type, job);
            events.Add(scheduledEvent);
            return scheduledEvent;
        }

        internal void Cancel(ScheduledEvent scheduledEvent)
        {
            if (scheduledEvent != null)
                scheduledEvent.Canceled = true;
        }

        internal bool TryPopNext(out ScheduledEvent scheduledEvent)
        {
            int bestIndex = -1;
            ScheduledEvent bestEvent = null;

            for (int i = 0; i < events.Count; i++)
            {
                ScheduledEvent candidate = events[i];
                if (candidate.Canceled)
                    continue;

                if (bestEvent == null
                    || candidate.Time < bestEvent.Time
                    || (candidate.Time == bestEvent.Time && candidate.Id < bestEvent.Id))
                {
                    bestEvent = candidate;
                    bestIndex = i;
                }
            }

            if (bestIndex < 0)
            {
                scheduledEvent = null;
                return false;
            }

            events.RemoveAt(bestIndex);
            scheduledEvent = bestEvent;
            return true;
        }
    }
}
