using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestSALT
{
    public partial class Form1 : Form
    {
        private Chart cpuFacilityChart;

        public Form1()
        {
            InitializeComponent();
            InitializeCpuFacilityGraph();
            AddCpuFacilityMenu();
        }

        private void InitializeCpuFacilityGraph()
        {
            tabPage1.Controls.Clear();

            TableLayoutPanel cpuFacilityOutputLayoutPanel = new TableLayoutPanel();
            cpuFacilityOutputLayoutPanel.ColumnCount = 1;
            cpuFacilityOutputLayoutPanel.Dock = DockStyle.Fill;
            cpuFacilityOutputLayoutPanel.RowCount = 2;
            cpuFacilityOutputLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            cpuFacilityOutputLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));

            outputTextBox.Dock = DockStyle.Fill;
            cpuFacilityOutputLayoutPanel.Controls.Add(outputTextBox, 0, 0);

            cpuFacilityChart = new Chart();
            cpuFacilityChart.AntiAliasing = AntiAliasingStyles.All;
            cpuFacilityChart.BackColor = Color.White;
            cpuFacilityChart.Dock = DockStyle.Fill;
            cpuFacilityChart.Padding = new Padding(8);
            cpuFacilityChart.Palette = ChartColorPalette.None;

            ChartArea chartArea = new ChartArea("DelayByClass");
            chartArea.AxisX.Interval = 1.0;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.Title = "Class";
            chartArea.AxisY.LabelStyle.Format = "0.##";
            chartArea.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chartArea.AxisY.Minimum = 0.0;
            chartArea.AxisY.Title = "Avg Delay in Queue (min)";
            cpuFacilityChart.ChartAreas.Add(chartArea);

            Legend legend = new Legend("Cases");
            legend.Alignment = StringAlignment.Center;
            legend.Docking = Docking.Top;
            cpuFacilityChart.Legends.Add(legend);

            cpuFacilityChart.Titles.Add(new Title(
                "Average Queue Delay by Class",
                Docking.Top,
                new Font("Segoe UI", 11F, FontStyle.Bold),
                Color.Black));

            cpuFacilityOutputLayoutPanel.Controls.Add(cpuFacilityChart, 0, 1);
            tabPage1.Controls.Add(cpuFacilityOutputLayoutPanel);

            ResetCpuFacilityChart();
        }

        private void AddCpuFacilityMenu()
        {
            ToolStripMenuItem cpuFacilityToolStripMenuItem = new ToolStripMenuItem("CPU Facility");
            ToolStripMenuItem runNonPreemptiveToolStripMenuItem = new ToolStripMenuItem("Run Non-Preemptive");
            ToolStripMenuItem runPreemptiveToolStripMenuItem = new ToolStripMenuItem("Run Preemptive-Resume");
            ToolStripMenuItem runBothToolStripMenuItem = new ToolStripMenuItem("Run Both Cases");
            ToolStripMenuItem runSectionFiveStudyToolStripMenuItem = new ToolStripMenuItem("Run Simulation Tables");
            ToolStripMenuItem clearOutputToolStripMenuItem = new ToolStripMenuItem("Clear Output");

            cpuFacilityToolStripMenuItem.Name = "cpuFacilityToolStripMenuItem";
            runNonPreemptiveToolStripMenuItem.Name = "cpuFacilityRunNonPreemptiveToolStripMenuItem";
            runPreemptiveToolStripMenuItem.Name = "cpuFacilityRunPreemptiveToolStripMenuItem";
            runBothToolStripMenuItem.Name = "cpuFacilityRunBothToolStripMenuItem";
            runSectionFiveStudyToolStripMenuItem.Name = "cpuFacilityRunSectionFiveStudyToolStripMenuItem";
            clearOutputToolStripMenuItem.Name = "cpuFacilityClearOutputToolStripMenuItem";

            runNonPreemptiveToolStripMenuItem.Click += CpuFacility_RunNonPreemptiveToolStripMenuItem_Click;
            runPreemptiveToolStripMenuItem.Click += CpuFacility_RunPreemptiveToolStripMenuItem_Click;
            runBothToolStripMenuItem.Click += CpuFacility_RunBothToolStripMenuItem_Click;
            runSectionFiveStudyToolStripMenuItem.Click += CpuFacility_RunSectionFiveStudyToolStripMenuItem_Click;
            clearOutputToolStripMenuItem.Click += CpuFacility_ClearOutputToolStripMenuItem_Click;

            cpuFacilityToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                runNonPreemptiveToolStripMenuItem,
                runPreemptiveToolStripMenuItem,
                runBothToolStripMenuItem,
                runSectionFiveStudyToolStripMenuItem,
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

        private void CpuFacility_RunSectionFiveStudyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCpuFacilityStudyReport();
        }

        private void CpuFacility_ClearOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            ResetCpuFacilityChart();
        }

        private void WriteCpuFacilityReport(params SALTx.CPUS.CpuFacilityResult[] results)
        {
            string reportText = SALTx.CPUS.CpuFacilityRunner.FormatReport(results);

            outputTextBox.WordWrap = false;
            outputTextBox.Font = new Font("Consolas", outputTextBox.Font.Size);
            outputTextBox.Text = reportText;
            SaveCpuFacilityReport(reportText, results);
            UpdateCpuFacilityChart(results);
            tabControl.SelectedTab = tabPage1;
        }

        private void SaveCpuFacilityReport(string reportText, params SALTx.CPUS.CpuFacilityResult[] results)
        {
            if (results == null || results.Length == 0)
                return;

            string reportsFolder = Path.Combine(Application.StartupPath, "CPUFacilityReports");
            Directory.CreateDirectory(reportsFolder);

            string caseName = results.Length == 1
                ? ToFileNamePart(results[0].ModeName)
                : "BothCases";
            string seed = results[0].Seed.ToString(CultureInfo.InvariantCulture);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            string fileName = string.Format(
                CultureInfo.InvariantCulture,
                "CPUFacility_{0}_Seed{1}_{2}.txt",
                caseName,
                seed,
                timestamp);

            File.WriteAllText(Path.Combine(reportsFolder, fileName), reportText, Encoding.UTF8);
        }

        private void WriteCpuFacilityStudyReport()
        {
            string reportText = SALTx.CPUS.CpuFacilityRunner.FormatSectionFiveTableReport(
                SALTx.CPUS.CpuFacilityRunner.DefaultSeed);

            outputTextBox.WordWrap = false;
            outputTextBox.Font = new Font("Consolas", outputTextBox.Font.Size);
            outputTextBox.Text = reportText;
            SaveCpuFacilityTextReport(
                "SimulationTables",
                SALTx.CPUS.CpuFacilityRunner.DefaultSeed,
                reportText);
            ResetCpuFacilityChart();
            tabControl.SelectedTab = tabPage1;
        }

        private void SaveCpuFacilityTextReport(string reportName, int seed, string reportText)
        {
            string reportsFolder = Path.Combine(Application.StartupPath, "CPUFacilityReports");
            Directory.CreateDirectory(reportsFolder);

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            string fileName = string.Format(
                CultureInfo.InvariantCulture,
                "CPUFacility_{0}_Seed{1}_{2}.txt",
                ToFileNamePart(reportName),
                seed,
                timestamp);

            File.WriteAllText(Path.Combine(reportsFolder, fileName), reportText, Encoding.UTF8);
        }

        private string ToFileNamePart(string value)
        {
            StringBuilder builder = new StringBuilder();

            foreach (char character in value)
            {
                if (char.IsLetterOrDigit(character))
                    builder.Append(character);
            }

            return builder.ToString();
        }

        private void UpdateCpuFacilityChart(params SALTx.CPUS.CpuFacilityResult[] results)
        {
            if (cpuFacilityChart == null)
                return;

            cpuFacilityChart.Series.Clear();

            if (results == null || results.Length == 0)
            {
                ResetCpuFacilityChart();
                return;
            }

            Color[] seriesColors = new Color[]
            {
                Color.FromArgb(54, 112, 214),
                Color.FromArgb(229, 119, 46)
            };

            for (int i = 0; i < results.Length; i++)
            {
                SALTx.CPUS.CpuFacilityResult result = results[i];
                Series series = new Series(result.ModeName);
                series.ChartArea = "DelayByClass";
                series.ChartType = SeriesChartType.Column;
                series.Color = seriesColors[i % seriesColors.Length];
                series.Font = new Font("Segoe UI", 8F);
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "0.##";
                series.Legend = "Cases";
                series["PointWidth"] = "0.58";

                for (int classLevel = 1; classLevel <= 4; classLevel++)
                {
                    SALTx.CPUS.CpuFacilityClassResult classResult = result.ClassResults
                        .First(item => item.ClassLevel == classLevel);
                    series.Points.AddXY(classLevel.ToString(CultureInfo.InvariantCulture), classResult.AverageDelayInQueue);
                }

                cpuFacilityChart.Series.Add(series);
            }

            cpuFacilityChart.ChartAreas["DelayByClass"].RecalculateAxesScale();
        }

        private void ResetCpuFacilityChart()
        {
            if (cpuFacilityChart == null)
                return;

            cpuFacilityChart.Series.Clear();
            cpuFacilityChart.ChartAreas["DelayByClass"].AxisY.Minimum = 0.0;
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
