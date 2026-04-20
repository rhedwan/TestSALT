using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALTx.ISYS
{
    public class Factory
    {
        public static int id = 0;
        public static Random r = new Random();

        public static Product MakeProduct(double[] meanInterDemand, int demandSize, object[] setupCost, object[] incrCost, object[] holdCost,
            object[] shrtCost, int maxLvl, int maxlag)
        {
            double midt = meanInterDemand[0] + (r.NextDouble() * meanInterDemand[1]);

            int dsize = demandSize / 2 + r.Next(1, demandSize + 1);
            
            object[][] d = new object[dsize][];
            
            for (int i = 0; i < dsize; i++)
                d[i] = new object[] { i + 1, 1.0 / dsize };
            
            int x = r.Next(1, (int)setupCost[0]); // K + mc 10, 0.5

            double ic = r.Next(1, (int)incrCost[0]) * (double)incrCost[1];
            double hc = r.Next(1, (int)holdCost[0]) * (double)holdCost[1];
            double sc = r.Next(1, (int)shrtCost[0]) * (double)shrtCost[1];
            
            int minlvl = 20;
            int maxlvl = minlvl + (r.Next(1, maxLvl) * 10);
            int lvl = r.Next(1, maxlvl);
            
            double mnlag = 0.10;
            double mxlag = mnlag + (r.Next(1, maxlag) * mnlag);

            Product p = new Product(id++, "product", d, midt, x * (double)setupCost[1], ic, hc, sc, lvl, mxlag, mnlag, maxlvl, minlvl);

            return p;
        }
    }
}
