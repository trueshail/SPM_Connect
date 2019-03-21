namespace SearchDataSPM
{
    partial class InvInOut
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvInOut));
            this.wo_label = new System.Windows.Forms.Label();
            this.emp_label = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.woid_txtbox = new MetroFramework.Controls.MetroTextBox();
            this.empid_txtbox = new MetroFramework.Controls.MetroTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.checkInWorkOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receivePartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialReAllocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllRequestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryBinStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DateTimeLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.versionlabel = new System.Windows.Forms.ToolStripLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.apprvlidtxt = new MetroFramework.Controls.MetroTextBox();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wo_label
            // 
            this.wo_label.BackColor = System.Drawing.Color.Transparent;
            this.wo_label.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wo_label.ForeColor = System.Drawing.Color.Black;
            this.wo_label.Location = new System.Drawing.Point(117, 219);
            this.wo_label.Name = "wo_label";
            this.wo_label.Size = new System.Drawing.Size(140, 33);
            this.wo_label.TabIndex = 1;
            this.wo_label.Text = "Work Order ID :";
            this.wo_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // emp_label
            // 
            this.emp_label.BackColor = System.Drawing.Color.Transparent;
            this.emp_label.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emp_label.ForeColor = System.Drawing.Color.Black;
            this.emp_label.Location = new System.Drawing.Point(136, 154);
            this.emp_label.Name = "emp_label";
            this.emp_label.Size = new System.Drawing.Size(121, 33);
            this.emp_label.TabIndex = 3;
            this.emp_label.Text = "Employee ID :";
            this.emp_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Thistle;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 347);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(652, 110);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.Visible = false;
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(218, 29);
            this.label1.MaximumSize = new System.Drawing.Size(220, 100);
            this.label1.MinimumSize = new System.Drawing.Size(220, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 100);
            this.label1.TabIndex = 17;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // woid_txtbox
            // 
            // 
            // 
            // 
            this.woid_txtbox.CustomButton.Image = null;
            this.woid_txtbox.CustomButton.Location = new System.Drawing.Point(170, 1);
            this.woid_txtbox.CustomButton.Name = "";
            this.woid_txtbox.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.woid_txtbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.woid_txtbox.CustomButton.TabIndex = 1;
            this.woid_txtbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.woid_txtbox.CustomButton.UseSelectable = true;
            this.woid_txtbox.CustomButton.Visible = false;
            this.woid_txtbox.DisplayIcon = true;
            this.woid_txtbox.Enabled = false;
            this.woid_txtbox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.woid_txtbox.Icon = ((System.Drawing.Image)(resources.GetObject("woid_txtbox.Icon")));
            this.woid_txtbox.Lines = new string[0];
            this.woid_txtbox.Location = new System.Drawing.Point(263, 217);
            this.woid_txtbox.MaxLength = 32767;
            this.woid_txtbox.Name = "woid_txtbox";
            this.woid_txtbox.PasswordChar = '\0';
            this.woid_txtbox.PromptText = "Scan Work Order";
            this.woid_txtbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.woid_txtbox.SelectedText = "";
            this.woid_txtbox.SelectionLength = 0;
            this.woid_txtbox.SelectionStart = 0;
            this.woid_txtbox.ShortcutsEnabled = true;
            this.woid_txtbox.ShowClearButton = true;
            this.woid_txtbox.Size = new System.Drawing.Size(204, 35);
            this.woid_txtbox.TabIndex = 2;
            this.woid_txtbox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.woid_txtbox.UseSelectable = true;
            this.woid_txtbox.WaterMark = "Scan Work Order";
            this.woid_txtbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.woid_txtbox.WaterMarkFont = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.woid_txtbox.TextChanged += new System.EventHandler(this.woid_txtbox_TextChanged);
            this.woid_txtbox.Click += new System.EventHandler(this.woid_txtbox_Click);
            this.woid_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.woid_txtbox_KeyDown);
            // 
            // empid_txtbox
            // 
            // 
            // 
            // 
            this.empid_txtbox.CustomButton.Image = null;
            this.empid_txtbox.CustomButton.Location = new System.Drawing.Point(170, 1);
            this.empid_txtbox.CustomButton.Name = "";
            this.empid_txtbox.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.empid_txtbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.empid_txtbox.CustomButton.TabIndex = 1;
            this.empid_txtbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.empid_txtbox.CustomButton.UseSelectable = true;
            this.empid_txtbox.CustomButton.Visible = false;
            this.empid_txtbox.DisplayIcon = true;
            this.empid_txtbox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.empid_txtbox.Icon = ((System.Drawing.Image)(resources.GetObject("empid_txtbox.Icon")));
            this.empid_txtbox.Lines = new string[0];
            this.empid_txtbox.Location = new System.Drawing.Point(263, 152);
            this.empid_txtbox.MaxLength = 32767;
            this.empid_txtbox.Name = "empid_txtbox";
            this.empid_txtbox.PasswordChar = '\0';
            this.empid_txtbox.PromptText = "Scan Emp Id";
            this.empid_txtbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.empid_txtbox.SelectedText = "";
            this.empid_txtbox.SelectionLength = 0;
            this.empid_txtbox.SelectionStart = 0;
            this.empid_txtbox.ShortcutsEnabled = true;
            this.empid_txtbox.ShowClearButton = true;
            this.empid_txtbox.Size = new System.Drawing.Size(204, 35);
            this.empid_txtbox.TabIndex = 0;
            this.empid_txtbox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.empid_txtbox.UseSelectable = true;
            this.empid_txtbox.WaterMark = "Scan Emp Id";
            this.empid_txtbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.empid_txtbox.WaterMarkFont = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.empid_txtbox.TextChanged += new System.EventHandler(this.empid_txtbox_TextChanged);
            this.empid_txtbox.Click += new System.EventHandler(this.empid_txtbox_Click);
            this.empid_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.empid_txtbox_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Thistle;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.DateTimeLbl,
            this.toolStripSeparator2,
            this.versionlabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(652, 25);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "MenuStrip";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DoubleClickEnabled = true;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkInWorkOrderToolStripMenuItem,
            this.receivePartsToolStripMenuItem,
            this.materialReAllocationToolStripMenuItem,
            this.inventoryBinStatusToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(86, 22);
            this.toolStripDropDownButton1.Text = "Inventory";
            this.toolStripDropDownButton1.DropDownOpening += new System.EventHandler(this.toolStripDropDownButton1_DropDownOpening);
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // checkInWorkOrderToolStripMenuItem
            // 
            this.checkInWorkOrderToolStripMenuItem.Enabled = false;
            this.checkInWorkOrderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("checkInWorkOrderToolStripMenuItem.Image")));
            this.checkInWorkOrderToolStripMenuItem.Name = "checkInWorkOrderToolStripMenuItem";
            this.checkInWorkOrderToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.checkInWorkOrderToolStripMenuItem.Text = "Check In Work Order";
            this.checkInWorkOrderToolStripMenuItem.Click += new System.EventHandler(this.checkInWorkOrderToolStripMenuItem_Click);
            // 
            // receivePartsToolStripMenuItem
            // 
            this.receivePartsToolStripMenuItem.Enabled = false;
            this.receivePartsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("receivePartsToolStripMenuItem.Image")));
            this.receivePartsToolStripMenuItem.Name = "receivePartsToolStripMenuItem";
            this.receivePartsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.receivePartsToolStripMenuItem.Text = "Receive Parts";
            // 
            // materialReAllocationToolStripMenuItem
            // 
            this.materialReAllocationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewRequestToolStripMenuItem,
            this.showAllRequestsToolStripMenuItem});
            this.materialReAllocationToolStripMenuItem.Enabled = false;
            this.materialReAllocationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("materialReAllocationToolStripMenuItem.Image")));
            this.materialReAllocationToolStripMenuItem.Name = "materialReAllocationToolStripMenuItem";
            this.materialReAllocationToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.materialReAllocationToolStripMenuItem.Text = "Material Re-Allocation";
            // 
            // createNewRequestToolStripMenuItem
            // 
            this.createNewRequestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createNewRequestToolStripMenuItem.Image")));
            this.createNewRequestToolStripMenuItem.Name = "createNewRequestToolStripMenuItem";
            this.createNewRequestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createNewRequestToolStripMenuItem.Text = "Create New Request";
            this.createNewRequestToolStripMenuItem.Click += new System.EventHandler(this.createNewRequestToolStripMenuItem_Click);
            // 
            // showAllRequestsToolStripMenuItem
            // 
            this.showAllRequestsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showAllRequestsToolStripMenuItem.Image")));
            this.showAllRequestsToolStripMenuItem.Name = "showAllRequestsToolStripMenuItem";
            this.showAllRequestsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showAllRequestsToolStripMenuItem.Text = "Show All Requests";
            this.showAllRequestsToolStripMenuItem.Click += new System.EventHandler(this.showAllRequestsToolStripMenuItem_Click);
            // 
            // inventoryBinStatusToolStripMenuItem
            // 
            this.inventoryBinStatusToolStripMenuItem.Enabled = false;
            this.inventoryBinStatusToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("inventoryBinStatusToolStripMenuItem.Image")));
            this.inventoryBinStatusToolStripMenuItem.Name = "inventoryBinStatusToolStripMenuItem";
            this.inventoryBinStatusToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.inventoryBinStatusToolStripMenuItem.Text = "Inventory Bin Status";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // DateTimeLbl
            // 
            this.DateTimeLbl.Name = "DateTimeLbl";
            this.DateTimeLbl.Size = new System.Drawing.Size(58, 22);
            this.DateTimeLbl.Text = "DateTime";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // versionlabel
            // 
            this.versionlabel.Name = "versionlabel";
            this.versionlabel.Size = new System.Drawing.Size(14, 22);
            this.versionlabel.Text = "V";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(136, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "Approval ID :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // apprvlidtxt
            // 
            // 
            // 
            // 
            this.apprvlidtxt.CustomButton.Image = null;
            this.apprvlidtxt.CustomButton.Location = new System.Drawing.Point(170, 1);
            this.apprvlidtxt.CustomButton.Name = "";
            this.apprvlidtxt.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.apprvlidtxt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.apprvlidtxt.CustomButton.TabIndex = 1;
            this.apprvlidtxt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.apprvlidtxt.CustomButton.UseSelectable = true;
            this.apprvlidtxt.CustomButton.Visible = false;
            this.apprvlidtxt.DisplayIcon = true;
            this.apprvlidtxt.Enabled = false;
            this.apprvlidtxt.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.apprvlidtxt.Icon = ((System.Drawing.Image)(resources.GetObject("apprvlidtxt.Icon")));
            this.apprvlidtxt.Lines = new string[0];
            this.apprvlidtxt.Location = new System.Drawing.Point(263, 280);
            this.apprvlidtxt.MaxLength = 32767;
            this.apprvlidtxt.Name = "apprvlidtxt";
            this.apprvlidtxt.PasswordChar = '\0';
            this.apprvlidtxt.PromptText = "Scan Emp Id";
            this.apprvlidtxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.apprvlidtxt.SelectedText = "";
            this.apprvlidtxt.SelectionLength = 0;
            this.apprvlidtxt.SelectionStart = 0;
            this.apprvlidtxt.ShortcutsEnabled = true;
            this.apprvlidtxt.ShowClearButton = true;
            this.apprvlidtxt.Size = new System.Drawing.Size(204, 35);
            this.apprvlidtxt.TabIndex = 3;
            this.apprvlidtxt.Theme = MetroFramework.MetroThemeStyle.Light;
            this.apprvlidtxt.UseSelectable = true;
            this.apprvlidtxt.WaterMark = "Scan Emp Id";
            this.apprvlidtxt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.apprvlidtxt.WaterMarkFont = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apprvlidtxt.Click += new System.EventHandler(this.apprvlidtxt_Click);
            this.apprvlidtxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.apprvlidtxt_KeyDown);
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // InvInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(652, 457);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.apprvlidtxt);
            this.Controls.Add(this.empid_txtbox);
            this.Controls.Add(this.woid_txtbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.emp_label);
            this.Controls.Add(this.wo_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.Name = "InvInOut";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Crib Management";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label wo_label;
        private System.Windows.Forms.Label emp_label;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox woid_txtbox;
        private MetroFramework.Controls.MetroTextBox empid_txtbox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel DateTimeLbl;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem checkInWorkOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receivePartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialReAllocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewRequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllRequestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryBinStatusToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTextBox apprvlidtxt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel versionlabel;
        private System.Windows.Forms.ToolTip TreeViewToolTip;
    }
}