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
            AddCpuFacilityMenu();
        }

        private void AddCpuFacilityMenu()
        {
            ToolStripMenuItem cpuFacilityToolStripMenuItem = new ToolStripMenuItem("CPU Facility");
            ToolStripMenuItem runNonPreemptiveToolStripMenuItem = new ToolStripMenuItem("Run Non-Preemptive");
            ToolStripMenuItem runPreemptiveToolStripMenuItem = new ToolStripMenuItem("Run Preemptive-Resume");
            ToolStripMenuItem runBothToolStripMenuItem = new ToolStripMenuItem("Run Both Cases");
            ToolStripMenuItem clearOutputToolStripMenuItem = new ToolStripMenuItem("Clear Output");

            cpuFacilityToolStripMenuItem.Name = "cpuFacilityToolStripMenuItem";
            runNonPreemptiveToolStripMenuItem.Name = "cpuFacilityRunNonPreemptiveToolStripMenuItem";
            runPreemptiveToolStripMenuItem.Name = "cpuFacilityRunPreemptiveToolStripMenuItem";
            runBothToolStripMenuItem.Name = "cpuFacilityRunBothToolStripMenuItem";
            clearOutputToolStripMenuItem.Name = "cpuFacilityClearOutputToolStripMenuItem";

            runNonPreemptiveToolStripMenuItem.Click += CpuFacility_RunNonPreemptiveToolStripMenuItem_Click;
            runPreemptiveToolStripMenuItem.Click += CpuFacility_RunPreemptiveToolStripMenuItem_Click;
            runBothToolStripMenuItem.Click += CpuFacility_RunBothToolStripMenuItem_Click;
            clearOutputToolStripMenuItem.Click += CpuFacility_ClearOutputToolStripMenuItem_Click;

            cpuFacilityToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                runNonPreemptiveToolStripMenuItem,
                runPreemptiveToolStripMenuItem,
                runBothToolStripMenuItem,
                new ToolStripSeparator(),
                clearOutputToolStripMenuItem
            });

            menuStrip1.Items.Add(cpuFacilityToolStripMenuItem);
        }

        private void CpuFacility_RunNonPreemptiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCpuFacilityReport(SALTx.CPUS.CpuFacilityRunner.Run(
                SALTx.CPUS.SimulationMode.NonPreemptive,
                SALTx.CPUS.CpuFacilityRunner.DefaultSeed));
        }

        private void CpuFacility_RunPreemptiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCpuFacilityReport(SALTx.CPUS.CpuFacilityRunner.Run(
                SALTx.CPUS.SimulationMode.PreemptiveResume,
                SALTx.CPUS.CpuFacilityRunner.DefaultSeed));
        }

        private void CpuFacility_RunBothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCpuFacilityReport(SALTx.CPUS.CpuFacilityRunner.RunBoth(SALTx.CPUS.CpuFacilityRunner.DefaultSeed));
        }

        private void CpuFacility_ClearOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
        }

        private void WriteCpuFacilityReport(params SALTx.CPUS.CpuFacilityResult[] results)
        {
            outputTextBox.WordWrap = false;
            outputTextBox.Font = new Font("Consolas", outputTextBox.Font.Size);
            outputTextBox.Text = SALTx.CPUS.CpuFacilityRunner.FormatReport(results);
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
