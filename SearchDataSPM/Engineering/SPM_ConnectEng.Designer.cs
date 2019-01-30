using System;

namespace SearchDataSPM
{
	partial class SPM_Connect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPM_Connect));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FormSelector = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whereUsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDrawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReportBOM = new System.Windows.Forms.ToolStripMenuItem();
            this.billsOfMaunfacturingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sparePartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eModelViewerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.sPM_DatabaseDataSet = new SearchDataSPM.SPM_DatabaseDataSet();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SPM = new System.Windows.Forms.Label();
            this.AddNewBttn = new System.Windows.Forms.Button();
            this.jobsbttn = new System.Windows.Forms.Button();
            this.admin_bttn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Reload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.advsearchbttn = new System.Windows.Forms.Button();
            this.Manufactureritemcomboxbox = new System.Windows.Forms.ComboBox();
            this.familycomboxbox = new System.Windows.Forms.ComboBox();
            this.lastsavedbycombo = new System.Windows.Forms.ComboBox();
            this.oemitemcombobox = new System.Windows.Forms.ComboBox();
            this.ActiveCadblockcombobox = new System.Windows.Forms.ComboBox();
            this.designedbycombobox = new System.Windows.Forms.ComboBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.Listviewcontextmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bomlistviewmenustrpc = new System.Windows.Forms.ToolStripMenuItem();
            this.whereusedlistviewStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.iteminfolistviewStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.recordlabel = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.itemNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.familyCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manufacturerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manufacturerItemNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clrfiltersbttn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.actcadblk = new System.Windows.Forms.Label();
            this.FormSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet)).BeginInit();
            this.Listviewcontextmenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FormSelector
            // 
            this.FormSelector.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bOMToolStripMenuItem,
            this.whereUsedToolStripMenuItem,
            this.openModelToolStripMenuItem,
            this.openDrawingToolStripMenuItem,
            this.editItemToolStripMenuItem,
            this.copySelectedItemToolStripMenuItem,
            this.viewReportBOM,
            this.deleteItemToolStripMenuItem,
            this.eModelViewerToolStripMenuItem1});
            this.FormSelector.Name = "contextMenuStrip1";
            this.FormSelector.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.FormSelector.Size = new System.Drawing.Size(177, 202);
            // 
            // bOMToolStripMenuItem
            // 
            this.bOMToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bOMToolStripMenuItem.Image")));
            this.bOMToolStripMenuItem.Name = "bOMToolStripMenuItem";
            this.bOMToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.bOMToolStripMenuItem.Text = "BOM";
            this.bOMToolStripMenuItem.ToolTipText = "Bills Of Material";
            this.bOMToolStripMenuItem.Click += new System.EventHandler(this.bOMToolStripMenuItem_Click);
            // 
            // whereUsedToolStripMenuItem
            // 
            this.whereUsedToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("whereUsedToolStripMenuItem.Image")));
            this.whereUsedToolStripMenuItem.Name = "whereUsedToolStripMenuItem";
            this.whereUsedToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.whereUsedToolStripMenuItem.Text = "Where Used";
            this.whereUsedToolStripMenuItem.ToolTipText = "Check Where Used";
            this.whereUsedToolStripMenuItem.Click += new System.EventHandler(this.whereUsedToolStripMenuItem_Click);
            // 
            // openModelToolStripMenuItem
            // 
            this.openModelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openModelToolStripMenuItem.Image")));
            this.openModelToolStripMenuItem.Name = "openModelToolStripMenuItem";
            this.openModelToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.openModelToolStripMenuItem.Text = "Open Model";
            this.openModelToolStripMenuItem.ToolTipText = "Open Selected Item\'s Model";
            this.openModelToolStripMenuItem.Click += new System.EventHandler(this.openModelToolStripMenuItem_Click);
            // 
            // openDrawingToolStripMenuItem
            // 
            this.openDrawingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openDrawingToolStripMenuItem.Image")));
            this.openDrawingToolStripMenuItem.Name = "openDrawingToolStripMenuItem";
            this.openDrawingToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.openDrawingToolStripMenuItem.Text = "Open Drawing";
            this.openDrawingToolStripMenuItem.ToolTipText = "Open Selected Item\'s Drawing";
            this.openDrawingToolStripMenuItem.Click += new System.EventHandler(this.openDrawingToolStripMenuItem_Click);
            // 
            // editItemToolStripMenuItem
            // 
            this.editItemToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editItemToolStripMenuItem.Image")));
            this.editItemToolStripMenuItem.Name = "editItemToolStripMenuItem";
            this.editItemToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.editItemToolStripMenuItem.Text = "Edit Item";
            this.editItemToolStripMenuItem.ToolTipText = "Edit Selected Item\'s Properties";
            this.editItemToolStripMenuItem.Click += new System.EventHandler(this.editItemToolStripMenuItem_Click);
            // 
            // copySelectedItemToolStripMenuItem
            // 
            this.copySelectedItemToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copySelectedItemToolStripMenuItem.Image")));
            this.copySelectedItemToolStripMenuItem.Name = "copySelectedItemToolStripMenuItem";
            this.copySelectedItemToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.copySelectedItemToolStripMenuItem.Text = "Copy Selected Item";
            this.copySelectedItemToolStripMenuItem.ToolTipText = "Copy selected item to new item number";
            this.copySelectedItemToolStripMenuItem.Click += new System.EventHandler(this.copySelectedItemToolStripMenuItem_Click);
            // 
            // viewReportBOM
            // 
            this.viewReportBOM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.billsOfMaunfacturingToolStripMenuItem,
            this.sparePartsToolStripMenuItem});
            this.viewReportBOM.Image = ((System.Drawing.Image)(resources.GetObject("viewReportBOM.Image")));
            this.viewReportBOM.Name = "viewReportBOM";
            this.viewReportBOM.Size = new System.Drawing.Size(176, 22);
            this.viewReportBOM.Text = "View Report (BOM)";
            this.viewReportBOM.ToolTipText = "View BOM Report";
            // 
            // billsOfMaunfacturingToolStripMenuItem
            // 
            this.billsOfMaunfacturingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("billsOfMaunfacturingToolStripMenuItem.Image")));
            this.billsOfMaunfacturingToolStripMenuItem.Name = "billsOfMaunfacturingToolStripMenuItem";
            this.billsOfMaunfacturingToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.billsOfMaunfacturingToolStripMenuItem.Text = "Bills of Manufacturing";
            this.billsOfMaunfacturingToolStripMenuItem.ToolTipText = "Preview Bills Of Manufacturing Report";
            this.billsOfMaunfacturingToolStripMenuItem.Click += new System.EventHandler(this.billsOfMaunfacturingToolStripMenuItem_Click);
            // 
            // sparePartsToolStripMenuItem
            // 
            this.sparePartsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sparePartsToolStripMenuItem.Image")));
            this.sparePartsToolStripMenuItem.Name = "sparePartsToolStripMenuItem";
            this.sparePartsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.sparePartsToolStripMenuItem.Text = "Spare Parts";
            this.sparePartsToolStripMenuItem.ToolTipText = "Preview Spare Parts Report";
            this.sparePartsToolStripMenuItem.Click += new System.EventHandler(this.sparePartsToolStripMenuItem_Click);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Enabled = false;
            this.deleteItemToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteItemToolStripMenuItem.Image")));
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.deleteItemToolStripMenuItem.Text = "Delete Item";
            this.deleteItemToolStripMenuItem.ToolTipText = "Delete Selected Item From Connect";
            this.deleteItemToolStripMenuItem.Visible = false;
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.deleteItemToolStripMenuItem_Click);
            // 
            // eModelViewerToolStripMenuItem1
            // 
            this.eModelViewerToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("eModelViewerToolStripMenuItem1.Image")));
            this.eModelViewerToolStripMenuItem1.Name = "eModelViewerToolStripMenuItem1";
            this.eModelViewerToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.eModelViewerToolStripMenuItem1.Text = "E Model Viewer";
            this.eModelViewerToolStripMenuItem1.ToolTipText = "Show E Model";
            this.eModelViewerToolStripMenuItem1.Click += new System.EventHandler(this.eModelViewerToolStripMenuItem1_Click);
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
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(1, 7);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(198, 85);
            this.SPM.TabIndex = 114;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TreeViewToolTip.SetToolTip(this.SPM, "SPM Automation Inc.");
            this.SPM.DoubleClick += new System.EventHandler(this.SPM_DoubleClick);
            // 
            // AddNewBttn
            // 
            this.AddNewBttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewBttn.BackColor = System.Drawing.Color.Transparent;
            this.AddNewBttn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.AddNewBttn.FlatAppearance.BorderSize = 0;
            this.AddNewBttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AddNewBttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AddNewBttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddNewBttn.ForeColor = System.Drawing.Color.Transparent;
            this.AddNewBttn.Image = ((System.Drawing.Image)(resources.GetObject("AddNewBttn.Image")));
            this.AddNewBttn.Location = new System.Drawing.Point(815, 19);
            this.AddNewBttn.MaximumSize = new System.Drawing.Size(25, 25);
            this.AddNewBttn.MinimumSize = new System.Drawing.Size(25, 25);
            this.AddNewBttn.Name = "AddNewBttn";
            this.AddNewBttn.Size = new System.Drawing.Size(25, 25);
            this.AddNewBttn.TabIndex = 117;
            this.AddNewBttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.AddNewBttn, "Create New Item");
            this.AddNewBttn.UseVisualStyleBackColor = false;
            this.AddNewBttn.Click += new System.EventHandler(this.AddNewBttn_Click);
            // 
            // jobsbttn
            // 
            this.jobsbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jobsbttn.BackColor = System.Drawing.Color.Transparent;
            this.jobsbttn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.jobsbttn.FlatAppearance.BorderSize = 0;
            this.jobsbttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.jobsbttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.jobsbttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jobsbttn.ForeColor = System.Drawing.Color.Transparent;
            this.jobsbttn.Image = ((System.Drawing.Image)(resources.GetObject("jobsbttn.Image")));
            this.jobsbttn.Location = new System.Drawing.Point(845, 19);
            this.jobsbttn.MaximumSize = new System.Drawing.Size(25, 25);
            this.jobsbttn.MinimumSize = new System.Drawing.Size(25, 25);
            this.jobsbttn.Name = "jobsbttn";
            this.jobsbttn.Size = new System.Drawing.Size(25, 25);
            this.jobsbttn.TabIndex = 115;
            this.jobsbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.jobsbttn, "SPM Job Numbers\r\nCTRL + J");
            this.jobsbttn.UseVisualStyleBackColor = false;
            this.jobsbttn.Click += new System.EventHandler(this.jobsbttn_Click);
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
            this.admin_bttn.Location = new System.Drawing.Point(872, 14);
            this.admin_bttn.MaximumSize = new System.Drawing.Size(35, 35);
            this.admin_bttn.MinimumSize = new System.Drawing.Size(35, 35);
            this.admin_bttn.Name = "admin_bttn";
            this.admin_bttn.Size = new System.Drawing.Size(35, 35);
            this.admin_bttn.TabIndex = 118;
            this.admin_bttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.admin_bttn, "Adminstrative Control");
            this.admin_bttn.UseVisualStyleBackColor = false;
            this.admin_bttn.Click += new System.EventHandler(this.admin_bttn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(879, 3);
            this.label1.MaximumSize = new System.Drawing.Size(26, 8);
            this.label1.MinimumSize = new System.Drawing.Size(26, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 8);
            this.label1.TabIndex = 116;
            this.label1.Text = "V7.6.0";
            this.TreeViewToolTip.SetToolTip(this.label1, "SPM Connect V7.6.0");
            // 
            // Reload
            // 
            this.Reload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.Location = new System.Drawing.Point(670, 17);
            this.Reload.MaximumSize = new System.Drawing.Size(140, 30);
            this.Reload.MinimumSize = new System.Drawing.Size(140, 30);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(140, 30);
            this.Reload.TabIndex = 113;
            this.Reload.Text = "Refresh / Show All";
            this.TreeViewToolTip.SetToolTip(this.Reload, "Click To Reset\r\nOr\r\nPress Home Button");
            this.Reload.UseVisualStyleBackColor = true;
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.AccessibleName = "";
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(209, 18);
            this.txtSearch.MaximumSize = new System.Drawing.Size(32767, 25);
            this.txtSearch.MinimumSize = new System.Drawing.Size(4, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(455, 26);
            this.txtSearch.TabIndex = 106;
            this.TreeViewToolTip.SetToolTip(this.txtSearch, "Enter Search Keyword.\r\n");
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // filter4
            // 
            this.filter4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filter4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filter4.Location = new System.Drawing.Point(705, 55);
            this.filter4.MaximumSize = new System.Drawing.Size(160, 26);
            this.filter4.MinimumSize = new System.Drawing.Size(160, 25);
            this.filter4.Name = "filter4";
            this.filter4.Size = new System.Drawing.Size(160, 26);
            this.filter4.TabIndex = 110;
            this.TreeViewToolTip.SetToolTip(this.filter4, "Enter Keyword 5");
            this.filter4.Visible = false;
            this.filter4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filter4_KeyDown);
            // 
            // filteroemitem_txtbox
            // 
            this.filteroemitem_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filteroemitem_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filteroemitem_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteroemitem_txtbox.Location = new System.Drawing.Point(540, 55);
            this.filteroemitem_txtbox.MaximumSize = new System.Drawing.Size(160, 26);
            this.filteroemitem_txtbox.MinimumSize = new System.Drawing.Size(160, 25);
            this.filteroemitem_txtbox.Name = "filteroemitem_txtbox";
            this.filteroemitem_txtbox.Size = new System.Drawing.Size(160, 26);
            this.filteroemitem_txtbox.TabIndex = 109;
            this.TreeViewToolTip.SetToolTip(this.filteroemitem_txtbox, "Enter keyworkd 4");
            this.filteroemitem_txtbox.Visible = false;
            this.filteroemitem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filteroemitem_txtbox_KeyDown);
            // 
            // filteroem_txtbox
            // 
            this.filteroem_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filteroem_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filteroem_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteroem_txtbox.Location = new System.Drawing.Point(375, 55);
            this.filteroem_txtbox.MaximumSize = new System.Drawing.Size(160, 26);
            this.filteroem_txtbox.MinimumSize = new System.Drawing.Size(160, 26);
            this.filteroem_txtbox.Name = "filteroem_txtbox";
            this.filteroem_txtbox.Size = new System.Drawing.Size(160, 26);
            this.filteroem_txtbox.TabIndex = 108;
            this.TreeViewToolTip.SetToolTip(this.filteroem_txtbox, "Enter Keyword 3");
            this.filteroem_txtbox.Visible = false;
            this.filteroem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filteroem_txtbox_KeyDown);
            // 
            // Descrip_txtbox
            // 
            this.Descrip_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Descrip_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Descrip_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descrip_txtbox.Location = new System.Drawing.Point(209, 55);
            this.Descrip_txtbox.MaximumSize = new System.Drawing.Size(160, 26);
            this.Descrip_txtbox.MinimumSize = new System.Drawing.Size(160, 26);
            this.Descrip_txtbox.Name = "Descrip_txtbox";
            this.Descrip_txtbox.Size = new System.Drawing.Size(160, 26);
            this.Descrip_txtbox.TabIndex = 107;
            this.TreeViewToolTip.SetToolTip(this.Descrip_txtbox, "Enter Keyword 2");
            this.Descrip_txtbox.Visible = false;
            this.Descrip_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Descrip_txtbox_KeyDown);
            // 
            // advsearchbttn
            // 
            this.advsearchbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advsearchbttn.Location = new System.Drawing.Point(869, 61);
            this.advsearchbttn.MaximumSize = new System.Drawing.Size(35, 25);
            this.advsearchbttn.MinimumSize = new System.Drawing.Size(35, 25);
            this.advsearchbttn.Name = "advsearchbttn";
            this.advsearchbttn.Size = new System.Drawing.Size(35, 25);
            this.advsearchbttn.TabIndex = 121;
            this.advsearchbttn.Text = ">>";
            this.TreeViewToolTip.SetToolTip(this.advsearchbttn, "Show Advance Filters");
            this.advsearchbttn.UseVisualStyleBackColor = true;
            this.advsearchbttn.Click += new System.EventHandler(this.advsearchbttn_Click);
            // 
            // Manufactureritemcomboxbox
            // 
            this.Manufactureritemcomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Manufactureritemcomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Manufactureritemcomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Manufactureritemcomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Manufactureritemcomboxbox.FormattingEnabled = true;
            this.Manufactureritemcomboxbox.Location = new System.Drawing.Point(3, 376);
            this.Manufactureritemcomboxbox.Name = "Manufactureritemcomboxbox";
            this.Manufactureritemcomboxbox.Size = new System.Drawing.Size(203, 21);
            this.Manufactureritemcomboxbox.TabIndex = 125;
            this.TreeViewToolTip.SetToolTip(this.Manufactureritemcomboxbox, "Filter by OEM item number");
            this.Manufactureritemcomboxbox.TextChanged += new System.EventHandler(this.Manufactureritemcomboxbox_TextChanged);
            this.Manufactureritemcomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Manufactureritemcomboxbox_KeyDown);
            // 
            // familycomboxbox
            // 
            this.familycomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.familycomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.familycomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.familycomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.familycomboxbox.FormattingEnabled = true;
            this.familycomboxbox.Location = new System.Drawing.Point(3, 227);
            this.familycomboxbox.Name = "familycomboxbox";
            this.familycomboxbox.Size = new System.Drawing.Size(203, 21);
            this.familycomboxbox.TabIndex = 123;
            this.TreeViewToolTip.SetToolTip(this.familycomboxbox, "Filter by family type");
            this.familycomboxbox.TextChanged += new System.EventHandler(this.familycomboxbox_TextChanged);
            this.familycomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.familycomboxbox_KeyDown);
            // 
            // lastsavedbycombo
            // 
            this.lastsavedbycombo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastsavedbycombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.lastsavedbycombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.lastsavedbycombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lastsavedbycombo.FormattingEnabled = true;
            this.lastsavedbycombo.Location = new System.Drawing.Point(3, 453);
            this.lastsavedbycombo.Name = "lastsavedbycombo";
            this.lastsavedbycombo.Size = new System.Drawing.Size(203, 21);
            this.lastsavedbycombo.TabIndex = 126;
            this.TreeViewToolTip.SetToolTip(this.lastsavedbycombo, "Filter by last saved");
            this.lastsavedbycombo.TextChanged += new System.EventHandler(this.lastsavedbycombo_TextChanged);
            this.lastsavedbycombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lastsavedbycombo_KeyDown);
            // 
            // oemitemcombobox
            // 
            this.oemitemcombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemitemcombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.oemitemcombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.oemitemcombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.oemitemcombobox.FormattingEnabled = true;
            this.oemitemcombobox.Items.AddRange(new object[] {
            "Festo",
            "SPM AUTOMATION (Canada) INC."});
            this.oemitemcombobox.Location = new System.Drawing.Point(3, 301);
            this.oemitemcombobox.Name = "oemitemcombobox";
            this.oemitemcombobox.Size = new System.Drawing.Size(203, 21);
            this.oemitemcombobox.TabIndex = 124;
            this.TreeViewToolTip.SetToolTip(this.oemitemcombobox, "Filter by manufacture");
            this.oemitemcombobox.TextChanged += new System.EventHandler(this.oemitemcombobox_TextChanged);
            this.oemitemcombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oemitemcombobox_KeyDown);
            // 
            // ActiveCadblockcombobox
            // 
            this.ActiveCadblockcombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActiveCadblockcombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ActiveCadblockcombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ActiveCadblockcombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ActiveCadblockcombobox.FormattingEnabled = true;
            this.ActiveCadblockcombobox.Location = new System.Drawing.Point(3, 534);
            this.ActiveCadblockcombobox.Name = "ActiveCadblockcombobox";
            this.ActiveCadblockcombobox.Size = new System.Drawing.Size(203, 21);
            this.ActiveCadblockcombobox.TabIndex = 127;
            this.TreeViewToolTip.SetToolTip(this.ActiveCadblockcombobox, "Filter by current cad block number");
            this.ActiveCadblockcombobox.TextChanged += new System.EventHandler(this.ActiveCadblockcombobox_TextChanged);
            this.ActiveCadblockcombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActiveCadblockcombobox_KeyDown);
            // 
            // designedbycombobox
            // 
            this.designedbycombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designedbycombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.designedbycombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.designedbycombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.designedbycombobox.FormattingEnabled = true;
            this.designedbycombobox.Items.AddRange(new object[] {
            "Shailkumar Patel",
            "Scott Reid",
            "Joel Goldsmith"});
            this.designedbycombobox.Location = new System.Drawing.Point(3, 150);
            this.designedbycombobox.Name = "designedbycombobox";
            this.designedbycombobox.Size = new System.Drawing.Size(203, 21);
            this.designedbycombobox.TabIndex = 122;
            this.TreeViewToolTip.SetToolTip(this.designedbycombobox, "Filter Designed by");
            this.designedbycombobox.TextChanged += new System.EventHandler(this.designedbycombobox_TextChanged);
            this.designedbycombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.designedbycombobox_KeyDown);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(256, 192);
            this.imageList.TransparentColor = System.Drawing.Color.Black;
            // 
            // Listviewcontextmenu
            // 
            this.Listviewcontextmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bomlistviewmenustrpc,
            this.whereusedlistviewStripMenu,
            this.iteminfolistviewStripMenu,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.Listviewcontextmenu.Name = "contextMenuStrip1";
            this.Listviewcontextmenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.Listviewcontextmenu.Size = new System.Drawing.Size(177, 114);
            this.Listviewcontextmenu.Opening += new System.ComponentModel.CancelEventHandler(this.Listviewcontextmenu_Opening);
            // 
            // bomlistviewmenustrpc
            // 
            this.bomlistviewmenustrpc.Image = ((System.Drawing.Image)(resources.GetObject("bomlistviewmenustrpc.Image")));
            this.bomlistviewmenustrpc.Name = "bomlistviewmenustrpc";
            this.bomlistviewmenustrpc.Size = new System.Drawing.Size(176, 22);
            this.bomlistviewmenustrpc.Text = "BOM";
            this.bomlistviewmenustrpc.ToolTipText = "Bills Of Material";
            this.bomlistviewmenustrpc.Click += new System.EventHandler(this.bomlistviewmenustrpc_Click);
            // 
            // whereusedlistviewStripMenu
            // 
            this.whereusedlistviewStripMenu.Image = ((System.Drawing.Image)(resources.GetObject("whereusedlistviewStripMenu.Image")));
            this.whereusedlistviewStripMenu.Name = "whereusedlistviewStripMenu";
            this.whereusedlistviewStripMenu.Size = new System.Drawing.Size(176, 22);
            this.whereusedlistviewStripMenu.Text = "Where Used";
            this.whereusedlistviewStripMenu.ToolTipText = "Check Where Used";
            this.whereusedlistviewStripMenu.Click += new System.EventHandler(this.whereusedlistviewStripMenu_Click);
            // 
            // iteminfolistviewStripMenu
            // 
            this.iteminfolistviewStripMenu.Image = ((System.Drawing.Image)(resources.GetObject("iteminfolistviewStripMenu.Image")));
            this.iteminfolistviewStripMenu.Name = "iteminfolistviewStripMenu";
            this.iteminfolistviewStripMenu.Size = new System.Drawing.Size(176, 22);
            this.iteminfolistviewStripMenu.Text = "Get Item Info";
            this.iteminfolistviewStripMenu.ToolTipText = "Get Selected Item\'s Info.";
            this.iteminfolistviewStripMenu.Click += new System.EventHandler(this.iteminfolistviewStripMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem1.Text = "Edit Item";
            this.toolStripMenuItem1.ToolTipText = "Edit Selected Item\'s Properties";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem2.Text = "Copy Selected Item";
            this.toolStripMenuItem2.ToolTipText = "Copy selected item to new item number";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(700, 600);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.advsearchbttn);
            this.splitContainer1.Panel1.Controls.Add(this.SPM);
            this.splitContainer1.Panel1.Controls.Add(this.recordlabel);
            this.splitContainer1.Panel1.Controls.Add(this.AddNewBttn);
            this.splitContainer1.Panel1.Controls.Add(this.jobsbttn);
            this.splitContainer1.Panel1.Controls.Add(this.listView);
            this.splitContainer1.Panel1.Controls.Add(this.admin_bttn);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.Reload);
            this.splitContainer1.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer1.Panel1.Controls.Add(this.filter4);
            this.splitContainer1.Panel1.Controls.Add(this.filteroemitem_txtbox);
            this.splitContainer1.Panel1.Controls.Add(this.filteroem_txtbox);
            this.splitContainer1.Panel1.Controls.Add(this.Descrip_txtbox);
            this.splitContainer1.Panel1MinSize = 500;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Gray;
            this.splitContainer1.Panel2.Controls.Add(this.clrfiltersbttn);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.actcadblk);
            this.splitContainer1.Panel2.Controls.Add(this.Manufactureritemcomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.familycomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.lastsavedbycombo);
            this.splitContainer1.Panel2.Controls.Add(this.oemitemcombobox);
            this.splitContainer1.Panel2.Controls.Add(this.ActiveCadblockcombobox);
            this.splitContainer1.Panel2.Controls.Add(this.designedbycombobox);
            this.splitContainer1.Panel2MinSize = 180;
            this.splitContainer1.Size = new System.Drawing.Size(1119, 761);
            this.splitContainer1.SplitterDistance = 912;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 105;
            // 
            // recordlabel
            // 
            this.recordlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recordlabel.AutoSize = true;
            this.recordlabel.Font = new System.Drawing.Font("Maiandra GD", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordlabel.ForeColor = System.Drawing.Color.White;
            this.recordlabel.Location = new System.Drawing.Point(702, 80);
            this.recordlabel.Name = "recordlabel";
            this.recordlabel.Size = new System.Drawing.Size(0, 14);
            this.recordlabel.TabIndex = 119;
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.BackColor = System.Drawing.Color.White;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.ContextMenuStrip = this.Listviewcontextmenu;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(1, 538);
            this.listView.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(908, 220);
            this.listView.TabIndex = 112;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_ItemDrag);
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.Enter += new System.EventHandler(this.listView_Enter);
            this.listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_KeyDown);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
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
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemNumberDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.familyCodeDataGridViewTextBoxColumn,
            this.manufacturerDataGridViewTextBoxColumn,
            this.manufacturerItemNumberDataGridViewTextBoxColumn});
            this.dataGridView.ContextMenuStrip = this.FormSelector;
            this.dataGridView.DataSource = this.inventoryBindingSource3;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dataGridView.Location = new System.Drawing.Point(1, 98);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(911, 437);
            this.dataGridView.TabIndex = 111;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseMove);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting_1);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // itemNumberDataGridViewTextBoxColumn
            // 
            this.itemNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.itemNumberDataGridViewTextBoxColumn.DataPropertyName = "ItemNumber";
            this.itemNumberDataGridViewTextBoxColumn.FillWeight = 152.2843F;
            this.itemNumberDataGridViewTextBoxColumn.HeaderText = "Item No.";
            this.itemNumberDataGridViewTextBoxColumn.MinimumWidth = 85;
            this.itemNumberDataGridViewTextBoxColumn.Name = "itemNumberDataGridViewTextBoxColumn";
            this.itemNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemNumberDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.itemNumberDataGridViewTextBoxColumn.Width = 85;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            dataGridViewCellStyle1.NullValue = null;
            this.descriptionDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.descriptionDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // familyCodeDataGridViewTextBoxColumn
            // 
            this.familyCodeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.familyCodeDataGridViewTextBoxColumn.DataPropertyName = "FamilyCode";
            this.familyCodeDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.familyCodeDataGridViewTextBoxColumn.HeaderText = "Family Code";
            this.familyCodeDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.familyCodeDataGridViewTextBoxColumn.Name = "familyCodeDataGridViewTextBoxColumn";
            this.familyCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.familyCodeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.familyCodeDataGridViewTextBoxColumn.Width = 50;
            // 
            // manufacturerDataGridViewTextBoxColumn
            // 
            this.manufacturerDataGridViewTextBoxColumn.DataPropertyName = "Manufacturer";
            dataGridViewCellStyle2.NullValue = null;
            this.manufacturerDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.manufacturerDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.manufacturerDataGridViewTextBoxColumn.HeaderText = "Manufacturer";
            this.manufacturerDataGridViewTextBoxColumn.Name = "manufacturerDataGridViewTextBoxColumn";
            this.manufacturerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // manufacturerItemNumberDataGridViewTextBoxColumn
            // 
            this.manufacturerItemNumberDataGridViewTextBoxColumn.DataPropertyName = "ManufacturerItemNumber";
            dataGridViewCellStyle3.NullValue = null;
            this.manufacturerItemNumberDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.manufacturerItemNumberDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.manufacturerItemNumberDataGridViewTextBoxColumn.HeaderText = "Manufacturer Item Number";
            this.manufacturerItemNumberDataGridViewTextBoxColumn.Name = "manufacturerItemNumberDataGridViewTextBoxColumn";
            this.manufacturerItemNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clrfiltersbttn
            // 
            this.clrfiltersbttn.Dock = System.Windows.Forms.DockStyle.Top;
            this.clrfiltersbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clrfiltersbttn.Location = new System.Drawing.Point(0, 0);
            this.clrfiltersbttn.MinimumSize = new System.Drawing.Size(0, 40);
            this.clrfiltersbttn.Name = "clrfiltersbttn";
            this.clrfiltersbttn.Size = new System.Drawing.Size(205, 40);
            this.clrfiltersbttn.TabIndex = 128;
            this.clrfiltersbttn.Text = "Clear Filters";
            this.clrfiltersbttn.UseVisualStyleBackColor = true;
            this.clrfiltersbttn.Click += new System.EventHandler(this.clrfiltersbttn_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 18);
            this.label6.TabIndex = 145;
            this.label6.Text = "Designed/Created by";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 431);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 18);
            this.label5.TabIndex = 144;
            this.label5.Text = "Last Saved By";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 18);
            this.label4.TabIndex = 143;
            this.label4.Text = "OEM Item Number";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 18);
            this.label3.TabIndex = 142;
            this.label3.Text = "Manufacturer";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 18);
            this.label2.TabIndex = 141;
            this.label2.Text = "Family Type";
            // 
            // actcadblk
            // 
            this.actcadblk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actcadblk.AutoSize = true;
            this.actcadblk.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actcadblk.Location = new System.Drawing.Point(22, 512);
            this.actcadblk.Name = "actcadblk";
            this.actcadblk.Size = new System.Drawing.Size(148, 18);
            this.actcadblk.TabIndex = 140;
            this.actcadblk.Text = "Active CAD Block No";
            // 
            // SPM_Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(1119, 761);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "SPM_Connect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Eng.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SPM_Connect_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_Connect_FormClosed);
            this.Load += new System.EventHandler(this.SPM_Connect_Load);
            this.FormSelector.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet)).EndInit();
            this.Listviewcontextmenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

		}

        

        #endregion
		private SPM_DatabaseDataSet sPM_DatabaseDataSet;
		private System.Windows.Forms.BindingSource inventoryBindingSource3;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.ContextMenuStrip FormSelector;
        private System.Windows.Forms.ToolStripMenuItem bOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whereUsedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDrawingToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip Listviewcontextmenu;
        private System.Windows.Forms.ToolStripMenuItem bomlistviewmenustrpc;
        private System.Windows.Forms.ToolStripMenuItem whereusedlistviewStripMenu;
        private System.Windows.Forms.ToolStripMenuItem iteminfolistviewStripMenu;
        private System.Windows.Forms.ToolStripMenuItem editItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        public System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem viewReportBOM;
        private System.Windows.Forms.ToolStripMenuItem billsOfMaunfacturingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sparePartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button advsearchbttn;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Label recordlabel;
        private System.Windows.Forms.Button AddNewBttn;
        private System.Windows.Forms.Button jobsbttn;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button admin_bttn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn familyCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerItemNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button Reload;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox filter4;
        private System.Windows.Forms.TextBox filteroemitem_txtbox;
        private System.Windows.Forms.TextBox filteroem_txtbox;
        private System.Windows.Forms.TextBox Descrip_txtbox;
        private System.Windows.Forms.ComboBox designedbycombobox;
        private System.Windows.Forms.ComboBox Manufactureritemcomboxbox;
        private System.Windows.Forms.ComboBox familycomboxbox;
        private System.Windows.Forms.ComboBox lastsavedbycombo;
        private System.Windows.Forms.ComboBox oemitemcombobox;
        private System.Windows.Forms.ComboBox ActiveCadblockcombobox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label actcadblk;
        private System.Windows.Forms.Button clrfiltersbttn;
        private System.Windows.Forms.ToolStripMenuItem eModelViewerToolStripMenuItem1;


#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

