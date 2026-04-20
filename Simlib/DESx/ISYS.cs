using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simlib.DESx
{
    /// <summary>
    /// Inventory SYStem
    /// </summary>
    public class ISYS : DES
    {
        private string etype;
        private Events events;
        private int next;
        private Clock clock;
        private Stats stats;

        public ISYS(int[] policy, int initialInventoryLevel, int[] demandSizes, double meanInterDemandTime, double minDeliveryLag, double maxDeliveryLag, int months,
            double holdingCost, double incrementalCost, double setupCost, double shortageCost)
            : base()
        {
            Policy = policy;

            InitialInventoryLevel = initialInventoryLevel;

            DemandSizes = demandSizes;

            MeanInterDemandTime = meanInterDemandTime;

            MaxDeliveryLag = maxDeliveryLag;
            MinDeliveryLag = minDeliveryLag;

            Months = months;
            // costs
            HoldingCost = holdingCost;
            IncrementalCost = incrementalCost;
            SetupCost = setupCost;
            ShotageCost = shortageCost;
        }

        private void Demand()
        {

        }

        public int[] DemandSizes { get; }

        public double[] DemandSizeDistr { get; }

        private void Evaluate()
        {

        }

        public double HoldingCost { get; }

        public double IncrementalCost { get; }

        public int InitialInventoryLevel { get; }

        public override int Init()
        {
            // 0. initialize random number streams
            Daemon.Random = new RNG[2];
            Daemon.Random[0] = new RNGx.Exponential(MeanInterDemandTime); // random variates for inter-arrival time
            Daemon.Random[1] = new RNGx.Uniform(MinDeliveryLag, MaxDeliveryLag); // random variates for service time

            // 1. initialize simulation clock
            clock = new Clock(0.0);
            clock.TimeOfLastEvent = 0.0;

            // 2. initialize statitics
            stats = new Stats();
            stats.AreaHolding = 0.0;
            stats.AreaShortage = 0.0;
            stats.Demand = 0;
            stats.Inventory = InitialInventoryLevel;
            stats.OrderAmt = 0;
            stats.TotalOrderingCost = 0.0;

            // 3. generate initial events
            events = new Events();
            events.Add(new Event(0.0, "EVALUATE")); // add evaluate event
            events.Add(new Event(clock.Now + Daemon.Random[0].Next(), "DEMAND")); // add demand event to event list
            events.Add(new Event(Months, "ENDSIMULATION")); // add end simulation event to event list

            // 4. return status
            return 1;
        }

        public double MaxDeliveryLag { get; }

        public double MinDeliveryLag { get; }

        public double MeanInterDemandTime { get; }

        public int Months { get; }

        public override int Next()
        {
            next = 1;

            // Determine the next event
            Timing();

            // Update time-average statistical accumulators
            UpdateStats();

            // Invoke the appropriate event function
            switch (etype)
            {
                case "ORDERARRIVAL":
                    OrderArrival();
                    break;
                case "DEMAND":
                    Demand();
                    break;
                case "EVALUATE":
                    Evaluate();
                    break;
                case "ENDSIMULATION":
                    Report();
                    next = 0;
                    break;
            }

            return next;
        }

        private void OrderArrival()
        {

        }

        public int[] Policy { get; }

        public string Report()
        {
            /*
            Compute and write estimates of desired measures of performance

            float avg_holding_cost, avg_ordering_cost, avg_shortage_cost;
            avg_ordering_cost = total_ordering_cost / num_months;
            avg_holding_cost = holding_cost * area_holding / num_months;
            avg_shortage_cost = shortage_cost * area_shortage / num_months;
            fprintf(outfile, "\n\n(%3d,%3d)%15.2f%15.2f%15.2f%15.2f",
            smalls, bigs,
            avg_ordering_cost + avg_holding_cost + avg_shortage_cost,
            avg_ordering_cost, avg_holding_cost, avg_shortage_cost);
            */

            return "";
        }

        public double SetupCost { get; }

        public double ShotageCost { get; }

        private void Timing()
        {

        }

        private void UpdateStats()
        {

        }

        public class Stats
        {
            public Stats()
            {

            }

            public double AreaHolding { get; set; }

            public double AreaShortage { get; set; }

            public int Demand { get; set; }

            public int Inventory { get; set; }

            public int OrderAmt { get; set; }

            public double TotalOrderingCost { get; set; }
        }
    }
}
