using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SALTx.ISYS;

namespace TestSALT
{
    partial class Form1 : Form
    {
        partial void Test_SALTx_ISYS_EdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object[][] demand = new object[][] { new object[] { 1, 1.0 / 6.0 }, new object[] { 2, 1.0 / 3.0 }, new object[] { 3, 1.0 / 3.0 }, new object[] { 4, 1.0 / 6.0 } };
            Product inv = new Product(0, "product", demand, 0.5, 2.51, 0.55, 1.45, 2.58, 64, 0.6, 0.2, 80, 20);
            Inventory mng = new Inventory(0, "inventory");

            Edge edge = new Edge(mng, inv);

            outputTextBox.Text = edge.ToString();
        }

        partial void Test_SALTx_ISYS_EndptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object[][] demand = new object[][] { new object[] { 1, 1.0 / 6.0 }, new object[] { 2, 1.0 / 3.0 }, 
                new object[] { 3, 1.0 / 3.0 }, new object[] { 4, 1.0 / 6.0 } };
            Product prd = new Product(0, "product", demand, 0.5, 2.51, 0.55, 1.45, 2.58, 64, 0.8, 0.1, 80, 20);
            Inventory inv = new Inventory(0, "inventory");

            Endpt x = new Endpt("inventory", inv);
            Endpt y = new Endpt("product", prd);

            x.Twin = y;
            y.Twin = x;

            string log = "";
            object[] data;

            log += $"\n\n\tinventory endpoint query's product endpoint for name and id";
            data = x.Get("name-&-id");
            log += $"\n\tresult: { (string)data[0] } { (int)data[1] }";
            log += $"\n\n\tproduct endpoint query's inventory endpoint for name and id";
            data = y.Get("name-&-id");
            log += $"\n\tresult: { (string)data[0] } { (int)data[1] }";
            //
            log += $"\n\n\tinventory sets new product level";
            log += $"\n\told product level { prd.Count }";
            log += $"\n\treply: { x.Set("add-new-supplies", 20) }";
            log += $"\n\tnew product level { prd.Count }";

