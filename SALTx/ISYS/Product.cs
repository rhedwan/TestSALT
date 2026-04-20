using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALTx.ISYS
{
    public class Product : SALT.Node
    {
        private int iLvl = 0; // inventory level

        /// <summary>
        /// constructor of product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfValuesDemand"></param>
        /// <param name="meanInterDemand"></param>
        /// <param name="setupCost"></param>
        /// <param name="incrementalCost"></param>
        /// <param name="holdingCost"></param>
        /// <param name="shortageCost"></param>
        /// <param name="maxLag"></param>
        /// <param name="minLag"></param>
        public Product(int id, string name, object[][] demand, double meanInterDemand, double setupCost, double incrementalCost,
            double holdingCost, double shortageCost, int initialInventoryLevel, double maxLag, double minLag, int maxLvl, int minLvl)
            : base(id, name, new SALT.Statx.Ct[] { new SALT.Statx.Ct("holding-costs", 1), new SALT.Statx.Ct("shortage-costs", -1)}, new SALT.Statx.Dt[] {new SALT.Statx.Dt("setup-costs")})
        {
            int n = demand.GetLength(0);
            Demand = new int[n];
            CumulativeDemandDistr = new double[n];
            double cd = 0.0;
            for (int i = 0; i < n; i++)
            {
                Demand[i] = (int)demand[i][0];
                CumulativeDemandDistr[i] = (double)demand[i][1] + cd;
                cd = CumulativeDemandDistr[i];
            }
            //
            HoldingCost = holdingCost;
            IncrementalCost = incrementalCost;
            MaxLag = maxLag;
            MaxLvl = maxLvl;
            MeanInterDemand = meanInterDemand;
            MinLag = minLag;
            MinLvl = minLvl;
            SetupCost = setupCost;
            ShortageCost = shortageCost;
            //
            iLvl = initialInventoryLevel;
        }

        public void Add(int qty)
        {
            iLvl += qty;
            CtStats[0].Add(iLvl * HoldingCost, SALT.Graph.Clock.Now); // update holding costs
            CtStats[1].Add(iLvl * ShortageCost, SALT.Graph.Clock.Now); // update shortage costs
            DtStats[0].Add(qty, SALT.Graph.Clock.Now);  // update setup cost
        }

        public int Count => iLvl;

        public int[] Demand { get; }

        public double[] CumulativeDemandDistr { get; }

        public override SALT.Event[] Execute()
        {
            List<SALT.Event> evn = new List<SALT.Event>();
            SALT.Event e = SALT.Graph.Next;

            switch (e.Type)
            {
                case "e1":
                    evn.Add(new SALT.Event(this, "e1", SALT.Graph.Clock.Now + 0));
                    break;
                case "e2":
                    break;
            }

            return evn.ToArray();
        }

        /// <summary>
        /// finalizes stats
        /// </summary>
        /// <returns></returns>
        public override SALT.Stat[] FinalizeStats()
        {
            int n = CtStats.Length;
            for (int i = 0; i < n; i++)
                if (CtStats[i].Time.Last() < SALT.Graph.Clock.Now)
                    Add(0);
            //
            return new SALT.Stat[] { CtStats[0], CtStats[1], DtStats[0] };
        }

        public double HoldingCost { get; }

        /// <summary>
        /// initialize inventory
        /// </summary>
        /// <returns></returns>
        public override SALT.Event[] Init()
        {
            // initialize continous time statistics
            // holding cost
            CtStats[0].Init(Math.Max(iLvl, 0.0), SALT.Graph.Clock.Now);
            // shortage cost
            CtStats[1].Init(Math.Max(0 - iLvl, 0.0), SALT.Graph.Clock.Now);
            return null;
        }

        public double IncrementalCost { get; }

        public double MaxLag { get; }

        public int MaxLvl { get; }

        public double MeanInterDemand { get; }

        public double MinLag { get; }

        public int MinLvl { get; }

        public void Remove(int qty)
        {
            iLvl -= qty;
            CtStats[0].Add(iLvl, SALT.Graph.Clock.Now);
            CtStats[1].Add(iLvl, SALT.Graph.Clock.Now);
        }

        public double ShortageCost { get; }

        public double SetupCost { get; }

        public override string ToString()
        {
            string d = "["; // demand string
            for (int i = 0; i < Demand.Length; i++)
                d += $"{ Demand[i] } ";
            d = d.TrimEnd(' ') + "]";
            string cd = "[";
            for (int i = 0; i < CumulativeDemandDistr.Length; i++)
                cd += $"{CumulativeDemandDistr[i]:0.###} ";
            cd = cd.TrimEnd(' ') + "]";

            string p = "";
            p += $"id={ ID }, name={ Name }, level={ Count }, demand={ d }, cumulativeDemandDistr={ cd }, holdCost={ HoldingCost }, incrCost={ IncrementalCost }";
            p += $"\nmaxlag={ MaxLag }, minlag={ MinLag }, maxlvl={ MaxLvl }, minlvl={ MinLvl }, meanInterDemand={ MeanInterDemand }, " +
                $"shortCost={ ShortageCost }, setupCost={ SetupCost}";

            return p;
        }
    }
}
