namespace SearchDataSPM
{
    partial class SPM_ConnectJobs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPM_ConnectJobs));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getBOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectEngineeringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getWorkOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Reload = new System.Windows.Forms.Button();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.SPM = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.purchasereq = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.CreateFolderButton = new System.Windows.Forms.Button();
            this.quotebttn = new System.Windows.Forms.Button();
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
            this.txtSearch.Location = new System.Drawing.Point(210, 10);
            this.txtSearch.MaximumSize = new System.Drawing.Size(32767, 25);
            this.txtSearch.MinimumSize = new System.Drawing.Size(4, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(452, 26);
            this.txtSearch.TabIndex = 1;
            this.TreeViewToolTip.SetToolTip(this.txtSearch, "Enter Search Keyword.\r\n(Double click to reset)");
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
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
            this.dataGridView.BackgroundColor = System.Drawing.Color.Gray;
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
            this.dataGridView.Size = new System.Drawing.Size(983, 559);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseMove);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getBOMToolStripMenuItem,
            this.projectEngineeringToolStripMenuItem,
            this.getWorkOrderToolStripMenuItem,
            this.createFoldersToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 92);
            // 
            // getBOMToolStripMenuItem
            // 
            this.getBOMToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("getBOMToolStripMenuItem.Image")));
            this.getBOMToolStripMenuItem.Name = "getBOMToolStripMenuItem";
            this.getBOMToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.getBOMToolStripMenuItem.Text = "Get BOM";
            this.getBOMToolStripMenuItem.ToolTipText = "Get Selected Job\'s BOM";
            this.getBOMToolStripMenuItem.Click += new System.EventHandler(this.getBOMToolStripMenuItem_Click);
            // 
            // projectEngineeringToolStripMenuItem
            // 
            this.projectEngineeringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remapToolStripMenuItem});
            this.projectEngineeringToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("projectEngineeringToolStripMenuItem.Image")));
            this.projectEngineeringToolStripMenuItem.Name = "projectEngineeringToolStripMenuItem";
            this.projectEngineeringToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.projectEngineeringToolStripMenuItem.Text = "Project Engineering";
            this.projectEngineeringToolStripMenuItem.ToolTipText = "Open Project Eng Folder";
            this.projectEngineeringToolStripMenuItem.Click += new System.EventHandler(this.projectEngineeringToolStripMenuItem_Click);
            // 
            // remapToolStripMenuItem
            // 
            this.remapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("remapToolStripMenuItem.Image")));
            this.remapToolStripMenuItem.Name = "remapToolStripMenuItem";
            this.remapToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.remapToolStripMenuItem.Text = "Remap folder";
            this.remapToolStripMenuItem.ToolTipText = "Assign new folder path";
            this.remapToolStripMenuItem.Click += new System.EventHandler(this.remapToolStripMenuItem_Click);
            // 
            // getWorkOrderToolStripMenuItem
            // 
            this.getWorkOrderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("getWorkOrderToolStripMenuItem.Image")));
            this.getWorkOrderToolStripMenuItem.Name = "getWorkOrderToolStripMenuItem";
            this.getWorkOrderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.getWorkOrderToolStripMenuItem.Text = "Get Work Order";
            this.getWorkOrderToolStripMenuItem.Click += new System.EventHandler(this.getWorkOrderToolStripMenuItem_Click);
            // 
            // createFoldersToolStripMenuItem
            // 
            this.createFoldersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createFoldersToolStripMenuItem.Image")));
            this.createFoldersToolStripMenuItem.Name = "createFoldersToolStripMenuItem";
            this.createFoldersToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.createFoldersToolStripMenuItem.Text = "Create Folders";
            this.createFoldersToolStripMenuItem.ToolTipText = "Create Folders For Selected Job";
            this.createFoldersToolStripMenuItem.Click += new System.EventHandler(this.createFoldersToolStripMenuItem_Click);
            // 
            // Reload
            // 
            this.Reload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.Location = new System.Drawing.Point(668, 5);
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
            this.Descrip_txtbox.Location = new System.Drawing.Point(210, 58);
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
            this.filteroem_txtbox.Location = new System.Drawing.Point(396, 58);
            this.filteroem_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.MinimumSize = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.Name = "filteroem_txtbox";
            this.filteroem_txtbox.Size = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.TabIndex = 3;
            this.TreeViewToolTip.SetToolTip(this.filteroem_txtbox, "Enter Keyword 3");
            this.filteroem_txtbox.Visible = false;
            this.filteroem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filteroem_txtbox_KeyDown);
            // 
            // filteroemitem_txtbox
            // 
            this.filteroemitem_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filteroemitem_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filteroemitem_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteroemitem_txtbox.Location = new System.Drawing.Point(582, 58);
            this.filteroemitem_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.filteroemitem_txtbox.MinimumSize = new System.Drawing.Size(120, 25);
            this.filteroemitem_txtbox.Name = "filteroemitem_txtbox";
            this.filteroemitem_txtbox.Size = new System.Drawing.Size(180, 26);
            this.filteroemitem_txtbox.TabIndex = 4;
            this.TreeViewToolTip.SetToolTip(this.filteroemitem_txtbox, "Enter keyworkd 4");
            this.filteroemitem_txtbox.Visible = false;
            this.filteroemitem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filteroemitem_txtbox_KeyDown);
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(958, 2);
            this.label1.MaximumSize = new System.Drawing.Size(26, 8);
            this.label1.MinimumSize = new System.Drawing.Size(26, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 8);
            this.label1.TabIndex = 11;
            this.label1.Text = "V7.0.0";
            this.TreeViewToolTip.SetToolTip(this.label1, "SPM Connect V7.0.0");
            // 
            // filter4
            // 
            this.filter4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filter4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filter4.Location = new System.Drawing.Point(768, 58);
            this.filter4.MaximumSize = new System.Drawing.Size(180, 26);
            this.filter4.MinimumSize = new System.Drawing.Size(120, 25);
            this.filter4.Name = "filter4";
            this.filter4.Size = new System.Drawing.Size(180, 26);
            this.filter4.TabIndex = 5;
            this.TreeViewToolTip.SetToolTip(this.filter4, "Enter Keyword 5");
            this.filter4.Visible = false;
            this.filter4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filter4_KeyDown);
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // purchasereq
            // 
            this.purchasereq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.purchasereq.BackColor = System.Drawing.Color.Transparent;
            this.purchasereq.Enabled = false;
            this.purchasereq.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.purchasereq.FlatAppearance.BorderSize = 0;
            this.purchasereq.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.purchasereq.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.purchasereq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.purchasereq.ForeColor = System.Drawing.Color.Transparent;
            this.purchasereq.Image = ((System.Drawing.Image)(resources.GetObject("purchasereq.Image")));
            this.purchasereq.Location = new System.Drawing.Point(957, 60);
            this.purchasereq.MaximumSize = new System.Drawing.Size(25, 25);
            this.purchasereq.MinimumSize = new System.Drawing.Size(25, 25);
            this.purchasereq.Name = "purchasereq";
            this.purchasereq.Size = new System.Drawing.Size(25, 25);
            this.purchasereq.TabIndex = 14;
            this.purchasereq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.purchasereq, "Create New Item");
            this.purchasereq.UseVisualStyleBackColor = false;
            this.purchasereq.Visible = false;
            this.purchasereq.Click += new System.EventHandler(this.purchasereq_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.CheckFileExists = false;
            this.openFileDialog1.CheckPathExists = false;
            this.openFileDialog1.FileName = "Select Folder";
            this.openFileDialog1.InitialDirectory = "\\\\spm-adfs\\SPM\\S500 Engineering\\Project_Engineering_Info";
            this.openFileDialog1.Title = "Assign Folder Path";
            this.openFileDialog1.ValidateNames = false;
            // 
            // CreateFolderButton
            // 
            this.CreateFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateFolderButton.Location = new System.Drawing.Point(815, 8);
            this.CreateFolderButton.MaximumSize = new System.Drawing.Size(83, 26);
            this.CreateFolderButton.MinimumSize = new System.Drawing.Size(83, 26);
            this.CreateFolderButton.Name = "CreateFolderButton";
            this.CreateFolderButton.Size = new System.Drawing.Size(83, 26);
            this.CreateFolderButton.TabIndex = 12;
            this.CreateFolderButton.Text = "CreateFolders";
            this.CreateFolderButton.UseVisualStyleBackColor = true;
            this.CreateFolderButton.Click += new System.EventHandler(this.CreateFolderButton_Click);
            // 
            // quotebttn
            // 
            this.quotebttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quotebttn.BackColor = System.Drawing.Color.Transparent;
            this.quotebttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.quotebttn.ForeColor = System.Drawing.Color.Black;
            this.quotebttn.Image = ((System.Drawing.Image)(resources.GetObject("quotebttn.Image")));
            this.quotebttn.Location = new System.Drawing.Point(904, 5);
            this.quotebttn.MaximumSize = new System.Drawing.Size(50, 50);
            this.quotebttn.MinimumSize = new System.Drawing.Size(32, 32);
            this.quotebttn.Name = "quotebttn";
            this.quotebttn.Size = new System.Drawing.Size(32, 32);
            this.quotebttn.TabIndex = 21;
            this.quotebttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.quotebttn, "Create New Quote");
            this.quotebttn.UseVisualStyleBackColor = false;
            this.quotebttn.Visible = false;
            this.quotebttn.Click += new System.EventHandler(this.quotebttn_Click_1);
            // 
            // SPM_ConnectJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(987, 661);
            this.Controls.Add(this.quotebttn);
            this.Controls.Add(this.purchasereq);
            this.Controls.Add(this.CreateFolderButton);
            this.Controls.Add(this.label1);
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
            this.Name = "SPM_ConnectJobs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Jobs";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_ConnectJobs_FormClosed);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filter4;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem getBOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectEngineeringToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem remapToolStripMenuItem;
        private System.Windows.Forms.Button CreateFolderButton;
        private System.Windows.Forms.Button purchasereq;
        private System.Windows.Forms.ToolStripMenuItem getWorkOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFoldersToolStripMenuItem;
        private System.Windows.Forms.Button quotebttn;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