            outputTextBox.Text = log;
        }
        partial void test_SALTx_ISYS_FactoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string log = "";

            double[] midt = new double[] { 0.05, 1.0 }; // low point range
            object[] setc = new object[] { 5, 1.5 };
            object[] incc = new object[] { 10, 0.1 };
            object[] hldc = new object[] { 10, 0.05 };
            object[] shtc = new object[] { 10, 0.25 };

            List<Product> ls = new List<Product>();
            Product p;

            for (int i = 0; i < 10; i++)
            {
                p = Factory.MakeProduct(midt, 4, setc, incc, hldc, shtc, 20, 10);
                log += $"\n\t{ p }";
                ls.Add(p);
            }

            outputTextBox.Text = log;
        }

        partial void Test_SALTx_ISYS_GraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int initialInventoryLevel = 57;
            int numberOfMonths = 60;
            object[][] demand = new object[][] { new object[] { 1, 1.0 / 6.0 }, new object[] { 2, 1.0 / 3.0 },
                new object[] { 3, 1.0 / 3.0 }, new object[] { 4, 1.0 / 6.0 } };

            double holdingCost = 0.5;
            double incrementalCost = 1.5;
            double meanInterDemand = 0.08;
            double maxLag = 0.6;
            double minLag = 0.2;
            double setupCost = 2.00;
            double shortageCost = 0.8;

            string log = "";

            Graph g = new Graph(initialInventoryLevel, numberOfMonths, demand, meanInterDemand, setupCost, incrementalCost, 
                holdingCost, shortageCost, maxLag, minLag, 20, 60);

            g.Run();

            log += g.Stats.ToString();

            outputTextBox.Text = log;
        }

        partial void Test_SALTx_ISYS_GraphProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string log = "";

            // 0. setup products factory
            double[] midt = new double[] { 0.05, 0.15 }; // mean interdemand time
            object[] setc = new object[] { 5, 1.5 };    // range of setup costs
            object[] incc = new object[] { 10, 0.1 };   // range of incremental costs
            object[] hldc = new object[] { 10, 0.05 };  // range of holding costs
            object[] shtc = new object[] { 10, 0.25 };  // range of shortage costs

            // 1. generate products
            List<Product> ls = new List<Product>();
            Product p;
            for (int i = 0; i < 10; i++)
            {
                p = Factory.MakeProduct(midt, 4, setc, incc, hldc, shtc, 20, 10);
                log += $"\n\t{ p }";
                ls.Add(p);
            }

            // 2. instantiate graph and run simulation
            Graph g = new Graph(ls);

            g.Run();

            // 3. get run statistics
            log += g.Stats.ToString();

            outputTextBox.Text = log;
        }

        partial void Test_SALTx_ISYS_InventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string log = "";

            Random r = new Random();

            object[][] d = new object[][] { new object[] { 1, 2.0/5.0}, new object[] { 2, 2.0/5.0 }, new object[] { 3, 1.0/10.0 },
                new object[] { 4, 1.0/10.0 } };
            double midt = 0.05; // mean inter demand time
            double hc = 0.15, ic = 2.03, sc = 2.0, st = 1.54; // hc: holding cost, ic: icremental cost, sc = setup cost, st= shortage cost
            double mxlg = 0.25, mnlg = 0.15; // mxl: max-supply-lag, mnl: min-supply-lag
            int iv = 34, mxlv = 120, mnlv = 20; // iv: inventory level

            Product p0 = new Product(0, "product", d, midt, sc, ic, hc, st, iv, mxlg, mnlg, mxlv, mnlv);
            Product p1 = new Product(1, "product", d, midt, sc, ic, hc, st, iv, mxlg, mnlg, mxlv, mnlv);
            Product p2 = new Product(2, "product", d, midt, sc, ic, hc, st, iv, mxlg, mnlg, mxlv, mnlv);
            Product p3 = new Product(3, "product", d, midt, sc, ic, hc, st, iv, mxlg, mnlg, mxlv, mnlv);

            Inventory v = new Inventory(0, "inventory");

            Edge[] edges = new Edge[] { new Edge(v, p0), new Edge(v, p1), new Edge(v, p2), new Edge(v, p3) };

            log += $"\n\tID={ v.ID }";
            log += $"\n\tName={ v.Name }";
            SALT.Edge.Endpt[] endpts = v.GetEndpts("product");
            for (int i = 0; i < endpts.Length; i++)
                log += $"\n\tendpt[{ i }][{ endpts[i] }]";

            int demand = v.GetDemand(r.NextDouble(), p0.Demand, p0.CumulativeDemandDistr);
            log += $"\n\tdemand={ demand }";

            double lag = v.GetLag(r.NextDouble(), p0.MinLag, p0.MaxLag);
            log += $"\n\tminlag={ p0.MinLag }, lag={ lag }, maxlag={ p0.MaxLag }";

            outputTextBox.Text = log;
        }

        partial void Test_SALTx_ISYS_ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object[][] d = new object[][] { new object[] { 1, 2.0/5.0}, new object[] { 2, 2.0/5.0 }, new object[] { 3, 1.0/10.0 },
                new object[] { 4, 1.0/10.0 } };
            double midt = 0.05; // mean inter demand time
            double hc = 0.15, ic = 2.03, sc = 2.0, st = 1.54; // hc: holding cost, ic: icremental cost, sc = setup cost, st= shortage cost
            double mxlg = 0.25, mnlg = 0.15; // mxl: max-supply-lag, mnl: min-supply-lag
            int iv = 34, mxlv = 120, mnlv = 20; // iv: inventory level

            Product p = new Product(0, "product", d, midt, sc, ic, hc, st, iv, mxlg, mnlg, mxlv, mnlv);

            outputTextBox.Text = p.ToString();
        }

        partial void Test_SALTx_CPUS_ServerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}