namespace SearchDataSPM.WorkOrder
{
    partial class SPM_ConnectWM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPM_ConnectWM));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getWOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewReleaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReleasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCurrentJobReleaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAllReleaseLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getBOMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Reload = new System.Windows.Forms.Button();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.SPM = new System.Windows.Forms.Label();
            this.versionlabel = new System.Windows.Forms.Label();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cribbttn = new System.Windows.Forms.Button();
            this.scanwobttn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.AccessibleName = "";
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(210, 18);
            this.txtSearch.MaximumSize = new System.Drawing.Size(32767, 25);
            this.txtSearch.MinimumSize = new System.Drawing.Size(4, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(516, 26);
            this.txtSearch.TabIndex = 1;
            this.TreeViewToolTip.SetToolTip(this.txtSearch, "Enter Search Keyword.\r\n(Double click to reset)");
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.Tan;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView.ColumnHeadersHeight = 50;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dataGridView.Location = new System.Drawing.Point(2, 100);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(980, 559);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseMove);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getWOToolStripMenuItem,
            this.releaseManagementToolStripMenuItem,
            this.getBOMToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // getWOToolStripMenuItem
            // 
            this.getWOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("getWOToolStripMenuItem.Image")));
            this.getWOToolStripMenuItem.Name = "getWOToolStripMenuItem";
            this.getWOToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.getWOToolStripMenuItem.Text = "View WorkOrder";
            this.getWOToolStripMenuItem.ToolTipText = "Preview Work Order Details";
            this.getWOToolStripMenuItem.Click += new System.EventHandler(this.GetWOToolStripMenuItem_Click);
            // 
            // releaseManagementToolStripMenuItem
            // 
            this.releaseManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewReleaseToolStripMenuItem,
            this.viewReleasesToolStripMenuItem,
            this.viewCurrentJobReleaseToolStripMenuItem,
            this.viewAllReleaseLogsToolStripMenuItem});
            this.releaseManagementToolStripMenuItem.Enabled = false;
            this.releaseManagementToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("releaseManagementToolStripMenuItem.Image")));
            this.releaseManagementToolStripMenuItem.Name = "releaseManagementToolStripMenuItem";
            this.releaseManagementToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.releaseManagementToolStripMenuItem.Text = "Release Management";
            this.releaseManagementToolStripMenuItem.Visible = false;
            // 
            // addNewReleaseToolStripMenuItem
            // 
            this.addNewReleaseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewReleaseToolStripMenuItem.Image")));
            this.addNewReleaseToolStripMenuItem.Name = "addNewReleaseToolStripMenuItem";
            this.addNewReleaseToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.addNewReleaseToolStripMenuItem.Text = "Add Release";
            this.addNewReleaseToolStripMenuItem.Click += new System.EventHandler(this.AddNewReleaseToolStripMenuItem_Click);
            // 
            // viewReleasesToolStripMenuItem
            // 
            this.viewReleasesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewReleasesToolStripMenuItem.Image")));
            this.viewReleasesToolStripMenuItem.Name = "viewReleasesToolStripMenuItem";
            this.viewReleasesToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.viewReleasesToolStripMenuItem.Text = "View Assy Releases";
            this.viewReleasesToolStripMenuItem.Click += new System.EventHandler(this.ViewReleasesToolStripMenuItem_Click);
            // 
            // viewCurrentJobReleaseToolStripMenuItem
            // 
            this.viewCurrentJobReleaseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewCurrentJobReleaseToolStripMenuItem.Image")));
            this.viewCurrentJobReleaseToolStripMenuItem.Name = "viewCurrentJobReleaseToolStripMenuItem";
            this.viewCurrentJobReleaseToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.viewCurrentJobReleaseToolStripMenuItem.Text = "View Current Job Releases";
            this.viewCurrentJobReleaseToolStripMenuItem.Click += new System.EventHandler(this.ViewCurrentJobReleaseToolStripMenuItem_Click);
            // 
            // viewAllReleaseLogsToolStripMenuItem
            // 
            this.viewAllReleaseLogsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewAllReleaseLogsToolStripMenuItem.Image")));
            this.viewAllReleaseLogsToolStripMenuItem.Name = "viewAllReleaseLogsToolStripMenuItem";
            this.viewAllReleaseLogsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.viewAllReleaseLogsToolStripMenuItem.Text = "View All Release Logs";
            this.viewAllReleaseLogsToolStripMenuItem.Click += new System.EventHandler(this.ViewAllReleaseLogsToolStripMenuItem_Click);
            // 
            // getBOMToolStripMenuItem1
            // 
            this.getBOMToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("getBOMToolStripMenuItem1.Image")));
            this.getBOMToolStripMenuItem1.Name = "getBOMToolStripMenuItem1";
            this.getBOMToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.getBOMToolStripMenuItem1.Text = "Get BOM";
            this.getBOMToolStripMenuItem1.Click += new System.EventHandler(this.GetBOMToolStripMenuItem_Click);
            // 
            // Reload
            // 
            this.Reload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.Location = new System.Drawing.Point(735, 16);
            this.Reload.MaximumSize = new System.Drawing.Size(140, 30);
            this.Reload.MinimumSize = new System.Drawing.Size(140, 30);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(140, 30);
            this.Reload.TabIndex = 7;
            this.Reload.Text = "Refresh / Show All";
            this.TreeViewToolTip.SetToolTip(this.Reload, "Click To Resest \r\nOr \r\nPress Home Buttom\r\n");
            this.Reload.UseVisualStyleBackColor = true;
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            // 
            // Descrip_txtbox
            // 
            this.Descrip_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Descrip_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Descrip_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descrip_txtbox.Location = new System.Drawing.Point(210, 63);
            this.Descrip_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.Descrip_txtbox.MinimumSize = new System.Drawing.Size(180, 26);
            this.Descrip_txtbox.Name = "Descrip_txtbox";
            this.Descrip_txtbox.Size = new System.Drawing.Size(180, 26);
            this.Descrip_txtbox.TabIndex = 2;
            this.TreeViewToolTip.SetToolTip(this.Descrip_txtbox, "Enter Keyword 2");
            this.Descrip_txtbox.Visible = false;
            this.Descrip_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Descrip_txtbox_KeyDown);
            // 
            // filteroem_txtbox
            // 
            this.filteroem_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filteroem_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filteroem_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteroem_txtbox.Location = new System.Drawing.Point(396, 63);
            this.filteroem_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.MinimumSize = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.Name = "filteroem_txtbox";
            this.filteroem_txtbox.Size = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.TabIndex = 3;
            this.TreeViewToolTip.SetToolTip(this.filteroem_txtbox, "Enter Keyword 3");
            this.filteroem_txtbox.Visible = false;
            this.filteroem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filteroem_txtbox_KeyDown);
            // 
            // filteroemitem_txtbox
            // 
            this.filteroemitem_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filteroemitem_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filteroemitem_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteroemitem_txtbox.Location = new System.Drawing.Point(582, 63);
            this.filteroemitem_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.filteroemitem_txtbox.MinimumSize = new System.Drawing.Size(120, 25);
            this.filteroemitem_txtbox.Name = "filteroemitem_txtbox";
            this.filteroemitem_txtbox.Size = new System.Drawing.Size(180, 26);
            this.filteroemitem_txtbox.TabIndex = 4;
            this.TreeViewToolTip.SetToolTip(this.filteroemitem_txtbox, "Enter keyworkd 4");
            this.filteroemitem_txtbox.Visible = false;
            this.filteroemitem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filteroemitem_txtbox_KeyDown);
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(0, 6);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(200, 85);
            this.SPM.TabIndex = 10;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TreeViewToolTip.SetToolTip(this.SPM, "SPM Automation Inc.");
            this.SPM.DoubleClick += new System.EventHandler(this.SPM_DoubleClick);
            // 
            // versionlabel
            // 
            this.versionlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.versionlabel.AutoSize = true;
            this.versionlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionlabel.ForeColor = System.Drawing.Color.Black;
            this.versionlabel.Location = new System.Drawing.Point(955, 2);
            this.versionlabel.MaximumSize = new System.Drawing.Size(26, 8);
            this.versionlabel.MinimumSize = new System.Drawing.Size(26, 8);
            this.versionlabel.Name = "versionlabel";
            this.versionlabel.Size = new System.Drawing.Size(26, 8);
            this.versionlabel.TabIndex = 11;
            this.versionlabel.Text = "V7.6.6";
            this.TreeViewToolTip.SetToolTip(this.versionlabel, "SPM Connect V7.0.0");
            // 
            // filter4
            // 
            this.filter4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filter4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filter4.Location = new System.Drawing.Point(768, 63);
            this.filter4.MaximumSize = new System.Drawing.Size(180, 26);
            this.filter4.MinimumSize = new System.Drawing.Size(120, 25);
            this.filter4.Name = "filter4";
            this.filter4.Size = new System.Drawing.Size(180, 26);
            this.filter4.TabIndex = 5;
            this.TreeViewToolTip.SetToolTip(this.filter4, "Enter Keyword 5");
            this.filter4.Visible = false;
            this.filter4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filter4_KeyDown);
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // cribbttn
            // 
            this.cribbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cribbttn.BackColor = System.Drawing.Color.Transparent;
            this.cribbttn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.cribbttn.FlatAppearance.BorderSize = 0;
            this.cribbttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cribbttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cribbttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cribbttn.ForeColor = System.Drawing.Color.Transparent;
            this.cribbttn.Image = ((System.Drawing.Image)(resources.GetObject("cribbttn.Image")));
            this.cribbttn.Location = new System.Drawing.Point(886, 15);
            this.cribbttn.MaximumSize = new System.Drawing.Size(35, 35);
            this.cribbttn.MinimumSize = new System.Drawing.Size(35, 35);
            this.cribbttn.Name = "cribbttn";
            this.cribbttn.Size = new System.Drawing.Size(35, 35);
            this.cribbttn.TabIndex = 15;
            this.cribbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.cribbttn, "Crib Management");
            this.cribbttn.UseVisualStyleBackColor = false;
            this.cribbttn.Click += new System.EventHandler(this.Cribbttn_Click);
            // 
            // scanwobttn
            // 
            this.scanwobttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scanwobttn.BackColor = System.Drawing.Color.Transparent;
            this.scanwobttn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.scanwobttn.FlatAppearance.BorderSize = 0;
            this.scanwobttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.scanwobttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.scanwobttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scanwobttn.ForeColor = System.Drawing.Color.Transparent;
            this.scanwobttn.Image = ((System.Drawing.Image)(resources.GetObject("scanwobttn.Image")));
            this.scanwobttn.Location = new System.Drawing.Point(937, 15);
            this.scanwobttn.MaximumSize = new System.Drawing.Size(35, 35);
            this.scanwobttn.MinimumSize = new System.Drawing.Size(35, 35);
            this.scanwobttn.Name = "scanwobttn";
            this.scanwobttn.Size = new System.Drawing.Size(35, 35);
            this.scanwobttn.TabIndex = 15;
            this.scanwobttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.scanwobttn, "Scan Work Order");
            this.scanwobttn.UseVisualStyleBackColor = false;
            this.scanwobttn.Click += new System.EventHandler(this.Scanwobttn_Click);
            // 
            // SPM_ConnectWM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.scanwobttn);
            this.Controls.Add(this.cribbttn);
            this.Controls.Add(this.versionlabel);
            this.Controls.Add(this.SPM);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.Reload);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.filter4);
            this.Controls.Add(this.filteroemitem_txtbox);
            this.Controls.Add(this.filteroem_txtbox);
            this.Controls.Add(this.Descrip_txtbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "SPM_ConnectWM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Work Order Management";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_ConnectWM_FormClosed);
            this.Load += new System.EventHandler(this.SPM_Connect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button Reload;
        private System.Windows.Forms.TextBox Descrip_txtbox;
        private System.Windows.Forms.TextBox filteroem_txtbox;
        private System.Windows.Forms.TextBox filteroemitem_txtbox;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Label versionlabel;
        private System.Windows.Forms.TextBox filter4;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem getWOToolStripMenuItem;
        private System.Windows.Forms.Button cribbttn;
        private System.Windows.Forms.Button scanwobttn;
        private System.Windows.Forms.ToolStripMenuItem releaseManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewReleaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewReleasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCurrentJobReleaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAllReleaseLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getBOMToolStripMenuItem1;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

