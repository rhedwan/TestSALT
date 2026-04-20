using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace TestSALT
{
    partial class Form1 : Form
    {
        partial void Test_SALT_DaemonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SALT.RNG exp = new SALT.RNGx.Exponential(0.45);
            SALT.RNG uni = new SALT.RNGx.Uniform(0.0, 1.0);

            SALT.Daemon.AddRNG(exp, "product", 0);
            SALT.Daemon.AddRNG(uni, "product", 1);

            SALT.RNG rng = SALT.Daemon.GetRNG("product", 0);
            double t = rng.Next();
            double m = ((SALT.RNGx.Exponential)rng).Mean;

            outputTextBox.Text = $"Mean={ m }, Next={ t }";
        }

        partial void Test_SALT_Stat_CtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string log = "";
            
            SALT.Statx.Ct ct = new SALT.Statx.Ct("server-utility", 1).Init(64, 0.0);
            ct.Add(68, 0.5);
            ct.Add(32, 1.0);
            ct.Add(30, 1.5);

            log += ct.ToString();

            outputTextBox.Text = log;
        }

        partial void Test_SALT_Stat_DtToolStripMenuItem(object sender, EventArgs e)
        {
            string log = "";

            SALT.Statx.Dt dt = new SALT.Statx.Dt("setup-cost");
            dt.Add(345, 1.0); // $345
            dt.Add(255, 2.0);
            dt.Add(902, 3.0);

            log += dt.ToString();

            outputTextBox.Text = log;
        }
    }
}