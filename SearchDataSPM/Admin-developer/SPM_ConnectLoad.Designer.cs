namespace SearchDataSPM
{
	partial class SPM_ConnectLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPM_ConnectLoad));
            this.FormSelector = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whereUsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoCadCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geniusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoCadCatalogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.geniusJobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.sPM_DatabaseDataSet = new SearchDataSPM.SPM_DatabaseDataSet();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SPM = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inventoryTableAdapter = new SearchDataSPM.SPM_DatabaseDataSetTableAdapters.InventoryTableAdapter();
            this.tableAdapterManager = new SearchDataSPM.SPM_DatabaseDataSetTableAdapters.TableAdapterManager();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.admin_bttn = new System.Windows.Forms.Button();
            this.Userlabel = new System.Windows.Forms.Label();
            this.Loginid = new System.Windows.Forms.Label();
            this.FormSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // FormSelector
            // 
            this.FormSelector.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToCatalogToolStripMenuItem,
            this.bOMToolStripMenuItem,
            this.whereUsedToolStripMenuItem});
            this.FormSelector.Name = "contextMenuStrip1";
            this.FormSelector.Size = new System.Drawing.Size(68, 70);
            // 
            // addToCatalogToolStripMenuItem
            // 
            this.addToCatalogToolStripMenuItem.Name = "addToCatalogToolStripMenuItem";
            this.addToCatalogToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // bOMToolStripMenuItem
            // 
            this.bOMToolStripMenuItem.Name = "bOMToolStripMenuItem";
            this.bOMToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // whereUsedToolStripMenuItem
            // 
            this.whereUsedToolStripMenuItem.Name = "whereUsedToolStripMenuItem";
            this.whereUsedToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // autoCadCatalogToolStripMenuItem
            // 
            this.autoCadCatalogToolStripMenuItem.Name = "autoCadCatalogToolStripMenuItem";
            this.autoCadCatalogToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // geniusToolStripMenuItem
            // 
            this.geniusToolStripMenuItem.Name = "geniusToolStripMenuItem";
            this.geniusToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // autoCadCatalogToolStripMenuItem1
            // 
            this.autoCadCatalogToolStripMenuItem1.Name = "autoCadCatalogToolStripMenuItem1";
            this.autoCadCatalogToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // geniusJobsToolStripMenuItem
            // 
            this.geniusJobsToolStripMenuItem.Name = "geniusJobsToolStripMenuItem";
            this.geniusJobsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // inventoryBindingSource3
            // 
            this.inventoryBindingSource3.DataMember = "Inventory";
            this.inventoryBindingSource3.DataSource = this.sPM_DatabaseDataSet;
            // 
            // sPM_DatabaseDataSet
            // 
            this.sPM_DatabaseDataSet.DataSetName = "SPM_DatabaseDataSet";
            this.sPM_DatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(23, 38);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(332, 160);
            this.SPM.TabIndex = 10;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TreeViewToolTip.SetToolTip(this.SPM, "SPM Automation Inc.");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(342, 9);
            this.label1.MaximumSize = new System.Drawing.Size(26, 8);
            this.label1.MinimumSize = new System.Drawing.Size(26, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 8);
            this.label1.TabIndex = 11;
            this.label1.Text = "V1.1.2";
            this.TreeViewToolTip.SetToolTip(this.label1, "SPM Connect V1.1.2");
            // 
            // inventoryTableAdapter
            // 
            this.inventoryTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.InventoryTableAdapter = this.inventoryTableAdapter;
            this.tableAdapterManager.UpdateOrder = SearchDataSPM.SPM_DatabaseDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // admin_bttn
            // 
            this.admin_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.admin_bttn.BackColor = System.Drawing.Color.Transparent;
            this.admin_bttn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.admin_bttn.FlatAppearance.BorderSize = 0;
            this.admin_bttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.admin_bttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.admin_bttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.admin_bttn.ForeColor = System.Drawing.Color.Transparent;
            this.admin_bttn.Image = ((System.Drawing.Image)(resources.GetObject("admin_bttn.Image")));
            this.admin_bttn.Location = new System.Drawing.Point(328, 161);
            this.admin_bttn.MaximumSize = new System.Drawing.Size(40, 40);
            this.admin_bttn.MinimumSize = new System.Drawing.Size(25, 25);
            this.admin_bttn.Name = "admin_bttn";
            this.admin_bttn.Size = new System.Drawing.Size(40, 37);
            this.admin_bttn.TabIndex = 17;
            this.admin_bttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.admin_bttn, "Adminstrative Control");
            this.admin_bttn.UseVisualStyleBackColor = false;
            this.admin_bttn.Click += new System.EventHandler(this.admin_bttn_Click);
            // 
            // Userlabel
            // 
            this.Userlabel.AutoSize = true;
            this.Userlabel.Location = new System.Drawing.Point(55, 10);
            this.Userlabel.Name = "Userlabel";
            this.Userlabel.Size = new System.Drawing.Size(73, 13);
            this.Userlabel.TabIndex = 12;
            this.Userlabel.Text = "SPM Connect";
            // 
            // Loginid
            // 
            this.Loginid.AutoSize = true;
            this.Loginid.Location = new System.Drawing.Point(10, 10);
            this.Loginid.Name = "Loginid";
            this.Loginid.Size = new System.Drawing.Size(47, 13);
            this.Loginid.TabIndex = 13;
            this.Loginid.Text = "User Id :";
            // 
            // SPM_ConnectLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(380, 207);
            this.Controls.Add(this.admin_bttn);
            this.Controls.Add(this.Loginid);
            this.Controls.Add(this.Userlabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SPM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "SPM_ConnectLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " SPM Connect";
            this.Load += new System.EventHandler(this.SPM_Connect_Load);
            this.FormSelector.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label SPM;
		private SPM_DatabaseDataSet sPM_DatabaseDataSet;
		private System.Windows.Forms.BindingSource inventoryBindingSource3;
		private SPM_DatabaseDataSetTableAdapters.InventoryTableAdapter inventoryTableAdapter;
		private SPM_DatabaseDataSetTableAdapters.TableAdapterManager tableAdapterManager;
		private System.Windows.Forms.Label label1;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.ContextMenuStrip FormSelector;
        private System.Windows.Forms.ToolStripMenuItem bOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whereUsedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToCatalogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoCadCatalogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geniusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoCadCatalogToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem geniusJobsToolStripMenuItem;
        public System.Windows.Forms.Label Userlabel;
        private System.Windows.Forms.Label Loginid;
        private System.Windows.Forms.Button admin_bttn;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

