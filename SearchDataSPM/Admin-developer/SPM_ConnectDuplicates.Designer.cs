namespace SearchDataSPM
{
	partial class SPM_ConnectDuplicates
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPM_ConnectDuplicates));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.familyCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manufacturerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manufacturerItemNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDrawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.sPM_DatabaseDataSet = new SearchDataSPM.SPM_DatabaseDataSet();
            this.Reload = new System.Windows.Forms.Button();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.SPM = new System.Windows.Forms.Label();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.inventoryTableAdapter = new SearchDataSPM.SPM_DatabaseDataSetTableAdapters.InventoryTableAdapter();
            this.tableAdapterManager = new SearchDataSPM.SPM_DatabaseDataSetTableAdapters.TableAdapterManager();
            this.ParentView = new System.Windows.Forms.Button();
            this.TreeView_Bttn = new System.Windows.Forms.Button();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet)).BeginInit();
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
            this.txtSearch.Size = new System.Drawing.Size(570, 26);
            this.txtSearch.TabIndex = 1;
            this.TreeViewToolTip.SetToolTip(this.txtSearch, "Enter Search Keyword.\r\n(Double click to reset)");
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.DoubleClick += new System.EventHandler(this.txtSearch_DoubleClick);
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
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemNumber,
            this.itemNumberDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.familyCodeDataGridViewTextBoxColumn,
            this.manufacturerDataGridViewTextBoxColumn,
            this.manufacturerItemNumberDataGridViewTextBoxColumn});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView.DataSource = this.inventoryBindingSource3;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dataGridView.Location = new System.Drawing.Point(2, 100);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(980, 559);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentDoubleClick);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting_1);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // ItemNumber
            // 
            this.ItemNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ItemNumber.DataPropertyName = "ItemNumber";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.ItemNumber.DefaultCellStyle = dataGridViewCellStyle1;
            this.ItemNumber.Frozen = true;
            this.ItemNumber.HeaderText = "";
            this.ItemNumber.MinimumWidth = 40;
            this.ItemNumber.Name = "ItemNumber";
            this.ItemNumber.ReadOnly = true;
            this.ItemNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemNumber.Width = 40;
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
            dataGridViewCellStyle2.NullValue = null;
            this.descriptionDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.NullValue = null;
            this.manufacturerDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.manufacturerDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.manufacturerDataGridViewTextBoxColumn.HeaderText = "Manufacturer";
            this.manufacturerDataGridViewTextBoxColumn.Name = "manufacturerDataGridViewTextBoxColumn";
            this.manufacturerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // manufacturerItemNumberDataGridViewTextBoxColumn
            // 
            this.manufacturerItemNumberDataGridViewTextBoxColumn.DataPropertyName = "ManufacturerItemNumber";
            dataGridViewCellStyle4.NullValue = null;
            this.manufacturerItemNumberDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.manufacturerItemNumberDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.manufacturerItemNumberDataGridViewTextBoxColumn.HeaderText = "Manufacturer Item Number";
            this.manufacturerItemNumberDataGridViewTextBoxColumn.Name = "manufacturerItemNumberDataGridViewTextBoxColumn";
            this.manufacturerItemNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.openModelToolStripMenuItem,
            this.openDrawingToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItem1.Text = "BOM";
            this.toolStripMenuItem1.ToolTipText = "Bills Of Material";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItem2.Text = "Where Used";
            this.toolStripMenuItem2.ToolTipText = "Check Where Used";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // openModelToolStripMenuItem
            // 
            this.openModelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openModelToolStripMenuItem.Image")));
            this.openModelToolStripMenuItem.Name = "openModelToolStripMenuItem";
            this.openModelToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openModelToolStripMenuItem.Text = "Open Model";
            this.openModelToolStripMenuItem.ToolTipText = "Open Selected Item\'s Model";
            this.openModelToolStripMenuItem.Click += new System.EventHandler(this.openModelToolStripMenuItem_Click);
            // 
            // openDrawingToolStripMenuItem
            // 
            this.openDrawingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openDrawingToolStripMenuItem.Image")));
            this.openDrawingToolStripMenuItem.Name = "openDrawingToolStripMenuItem";
            this.openDrawingToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openDrawingToolStripMenuItem.Text = "Open Drawing";
            this.openDrawingToolStripMenuItem.ToolTipText = "Open Selected Item\'s Drawing";
            this.openDrawingToolStripMenuItem.Click += new System.EventHandler(this.openDrawingToolStripMenuItem_Click);
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
            // Reload
            // 
            this.Reload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.Location = new System.Drawing.Point(782, 8);
            this.Reload.MaximumSize = new System.Drawing.Size(140, 30);
            this.Reload.MinimumSize = new System.Drawing.Size(140, 30);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(140, 30);
            this.Reload.TabIndex = 7;
            this.Reload.Text = "Refresh / Show All";
            this.TreeViewToolTip.SetToolTip(this.Reload, "Click To Resest & Get Updated Data");
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
            // ParentView
            // 
            this.ParentView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ParentView.BackColor = System.Drawing.Color.Transparent;
            this.ParentView.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.ParentView.FlatAppearance.BorderSize = 0;
            this.ParentView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ParentView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ParentView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ParentView.ForeColor = System.Drawing.Color.Transparent;
            this.ParentView.Image = ((System.Drawing.Image)(resources.GetObject("ParentView.Image")));
            this.ParentView.Location = new System.Drawing.Point(955, 10);
            this.ParentView.MaximumSize = new System.Drawing.Size(25, 25);
            this.ParentView.MinimumSize = new System.Drawing.Size(25, 25);
            this.ParentView.Name = "ParentView";
            this.ParentView.Size = new System.Drawing.Size(25, 25);
            this.ParentView.TabIndex = 15;
            this.ParentView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.ParentView, "Where Used");
            this.ParentView.UseVisualStyleBackColor = false;
            this.ParentView.Click += new System.EventHandler(this.ParentView_Click);
            // 
            // TreeView_Bttn
            // 
            this.TreeView_Bttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_Bttn.BackColor = System.Drawing.Color.Transparent;
            this.TreeView_Bttn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.TreeView_Bttn.FlatAppearance.BorderSize = 0;
            this.TreeView_Bttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TreeView_Bttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TreeView_Bttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TreeView_Bttn.ForeColor = System.Drawing.Color.Transparent;
            this.TreeView_Bttn.Image = ((System.Drawing.Image)(resources.GetObject("TreeView_Bttn.Image")));
            this.TreeView_Bttn.Location = new System.Drawing.Point(924, 10);
            this.TreeView_Bttn.MaximumSize = new System.Drawing.Size(25, 25);
            this.TreeView_Bttn.MinimumSize = new System.Drawing.Size(25, 25);
            this.TreeView_Bttn.Name = "TreeView_Bttn";
            this.TreeView_Bttn.Size = new System.Drawing.Size(25, 25);
            this.TreeView_Bttn.TabIndex = 16;
            this.TreeView_Bttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.TreeView_Bttn, "Bill of Manufacturing");
            this.TreeView_Bttn.UseVisualStyleBackColor = false;
            this.TreeView_Bttn.Click += new System.EventHandler(this.TreeView_Bttn_Click);
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // SPM_ConnectDuplicates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.TreeView_Bttn);
            this.Controls.Add(this.ParentView);
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
            this.Name = "SPM_ConnectDuplicates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Duplicate Items";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SPM_Connect_FormClosing);
            this.Load += new System.EventHandler(this.SPM_Connect_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SPM_Connect_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sPM_DatabaseDataSet)).EndInit();
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
		private SPM_DatabaseDataSet sPM_DatabaseDataSet;
		private System.Windows.Forms.BindingSource inventoryBindingSource3;
		private SPM_DatabaseDataSetTableAdapters.InventoryTableAdapter inventoryTableAdapter;
		private SPM_DatabaseDataSetTableAdapters.TableAdapterManager tableAdapterManager;
		private System.Windows.Forms.TextBox filter4;
		private System.Windows.Forms.Button ParentView;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
		public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button TreeView_Bttn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn familyCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerItemNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDrawingToolStripMenuItem;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

