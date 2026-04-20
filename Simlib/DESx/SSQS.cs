using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib.DESx
{
    public class SSQS : DES
    {
        private Clock clock;
        private Events events;
        private List<Entity> queue;
        private Server[] server;
        private Stats stats;
        private string etype; // type of event

        private int next;
        private string log = "";

        public SSQS()
            : base()
        {
            //
        }

        private void Arrival()
        {
            // 0. schedule next arrival
            double t = clock.Now + Daemon.Random[0].Next(); // generates the next arrival time
            events.Add(new Event(t, "ARRIVAL")); // add event to event list

            // 1. get id of unit
            int id = Entity.NextID;

            // 1. check if server is busy
            if (server[0].Status == Status.BUSY)
            {
                // server is busy so increment numberof customers in queue
                queue.Add(new Entity(id, clock.Now));
                // check for overflow
                if (queue.Count > QLIMIT)
                {
                    // queue overflow, stop simulation
                    log += $"Queue overflow at arrival at time: {clock.Now}";
                    next = 0;
                    return;
                }
                // there is still room in the queue
                return;
            }

            // server is not busy
            // arriving has a delay of 0
            stats.TotalDelay += 0.0;
            // increment number of customers and make server idle
            stats.NumCustsDelayed += 1;
            server[0].Status = Status.BUSY;
            // schedule departure event
            t = clock.Now + Daemon.Random[1].Next(); // generates the next departure time
            events.Add(new Event(t, "DEPARTURE")); // add department event to event list
        }

        private void Departure()
        {
            // check if queue is empty
            if (queue.Count == 0)
            {
                server[0].Status = Status.IDLE;
                return;
            }
            // queue is not empty
            // compute the delay of customer entering service
            double d = clock.Now - queue[0].TofA;
            stats.TotalDelay += d;
            // increment number of customers delayed and schedule departure
            stats.NumCustsDelayed += 1;
            // schedule departure event
            double t = clock.Now + Daemon.Random[1].Next(); // generates the next departure time
            events.Add(new Event(t, "DEPARTURE")); // add department event to event list
            // remove customers in queue
            queue.RemoveAt(0);
        }

        public override int Init()
        {
            // 0. initialize RN streams
            Daemon.Random = new RNG[2];
            Daemon.Random[0] = new RNGx.Exponential(MeanInterArrivalTime); // random variates for inter-arrival time
            Daemon.Random[1] = new RNGx.Exponential(MeanServiceTime); // random variates for service time

            log += $"\n\tMean Inter-Arrival Time: {MeanInterArrivalTime}";
            log += $"\n\tMean Service Time: {MeanServiceTime}";
            log += $"\n\tNumber of Customers: {NumberOfDelays}";

            // 1. initialize the simulation clock 
            clock = new Clock(0);

            // 2. initialize state variables
            server = new Server[1];
            server[0] = new Server();
            server[0].Status = Status.IDLE;
            queue = new List<Entity>();
            if (QLIMIT == 0)
                return 0;

            // 3. initialize statistical counters
            stats = new Stats();
            stats.NumCustsDelayed = 0;
            stats.TotalDelay = 0.0;

            // 4. initialize events list
            events = new Events();
            double t = clock.Now + Daemon.Random[0].Next(); // generates the next arrival time
            Event e = new Event(t, "ARRIVAL");
            events.Add(e); // add event to event list

            // 5. return 1 for succesful initialization
            return 1;
        }

        public double MeanInterArrivalTime { get; set; }

        public double MeanServiceTime { get; set; }

        public override int Next()
        {
            if (stats.NumCustsDelayed >= NumberOfDelays)
                return 0;
            next = 1;

            // 0. invoke the timing routine
            Timing();

            // 1. update statistics
            stats.Update(server[0].Status == Status.BUSY ? 1 : 0, queue.Count, clock.Now, clock.TimeOfLastEvent);
            log += ToString();

            // 2. invoke the event routine
            switch (etype)
            {
                case "ARRIVAL":
                    Arrival();
                    break;
                case "DEPARTURE":
                    Departure();
                    break;
            }

            return next;
        }

        public int NumberOfDelays { get; set; }

        public int QLIMIT { get; set; }

        public string Report()
        {
            log += "\n\n\tSUMMARY...";
            // log average delay in queue statistic
            log += $"\n\tAverage Delay in Queue: { stats.AvgDelayInQ }";
            // log average number in queue statistic
            log += $"\n\tAverage Number in Queue: { stats.AvgNumInQ }";
            // log server utilization
            log += $"\n\tServer Utilization: {stats.ServerUtilization}";
            // log end of simulation
            log += $"\n\tTime Simulation Ended: {clock.Now} minutes";

            return log;
        }

        private void Timing()
        {
            // retrive the next event
            Simlib.Event e = events.Next();
            clock.TimeOfLastEvent = clock.Now;
            clock.Now = e.Time;
            etype = e.Type;
        }

        /// <summary>
        /// dashboard
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string db = "\n\n\tDASHBOARD...";
            db += $"\n\tClock: { clock.Now }; Current Event: { etype }";
            db += "\n\tEvents...";
            IOrderedEnumerable<Event> e = events.List.OrderBy(ev => ev.Time);
            int n = e.Count();
            if (n == 0)
                db += "\n\t####";
            else
            {
                for (int i = 0; i < n; i++)
                    db += $"\n\t[{ i + 1 }] { e.ElementAt(i) }";
            }
            db += $"\n\tNumber in Queue: { queue.Count }; Time of Last Event: { clock.TimeOfLastEvent }; Server Status: { (server[0].Status == Status.BUSY ? 1 : 0) }";
            db += "\n\tTime of Arrival in Queue...";
            n = queue.Count;
            if (n == 0)
                db += "\n\t####";
            else
            {
                for (int i = 0; i < n; i++)
                    db += $"\n\t[{ i + 1 }] Time: { queue[i].TofA }; ID: { queue[i].ID }";
            }
            db += "\n\tStatistics...";
            db += stats.ToString();
            return db;
        }

        /// <summary>
        /// statistics
        /// </summary>
        public class Stats
        {
            private double aunq = 0.0, auss = 0.0, avnq = 0.0, sutl = 0.0;

            /// <summary>
            /// area under number in queue curve
            /// </summary>
            public double AreaUnderNumInQ => aunq;

            /// <summary>
            /// area under server status curve
            /// </summary>
            public double AreaUnderServerStatus => auss;

            /// <summary>
            /// average delay in queue
            /// </summary>
            public double AvgDelayInQ => TotalDelay / NumCustsDelayed;

            /// <summary>
            /// avarege number in queue
            /// </summary>
            public double AvgNumInQ => avnq;

            /// <summary>
            /// number of customers delay
            /// </summary>
            public int NumCustsDelayed { get; set; }

            /// <summary>
            /// server untilization
            /// </summary>
            public double ServerUtilization => sutl;

            /// <summary>
            /// total delay of customers
            /// </summary>
            public double TotalDelay { get; set; }

            public override string ToString()
            {
                string board = "";
                board += $"\n\tNumber of Customers Delayed: { NumCustsDelayed }; Total Delay: { TotalDelay }";
                board += $"\n\tAverage Number in Queue: { AvgNumInQ }; Average Delay in Queue: { AvgDelayInQ }";
                board += $"\n\tArea Under Number in Queue: { AreaUnderNumInQ }; Area Under Server Status: { AreaUnderServerStatus }";
                return board;
            }

            /// <summary>
            /// update statistics
            /// </summary>
            /// <param name="serverStatus">server status</param>
            /// <param name="ninQ">number in queue</param>
            /// <param name="timeNow">time now</param>
            /// <param name="timeoflastEvent">time of last event</param>
            /// <returns></returns>
            public string Update(int serverStatus, int ninQ, double timeNow, double timeoflastEvent)
            {
                double t = timeNow - timeoflastEvent;
                aunq += ninQ * t;
                auss += serverStatus * t;
                avnq = AreaUnderNumInQ / timeNow;
                sutl = AreaUnderServerStatus / timeNow;
                //
                return ToString();
            }
        }
    }
}
