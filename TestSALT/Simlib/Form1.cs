using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Simlib;

namespace TestSALT
{
    partial class Form1 : Form
    {
        private string CounterFormattingString(int count)
        {
            double x = Math.Log10(count);
            double f = Math.Floor(x);
            if (f != x)
                f += 1.0;
            int m = (int)f;
            string s = "";
            for (int i = 0; i < m; i++)
                s += "0";
            return s;
        }

        partial void Test_Simlib_ExponentialRNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random r = new Random(); // instantiate RNG

            string f, log = "\n"; // initialize log

            int n = 1000, p = 20; // n=number of random numbers, p=partitions in the range [0,1]
            double m = 5.0; // m=mean (or beta)

            double[] uf = new double[n], ex = new double[n];

            int[] dt = new int[p + 1]; // dt=data-store
            f = CounterFormattingString(n);
            for (int i = 0; i < n; i++)
            {
                uf[i] = r.NextDouble(); // number in uniform distribution [0.0, 1.0]
                ex[i] = (0.0 - m) * Math.Log(uf[i]); // number in exponential distribution
                //
                log += "\n\tu[" + i.ToString(f) + "]: " + uf[i].ToString("0.000000") + " e[" + i.ToString(f) + "]: " + ex[i].ToString("00.000000");
            }

            double mx = double.MinValue; // stroes max value in e
            for (int i = 0; i < n; i++)
                mx = Math.Max(mx, ex[i]);
            double d = 1.0 / p, y; // d=delta: size-of-partition in normalized sample space
            int j;
            for (int i = 0; i < n; i++)
            {
                y = ex[i] / mx;
                j = (int)Math.Floor(y / d);
                dt[j] += 1;
            }

            log += "\n\nhistogram @ spacings:" + d + "\n";
            f = CounterFormattingString(p);
            for (int i = 0; i < p; i++)
                log += "\n\tpd[" + i.ToString(f) + "]: " + ((double)dt[i] / n);

            outputTextBox.Text = log;
        }

        partial void Test_Simlib_InventorySystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] policy;
            int inv = 34;
            int[] demand = new int[] { 1, 2, 3, 4 };
            int months = 60;
            double meanInterDemandTime = 0.1;
            double minDeliveryLag = 0.5, maxDeliveryLag = 1.0;

            double orderingCost = 3.0, holdingCost = 1.0, setupCost = 32.0, shortageCost = 5.0;

            int[,] policies = { { 20, 60 }, { 40, 60 } };

            Simlib.DESx.ISYS inms;

            for (int i = 0; i < policies.GetLength(0); i++)
            {
                policy = new int[] { policies[i, 0], policies[i, 1] };

                inms = new Simlib.DESx.ISYS(policy, inv, demand, meanInterDemandTime, minDeliveryLag, maxDeliveryLag,
                    months, holdingCost, orderingCost, setupCost, shortageCost);

                inms.Init();

                // 3. run simulation
                while (inms.Next() == 1) ;

                // 4. report run
                outputTextBox.Text = inms.Report();
            }
        }

        partial void Test_Simlib_SingleServerQueueingSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 0. instantiate SSQS object
            Simlib.DESx.SSQS ssqs = new Simlib.DESx.SSQS();

            // 1. set mean interarrivaltime and meanservicetime
            ssqs.MeanInterArrivalTime = 1.0;
            ssqs.MeanServiceTime = 0.5;
            ssqs.NumberOfDelays = 20;
            ssqs.QLIMIT = 20;

            // 2. initialiaze ssqs
            if (ssqs.Init() != 1)
                throw new Exception();

            // 3. run simulation
            while (ssqs.Next() == 1) ;

            // 4. report run
            outputTextBox.Text = ssqs.Report();
        }

        partial void Test_Simlib_UniformRNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Random r = new Random();

            string log = "\n";

            int n = 10000, p = 30, X; // X=random variable
            int m = (int)Math.Ceiling(Math.Log10(n));
            string f = CounterFormattingString(n);

            double[] uf = new double[n]; // uf=uniformly distributed random-numbers

            int[] rv = new int[p]; // rv=freq of random-variables

            for (int i = 0; i < n; i++)
            {
                uf[i] = r.NextDouble(); // uniform distribution [0.0, 1.0]
                X = (int)Math.Floor(uf[i] * p);
                rv[X] += 1;
                log += "\n\tr[" + i.ToString(f) + "]: " + uf[i].ToString("0.000000") + " rv[" + i.ToString(f) + "]: " + X;
            }

            f = CounterFormattingString(p);
            log += "\n\nhistogram @ " + (1.0 / p) + "\n";

            for (int i = 0; i < p; i++)
                log += "\n\tpd[" + i.ToString(f) + "]: " + ((double)rv[i] / n); // probability density

            outputTextBox.Text = log;
        }
    }
}
