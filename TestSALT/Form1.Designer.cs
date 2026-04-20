
namespace TestSALT
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sALTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALT_DaemonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALT_Stat_CtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALT_Stat_DtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sALTxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iSYSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_ISYS_EdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_ISYS_EndptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_ISYS_FactoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_ISYS_GraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_ISYS_GraphProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_ISYS_InventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_ISYS_ProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simlibToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_Simlib_ExponentialRNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_Simlib_InventorySystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_Simlib_SingleServerQueueingSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_Simlib_UniformRNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cPUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test_SALTx_CPUS_ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testToolStripMenuItem,
            this.runToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(882, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sALTToolStripMenuItem,
            this.sALTxToolStripMenuItem,
            this.simlibToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // sALTToolStripMenuItem
            // 
            this.sALTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test_SALT_DaemonToolStripMenuItem,
            this.statToolStripMenuItem});
            this.sALTToolStripMenuItem.Name = "sALTToolStripMenuItem";
            this.sALTToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.sALTToolStripMenuItem.Text = "SALT";
            // 
            // test_SALT_DaemonToolStripMenuItem
            // 
            this.test_SALT_DaemonToolStripMenuItem.Name = "test_SALT_DaemonToolStripMenuItem";
            this.test_SALT_DaemonToolStripMenuItem.Size = new System.Drawing.Size(149, 26);
            this.test_SALT_DaemonToolStripMenuItem.Text = "Daemon";
            this.test_SALT_DaemonToolStripMenuItem.Click += new System.EventHandler(this.Test_SALT_DaemonToolStripMenuItem_Click);
            // 
            // statToolStripMenuItem
            // 
            this.statToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test_SALT_Stat_CtToolStripMenuItem,
            this.test_SALT_Stat_DtToolStripMenuItem});
            this.statToolStripMenuItem.Name = "statToolStripMenuItem";
            this.statToolStripMenuItem.Size = new System.Drawing.Size(149, 26);
            this.statToolStripMenuItem.Text = "Stat";
            // 
            // test_SALT_Stat_CtToolStripMenuItem
            // 
            this.test_SALT_Stat_CtToolStripMenuItem.Name = "test_SALT_Stat_CtToolStripMenuItem";
            this.test_SALT_Stat_CtToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.test_SALT_Stat_CtToolStripMenuItem.Text = "Ct";
            this.test_SALT_Stat_CtToolStripMenuItem.Click += new System.EventHandler(this.Test_SALT_Stat_CtToolStripMenuItem_Click);
            // 
            // test_SALT_Stat_DtToolStripMenuItem
            // 
            this.test_SALT_Stat_DtToolStripMenuItem.Name = "test_SALT_Stat_DtToolStripMenuItem";
            this.test_SALT_Stat_DtToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.test_SALT_Stat_DtToolStripMenuItem.Text = "Dt";
            this.test_SALT_Stat_DtToolStripMenuItem.Click += new System.EventHandler(this.Test_SALT_Stat_DtToolStripMenuItem);
            // 
            // sALTxToolStripMenuItem
            // 
            this.sALTxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPUSToolStripMenuItem,
            this.iSYSToolStripMenuItem});
            this.sALTxToolStripMenuItem.Name = "sALTxToolStripMenuItem";
            this.sALTxToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.sALTxToolStripMenuItem.Text = "SALTx";
            // 
            // iSYSToolStripMenuItem
            // 
            this.iSYSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test_SALTx_ISYS_EdgeToolStripMenuItem,
            this.test_SALTx_ISYS_EndptToolStripMenuItem,
            this.test_SALTx_ISYS_FactoryToolStripMenuItem,
            this.test_SALTx_ISYS_GraphToolStripMenuItem,
            this.test_SALTx_ISYS_GraphProductsToolStripMenuItem,
            this.test_SALTx_ISYS_InventoryToolStripMenuItem,
            this.test_SALTx_ISYS_ProductToolStripMenuItem,
            this.statsToolStripMenuItem});
            this.iSYSToolStripMenuItem.Name = "iSYSToolStripMenuItem";
            this.iSYSToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.iSYSToolStripMenuItem.Text = "ISYS";
            // 
            // test_SALTx_ISYS_EdgeToolStripMenuItem
            // 
            this.test_SALTx_ISYS_EdgeToolStripMenuItem.Name = "test_SALTx_ISYS_EdgeToolStripMenuItem";
            this.test_SALTx_ISYS_EdgeToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.test_SALTx_ISYS_EdgeToolStripMenuItem.Text = "Edge";
            this.test_SALTx_ISYS_EdgeToolStripMenuItem.Click += new System.EventHandler(this.Test_SALTx_ISYS_EdgeToolStripMenuItem_Click);
            // 
            // test_SALTx_ISYS_EndptToolStripMenuItem
            // 
            this.test_SALTx_ISYS_EndptToolStripMenuItem.Name = "test_SALTx_ISYS_EndptToolStripMenuItem";
            this.test_SALTx_ISYS_EndptToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.test_SALTx_ISYS_EndptToolStripMenuItem.Text = "Endpt";
            this.test_SALTx_ISYS_EndptToolStripMenuItem.Click += new System.EventHandler(this.Test_SALTx_ISYS_EndptToolStripMenuItem_Click);
            // 
            // test_SALTx_ISYS_FactoryToolStripMenuItem
            // 
            this.test_SALTx_ISYS_FactoryToolStripMenuItem.Name = "test_SALTx_ISYS_FactoryToolStripMenuItem";
            this.test_SALTx_ISYS_FactoryToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.test_SALTx_ISYS_FactoryToolStripMenuItem.Text = "Factory";
            this.test_SALTx_ISYS_FactoryToolStripMenuItem.Click += new System.EventHandler(this.test_SALTx_ISYS_FactoryToolStripMenuItem_Click);
            // 
            // test_SALTx_ISYS_GraphToolStripMenuItem
            // 
            this.test_SALTx_ISYS_GraphToolStripMenuItem.Name = "test_SALTx_ISYS_GraphToolStripMenuItem";
            this.test_SALTx_ISYS_GraphToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.test_SALTx_ISYS_GraphToolStripMenuItem.Text = "Graph";
            this.test_SALTx_ISYS_GraphToolStripMenuItem.Click += new System.EventHandler(this.Test_SALTx_ISYS_GraphToolStripMenuItem_Click);
            // 
            // test_SALTx_ISYS_GraphProductsToolStripMenuItem
            // 
            this.test_SALTx_ISYS_GraphProductsToolStripMenuItem.Name = "test_SALTx_ISYS_GraphProductsToolStripMenuItem";
            this.test_SALTx_ISYS_GraphProductsToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.test_SALTx_ISYS_GraphProductsToolStripMenuItem.Text = "Graph Products";
            this.test_SALTx_ISYS_GraphProductsToolStripMenuItem.Click += new System.EventHandler(this.Test_SALTx_ISYS_GraphProductsToolStripMenuItem_Click);
            // 
            // test_SALTx_ISYS_InventoryToolStripMenuItem
            // 
            this.test_SALTx_ISYS_InventoryToolStripMenuItem.Name = "test_SALTx_ISYS_InventoryToolStripMenuItem";
            this.test_SALTx_ISYS_InventoryToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.test_SALTx_ISYS_InventoryToolStripMenuItem.Text = "Inventory";
            this.test_SALTx_ISYS_InventoryToolStripMenuItem.Click += new System.EventHandler(this.Test_SALTx_ISYS_InventoryToolStripMenuItem_Click);
            // 
            // test_SALTx_ISYS_ProductToolStripMenuItem
            // 
            this.test_SALTx_ISYS_ProductToolStripMenuItem.Name = "test_SALTx_ISYS_ProductToolStripMenuItem";
            this.test_SALTx_ISYS_ProductToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.test_SALTx_ISYS_ProductToolStripMenuItem.Text = "Product";
            this.test_SALTx_ISYS_ProductToolStripMenuItem.Click += new System.EventHandler(this.Test_SALTx_ISYS_ProductToolStripMenuItem_Click);
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.statsToolStripMenuItem.Text = "Stats";
            // 
            // simlibToolStripMenuItem
            // 
            this.simlibToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test_Simlib_ExponentialRNGToolStripMenuItem,
            this.test_Simlib_InventorySystemToolStripMenuItem,
            this.test_Simlib_SingleServerQueueingSystemToolStripMenuItem,
            this.test_Simlib_UniformRNGToolStripMenuItem});
            this.simlibToolStripMenuItem.Name = "simlibToolStripMenuItem";
            this.simlibToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.simlibToolStripMenuItem.Text = "Simlib";
            // 
            // test_Simlib_ExponentialRNGToolStripMenuItem
            // 
            this.test_Simlib_ExponentialRNGToolStripMenuItem.Name = "test_Simlib_ExponentialRNGToolStripMenuItem";
            this.test_Simlib_ExponentialRNGToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.test_Simlib_ExponentialRNGToolStripMenuItem.Text = "Exponential RNG";
            this.test_Simlib_ExponentialRNGToolStripMenuItem.Click += new System.EventHandler(this.Test_Simlib_ExponentialRNGToolStripMenuItem_Click);
            // 
            // test_Simlib_InventorySystemToolStripMenuItem
            // 
            this.test_Simlib_InventorySystemToolStripMenuItem.Name = "test_Simlib_InventorySystemToolStripMenuItem";
            this.test_Simlib_InventorySystemToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.test_Simlib_InventorySystemToolStripMenuItem.Text = "Inventory System";
            this.test_Simlib_InventorySystemToolStripMenuItem.Click += new System.EventHandler(this.Test_Simlib_InventorySystemToolStripMenuItem_Click);
            // 
            // test_Simlib_SingleServerQueueingSystemToolStripMenuItem
            // 
            this.test_Simlib_SingleServerQueueingSystemToolStripMenuItem.Name = "test_Simlib_SingleServerQueueingSystemToolStripMenuItem";
            this.test_Simlib_SingleServerQueueingSystemToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.test_Simlib_SingleServerQueueingSystemToolStripMenuItem.Text = "Single Server Queueing System";
            this.test_Simlib_SingleServerQueueingSystemToolStripMenuItem.Click += new System.EventHandler(this.Test_Simlib_SingleServerQueueingSystemToolStripMenuItem_Click);
            // 
            // test_Simlib_UniformRNGToolStripMenuItem
            // 
            this.test_Simlib_UniformRNGToolStripMenuItem.Name = "test_Simlib_UniformRNGToolStripMenuItem";
            this.test_Simlib_UniformRNGToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.test_Simlib_UniformRNGToolStripMenuItem.Text = "Uniform RNG";
            this.test_Simlib_UniformRNGToolStripMenuItem.Click += new System.EventHandler(this.Test_Simlib_UniformRNGToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainPanel.Location = new System.Drawing.Point(0, 651);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(882, 37);
            this.mainPanel.TabIndex = 1;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 28);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(882, 623);
            this.splitContainer.SplitterDistance = 294;
            this.splitContainer.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(584, 623);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.outputTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 594);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(570, 588);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(576, 594);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cPUSToolStripMenuItem
            // 
            this.cPUSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test_SALTx_CPUS_ServerToolStripMenuItem});
            this.cPUSToolStripMenuItem.Name = "cPUSToolStripMenuItem";
            this.cPUSToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.cPUSToolStripMenuItem.Text = "CPUS";
            // 
            // test_SALTx_CPUS_ServerToolStripMenuItem
            // 
            this.test_SALTx_CPUS_ServerToolStripMenuItem.Name = "test_SALTx_CPUS_ServerToolStripMenuItem";
            this.test_SALTx_CPUS_ServerToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.test_SALTx_CPUS_ServerToolStripMenuItem.Text = "Server";
            this.test_SALTx_CPUS_ServerToolStripMenuItem.Click += new System.EventHandler(this.Test_SALTx_CPUS_ServerToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 688);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem sALTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sALTxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simlibToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_Simlib_ExponentialRNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_Simlib_UniformRNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_Simlib_InventorySystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_Simlib_SingleServerQueueingSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iSYSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_ISYS_GraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_ISYS_EdgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_ISYS_EndptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_ISYS_ProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_ISYS_InventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALT_DaemonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_ISYS_FactoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_ISYS_GraphProductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALT_Stat_CtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALT_Stat_DtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPUSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test_SALTx_CPUS_ServerToolStripMenuItem;
    }
}

