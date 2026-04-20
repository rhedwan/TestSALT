using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALTx.ISYS
{
    public class Inventory : SALT.Node
    {
        public Inventory(int id, string name)
            : base(id, name, null, null)
        {

        }

        public override SALT.Event[] Execute()
        {
            SALT.Edge.Endpt[] endpts;
            Endpt endpt;
            object[] obj;
            int id, x, y, z;
            int[] d;
            double[] c;
            double r, t;
            List<SALT.Event> nxt = new List<SALT.Event>();
            SALT.RNG rng;
            string nm, lv;

            switch (SALT.Graph.Next.Type)
            {
                case "demand":
                    // execute demand
                    id = (int)SALT.Graph.Next.Args[1];
                    nm = (string)SALT.Graph.Next.Args[0];
                    endpt = (Endpt)GetEndpt(nm, id);
                    if (endpt == null)
                        throw new Exception();
                    x = (int)SALT.Graph.Next.Args[2]; // retrieve demand
                    lv = endpt.Set("demand", x);

                    // generate next demand event
                    rng = SALT.Daemon.GetRNG(nm, id); // exponential random number
                    t = rng.Next(); // time of next event
                    obj = endpt.Get("demands"); // get demands
                    d = (int[])obj[0];
                    obj = endpt.Get("cumulative-demand-distribution"); // get cumulative distribution of demands
                    c = (double[])obj[0];
                    rng = SALT.Daemon.GetRNG("inventory", 0);
                    nxt.Add(new SALT.Event(this, "demand", SALT.Graph.Clock.Now + t, "product", id, GetDemand(rng.Next(), d, c)));
                    break;

                case "evaluate":
                    endpts = GetEndpts((string)SALT.Graph.Next.Args[0]); // (string)Args[0] = "product"
                    if (endpts == null)
                        throw new Exception();
                    for (int i = 0; i < endpts.Length; i++)
                    {
                        // get product level
                        endpt = (Endpt)endpts[i];
                        obj = endpt.Get("id");
                        id = (int)obj[0];
                        obj = endpt.Get("levels");
                        x = (int)obj[0]; // num
                        y = (int)obj[1]; // min
                        if (x > y)
                            continue;

                        // schedule order-arrival event
                        z = (int)obj[2]; // max
                        x = z - x; // order qty
                        rng = SALT.Daemon.GetRNG("inventory", 0); // get uniform random number generator
                        obj = endpt.Get("lags");
                        t = GetLag(rng.Next(), (double)obj[0], (double)obj[1]);
                        nxt.Add(new SALT.Event(this, "order-arrival", SALT.Graph.Clock.Now + t, "product", id, x));
                    }
                    //
                    nxt.Add(new SALT.Event(this, "evaluate", SALT.Graph.Clock.Now + 1.0, "product"));
                    break;

                case "order-arrival":
                    id = (int)SALT.Graph.Next.Args[1];
                    nm = (string)SALT.Graph.Next.Args[0];
                    endpt = (Endpt)GetEndpt(nm, id);
                    if (endpt == null)
                        throw new Exception();
                    x = (int)SALT.Graph.Next.Args[2]; // retrieve order
                    lv = endpt.Set("order", x);
                    break;
            }

            if (nxt.Count == 0)
                return null;

            return nxt.ToArray();
        }

        public override SALT.Stat[] FinalizeStats() => null;

        public int GetDemand(double rnd, int[] dm, double[] cd)
        {
            int n = dm.Length;
            for (int i = 0; i < n; i++)
                if (rnd <= cd[i])
                    return dm[i];
            return dm[n - 1];
        }

        public double GetLag(double rnd, double minlag, double maxlag) => minlag + (rnd * (maxlag - minlag));

        public override SALT.Event[] Init()
        {
            double t;
            int[] d;
            double[] c;
            object[] obj;
            int j;
            SALT.RNG erg, urg;

            // 1. add a uniform RNG
            urg = new SALT.RNGx.Uniform(0.0, 1.0);
            SALT.Daemon.AddRNG(urg, "inventory", 0);

            // 2. generate evaluate event
            List<SALT.Event> evn = new List<SALT.Event>();
            evn.Add(new SALT.Event(this, "evaluate", SALT.Graph.Clock.Now + 0.0, "product"));

            // 3. generate demand events
            SALT.Edge.Endpt[] ept = GetEndpts("product"); // get all product endpoints
            for (int i = 0; i < ept.Length; i++)
            {
                // add random number generator
                obj = ept[i].Get("mean-interdemand-interval");
                erg = new SALT.RNGx.Exponential((double)obj[0]);
                obj = ept[i].Get("id");
                j = (int)obj[0]; // identifier
                SALT.Daemon.AddRNG(erg, "product", j);

                // generate demand event
                t = erg.Next();
                obj = ept[i].Get("demands"); // get demands
                d = (int[])obj[0];
                obj = ept[i].Get("cumulative-demand-distribution"); // get cumulative distribution of demands
                c = (double[])obj[0];
                evn.Add(new SALT.Event(this, "demand", SALT.Graph.Clock.Now + t, "product", j, GetDemand(urg.Next(), d, c)));
            }

            // 4. return events
            return evn.ToArray();
        }
    }
}
