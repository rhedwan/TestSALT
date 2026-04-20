using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSALT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region SALT
        partial void Test_SALT_DaemonToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALT_Stat_CtToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALT_Stat_DtToolStripMenuItem(object sender, EventArgs e);

        #endregion

        #region SALTx

        partial void Test_SALTx_ISYS_EdgeToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALTx_ISYS_EndptToolStripMenuItem_Click(object sender, EventArgs e);

        partial void test_SALTx_ISYS_FactoryToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALTx_ISYS_GraphToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALTx_ISYS_GraphProductsToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALTx_ISYS_InventoryToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALTx_ISYS_ProductToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_SALTx_CPUS_ServerToolStripMenuItem_Click(object sender, EventArgs e);

        #endregion

        #region Simlib

        partial void Test_Simlib_ExponentialRNGToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_Simlib_InventorySystemToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_Simlib_SingleServerQueueingSystemToolStripMenuItem_Click(object sender, EventArgs e);

        partial void Test_Simlib_UniformRNGToolStripMenuItem_Click(object sender, EventArgs e);

        #endregion
    }
}