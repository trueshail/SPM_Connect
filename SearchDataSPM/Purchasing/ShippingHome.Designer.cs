using System;

namespace SearchDataSPM
{
	partial class ShippingHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShippingHome));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SPM = new System.Windows.Forms.Label();
            this.versionlabel = new System.Windows.Forms.Label();
            this.Reload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.advsearchbttn = new System.Windows.Forms.Button();
            this.Shiptocomboxbox = new System.Windows.Forms.ComboBox();
            this.Salespersoncomboxbox = new System.Windows.Forms.ComboBox();
            this.lastsavedbycombo = new System.Windows.Forms.ComboBox();
            this.Soldtocombobox = new System.Windows.Forms.ComboBox();
            this.custvendcombobox = new System.Windows.Forms.ComboBox();
            this.Createdbycombobox = new System.Windows.Forms.ComboBox();
            this.CarrierscomboBox = new System.Windows.Forms.ComboBox();
            this.addnewbttn = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.InvoiceItemsgrp = new System.Windows.Forms.GroupBox();
            this.invoiceitemsdataGridView2 = new System.Windows.Forms.DataGridView();
            this.recordlabel = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContextMenuStripShipping = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.invoiceinfostripmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.copyinvoicestrip = new System.Windows.Forms.ToolStripMenuItem();
            this.matlbl = new System.Windows.Forms.Label();
            this.clrfiltersbttn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.actcadblk = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.InvoiceItemsgrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceitemsdataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.ContextMenuStripShipping.SuspendLayout();
            this.SuspendLayout();
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
            this.SPM.Location = new System.Drawing.Point(4, 9);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(198, 85);
            this.SPM.TabIndex = 114;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TreeViewToolTip.SetToolTip(this.SPM, "SPM Automation Inc.");
            // 
            // versionlabel
            // 
            this.versionlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.versionlabel.AutoSize = true;
            this.versionlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionlabel.ForeColor = System.Drawing.Color.White;
            this.versionlabel.Location = new System.Drawing.Point(837, 3);
            this.versionlabel.MaximumSize = new System.Drawing.Size(26, 8);
            this.versionlabel.MinimumSize = new System.Drawing.Size(26, 8);
            this.versionlabel.Name = "versionlabel";
            this.versionlabel.Size = new System.Drawing.Size(26, 8);
            this.versionlabel.TabIndex = 116;
            this.versionlabel.Text = "V7.6.1";
            this.TreeViewToolTip.SetToolTip(this.versionlabel, "SPM Connect V7.6.1");
            // 
            // Reload
            // 
            this.Reload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.Location = new System.Drawing.Point(625, 23);
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
            this.txtSearch.Location = new System.Drawing.Point(209, 25);
            this.txtSearch.MaximumSize = new System.Drawing.Size(32767, 25);
            this.txtSearch.MinimumSize = new System.Drawing.Size(4, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(410, 25);
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
            this.filter4.Location = new System.Drawing.Point(675, 63);
            this.filter4.MaximumSize = new System.Drawing.Size(150, 26);
            this.filter4.MinimumSize = new System.Drawing.Size(150, 25);
            this.filter4.Name = "filter4";
            this.filter4.Size = new System.Drawing.Size(150, 26);
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
            this.filteroemitem_txtbox.Location = new System.Drawing.Point(520, 63);
            this.filteroemitem_txtbox.MaximumSize = new System.Drawing.Size(150, 26);
            this.filteroemitem_txtbox.MinimumSize = new System.Drawing.Size(150, 25);
            this.filteroemitem_txtbox.Name = "filteroemitem_txtbox";
            this.filteroemitem_txtbox.Size = new System.Drawing.Size(150, 26);
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
            this.filteroem_txtbox.Location = new System.Drawing.Point(365, 63);
            this.filteroem_txtbox.MaximumSize = new System.Drawing.Size(150, 26);
            this.filteroem_txtbox.MinimumSize = new System.Drawing.Size(150, 26);
            this.filteroem_txtbox.Name = "filteroem_txtbox";
            this.filteroem_txtbox.Size = new System.Drawing.Size(150, 26);
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
            this.Descrip_txtbox.Location = new System.Drawing.Point(209, 63);
            this.Descrip_txtbox.MaximumSize = new System.Drawing.Size(150, 26);
            this.Descrip_txtbox.MinimumSize = new System.Drawing.Size(150, 26);
            this.Descrip_txtbox.Name = "Descrip_txtbox";
            this.Descrip_txtbox.Size = new System.Drawing.Size(150, 26);
            this.Descrip_txtbox.TabIndex = 107;
            this.TreeViewToolTip.SetToolTip(this.Descrip_txtbox, "Enter Keyword 2");
            this.Descrip_txtbox.Visible = false;
            this.Descrip_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Descrip_txtbox_KeyDown);
            // 
            // advsearchbttn
            // 
            this.advsearchbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advsearchbttn.Location = new System.Drawing.Point(826, 64);
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
            // Shiptocomboxbox
            // 
            this.Shiptocomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Shiptocomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Shiptocomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Shiptocomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Shiptocomboxbox.FormattingEnabled = true;
            this.Shiptocomboxbox.Location = new System.Drawing.Point(6, 379);
            this.Shiptocomboxbox.Name = "Shiptocomboxbox";
            this.Shiptocomboxbox.Size = new System.Drawing.Size(161, 21);
            this.Shiptocomboxbox.TabIndex = 125;
            this.TreeViewToolTip.SetToolTip(this.Shiptocomboxbox, "Filter by ship to");
            this.Shiptocomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Manufactureritemcomboxbox_KeyDown);
            // 
            // Salespersoncomboxbox
            // 
            this.Salespersoncomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Salespersoncomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Salespersoncomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Salespersoncomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Salespersoncomboxbox.FormattingEnabled = true;
            this.Salespersoncomboxbox.Location = new System.Drawing.Point(6, 230);
            this.Salespersoncomboxbox.Name = "Salespersoncomboxbox";
            this.Salespersoncomboxbox.Size = new System.Drawing.Size(161, 21);
            this.Salespersoncomboxbox.TabIndex = 123;
            this.TreeViewToolTip.SetToolTip(this.Salespersoncomboxbox, "Filter by sales person");
            this.Salespersoncomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.familycomboxbox_KeyDown);
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
            this.lastsavedbycombo.Location = new System.Drawing.Point(6, 520);
            this.lastsavedbycombo.Name = "lastsavedbycombo";
            this.lastsavedbycombo.Size = new System.Drawing.Size(161, 21);
            this.lastsavedbycombo.TabIndex = 127;
            this.TreeViewToolTip.SetToolTip(this.lastsavedbycombo, "Filter by last saved");
            this.lastsavedbycombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lastsavedbycombo_KeyDown);
            // 
            // Soldtocombobox
            // 
            this.Soldtocombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Soldtocombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Soldtocombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Soldtocombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Soldtocombobox.FormattingEnabled = true;
            this.Soldtocombobox.Items.AddRange(new object[] {
            "Festo",
            "SPM AUTOMATION (Canada) INC."});
            this.Soldtocombobox.Location = new System.Drawing.Point(6, 304);
            this.Soldtocombobox.Name = "Soldtocombobox";
            this.Soldtocombobox.Size = new System.Drawing.Size(161, 21);
            this.Soldtocombobox.TabIndex = 124;
            this.TreeViewToolTip.SetToolTip(this.Soldtocombobox, "Filter by sold to");
            this.Soldtocombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oemitemcombobox_KeyDown);
            // 
            // custvendcombobox
            // 
            this.custvendcombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.custvendcombobox.AutoCompleteCustomSource.AddRange(new string[] {
            "0 - Vendor",
            "1 - Customer"});
            this.custvendcombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.custvendcombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.custvendcombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.custvendcombobox.FormattingEnabled = true;
            this.custvendcombobox.Items.AddRange(new object[] {
            "0 - Vendor",
            "1 - Customer"});
            this.custvendcombobox.Location = new System.Drawing.Point(6, 594);
            this.custvendcombobox.Name = "custvendcombobox";
            this.custvendcombobox.Size = new System.Drawing.Size(161, 21);
            this.custvendcombobox.TabIndex = 128;
            this.TreeViewToolTip.SetToolTip(this.custvendcombobox, "Filter by customer or vendor");
            this.custvendcombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActiveCadblockcombobox_KeyDown);
            // 
            // Createdbycombobox
            // 
            this.Createdbycombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Createdbycombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Createdbycombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Createdbycombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Createdbycombobox.FormattingEnabled = true;
            this.Createdbycombobox.Items.AddRange(new object[] {
            "Shailkumar Patel",
            "Scott Reid",
            "Joel Goldsmith"});
            this.Createdbycombobox.Location = new System.Drawing.Point(6, 153);
            this.Createdbycombobox.Name = "Createdbycombobox";
            this.Createdbycombobox.Size = new System.Drawing.Size(161, 21);
            this.Createdbycombobox.TabIndex = 122;
            this.TreeViewToolTip.SetToolTip(this.Createdbycombobox, "Filter Created by");
            this.Createdbycombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.designedbycombobox_KeyDown);
            // 
            // CarrierscomboBox
            // 
            this.CarrierscomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CarrierscomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CarrierscomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.CarrierscomboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CarrierscomboBox.FormattingEnabled = true;
            this.CarrierscomboBox.Location = new System.Drawing.Point(6, 450);
            this.CarrierscomboBox.Name = "CarrierscomboBox";
            this.CarrierscomboBox.Size = new System.Drawing.Size(161, 21);
            this.CarrierscomboBox.TabIndex = 126;
            this.TreeViewToolTip.SetToolTip(this.CarrierscomboBox, "Filter by carrier");
            this.CarrierscomboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaterialcomboBox_KeyDown);
            // 
            // addnewbttn
            // 
            this.addnewbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addnewbttn.BackColor = System.Drawing.Color.Transparent;
            this.addnewbttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addnewbttn.ForeColor = System.Drawing.Color.White;
            this.addnewbttn.Image = ((System.Drawing.Image)(resources.GetObject("addnewbttn.Image")));
            this.addnewbttn.Location = new System.Drawing.Point(768, 3);
            this.addnewbttn.MaximumSize = new System.Drawing.Size(65, 56);
            this.addnewbttn.MinimumSize = new System.Drawing.Size(65, 56);
            this.addnewbttn.Name = "addnewbttn";
            this.addnewbttn.Size = new System.Drawing.Size(65, 56);
            this.addnewbttn.TabIndex = 122;
            this.addnewbttn.Text = "Add New";
            this.addnewbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.addnewbttn, "Create New Invoice");
            this.addnewbttn.UseVisualStyleBackColor = false;
            this.addnewbttn.Click += new System.EventHandler(this.addnewbttn_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(750, 600);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.splitContainer1.Panel1.Controls.Add(this.InvoiceItemsgrp);
            this.splitContainer1.Panel1.Controls.Add(this.addnewbttn);
            this.splitContainer1.Panel1.Controls.Add(this.advsearchbttn);
            this.splitContainer1.Panel1.Controls.Add(this.SPM);
            this.splitContainer1.Panel1.Controls.Add(this.recordlabel);
            this.splitContainer1.Panel1.Controls.Add(this.versionlabel);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.Reload);
            this.splitContainer1.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer1.Panel1.Controls.Add(this.filter4);
            this.splitContainer1.Panel1.Controls.Add(this.filteroemitem_txtbox);
            this.splitContainer1.Panel1.Controls.Add(this.filteroem_txtbox);
            this.splitContainer1.Panel1.Controls.Add(this.Descrip_txtbox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 600;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.splitContainer1.Panel2.Controls.Add(this.matlbl);
            this.splitContainer1.Panel2.Controls.Add(this.CarrierscomboBox);
            this.splitContainer1.Panel2.Controls.Add(this.clrfiltersbttn);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.actcadblk);
            this.splitContainer1.Panel2.Controls.Add(this.Shiptocomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.Salespersoncomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.lastsavedbycombo);
            this.splitContainer1.Panel2.Controls.Add(this.Soldtocombobox);
            this.splitContainer1.Panel2.Controls.Add(this.custvendcombobox);
            this.splitContainer1.Panel2.Controls.Add(this.Createdbycombobox);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2MinSize = 175;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1044, 711);
            this.splitContainer1.SplitterDistance = 867;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 105;
            // 
            // InvoiceItemsgrp
            // 
            this.InvoiceItemsgrp.Controls.Add(this.invoiceitemsdataGridView2);
            this.InvoiceItemsgrp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InvoiceItemsgrp.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceItemsgrp.Location = new System.Drawing.Point(0, 510);
            this.InvoiceItemsgrp.Name = "InvoiceItemsgrp";
            this.InvoiceItemsgrp.Size = new System.Drawing.Size(867, 201);
            this.InvoiceItemsgrp.TabIndex = 123;
            this.InvoiceItemsgrp.TabStop = false;
            this.InvoiceItemsgrp.Text = "Showing items for InvoiceNo: ";
            // 
            // invoiceitemsdataGridView2
            // 
            this.invoiceitemsdataGridView2.AllowUserToAddRows = false;
            this.invoiceitemsdataGridView2.AllowUserToDeleteRows = false;
            this.invoiceitemsdataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.invoiceitemsdataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.invoiceitemsdataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.invoiceitemsdataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.invoiceitemsdataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invoiceitemsdataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.invoiceitemsdataGridView2.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.invoiceitemsdataGridView2.Location = new System.Drawing.Point(3, 16);
            this.invoiceitemsdataGridView2.Name = "invoiceitemsdataGridView2";
            this.invoiceitemsdataGridView2.ReadOnly = true;
            this.invoiceitemsdataGridView2.RowHeadersVisible = false;
            this.invoiceitemsdataGridView2.Size = new System.Drawing.Size(861, 182);
            this.invoiceitemsdataGridView2.TabIndex = 0;
            // 
            // recordlabel
            // 
            this.recordlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recordlabel.AutoSize = true;
            this.recordlabel.Font = new System.Drawing.Font("Maiandra GD", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordlabel.ForeColor = System.Drawing.Color.Black;
            this.recordlabel.Location = new System.Drawing.Point(657, 80);
            this.recordlabel.Name = "recordlabel";
            this.recordlabel.Size = new System.Drawing.Size(0, 14);
            this.recordlabel.TabIndex = 119;
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
            this.dataGridView.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceNo,
            this.SoldTo,
            this.ShipTo,
            this.JobNumber,
            this.DateCreated});
            this.dataGridView.ContextMenuStrip = this.ContextMenuStripShipping;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dataGridView.Location = new System.Drawing.Point(1, 98);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(863, 406);
            this.dataGridView.StandardTab = true;
            this.dataGridView.TabIndex = 111;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseMove);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting_1);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // InvoiceNo
            // 
            this.InvoiceNo.DataPropertyName = "InvoiceNo";
            this.InvoiceNo.FillWeight = 126.9036F;
            this.InvoiceNo.HeaderText = "Invoice No";
            this.InvoiceNo.Name = "InvoiceNo";
            this.InvoiceNo.ReadOnly = true;
            // 
            // SoldTo
            // 
            this.SoldTo.DataPropertyName = "SoldToName";
            this.SoldTo.FillWeight = 116.1212F;
            this.SoldTo.HeaderText = "Sold To";
            this.SoldTo.Name = "SoldTo";
            this.SoldTo.ReadOnly = true;
            // 
            // ShipTo
            // 
            this.ShipTo.DataPropertyName = "ShipToName";
            this.ShipTo.FillWeight = 77.8091F;
            this.ShipTo.HeaderText = "Ship To";
            this.ShipTo.Name = "ShipTo";
            this.ShipTo.ReadOnly = true;
            // 
            // JobNumber
            // 
            this.JobNumber.DataPropertyName = "JobNumber";
            this.JobNumber.FillWeight = 73.30341F;
            this.JobNumber.HeaderText = "Job No";
            this.JobNumber.Name = "JobNumber";
            this.JobNumber.ReadOnly = true;
            // 
            // DateCreated
            // 
            this.DateCreated.DataPropertyName = "DateCreated";
            this.DateCreated.FillWeight = 105.8627F;
            this.DateCreated.HeaderText = "Created On";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            // 
            // ContextMenuStripShipping
            // 
            this.ContextMenuStripShipping.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoiceinfostripmenu,
            this.copyinvoicestrip});
            this.ContextMenuStripShipping.Name = "contextMenuStrip1";
            this.ContextMenuStripShipping.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ContextMenuStripShipping.Size = new System.Drawing.Size(191, 48);
            this.ContextMenuStripShipping.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripShipping_Opening);
            // 
            // invoiceinfostripmenu
            // 
            this.invoiceinfostripmenu.Image = ((System.Drawing.Image)(resources.GetObject("invoiceinfostripmenu.Image")));
            this.invoiceinfostripmenu.Name = "invoiceinfostripmenu";
            this.invoiceinfostripmenu.Size = new System.Drawing.Size(190, 22);
            this.invoiceinfostripmenu.Text = "Get Invoice Info";
            this.invoiceinfostripmenu.ToolTipText = "Get Selected Invoice Info.";
            this.invoiceinfostripmenu.Click += new System.EventHandler(this.invoiceinfostripmenu_Click);
            // 
            // copyinvoicestrip
            // 
            this.copyinvoicestrip.Image = ((System.Drawing.Image)(resources.GetObject("copyinvoicestrip.Image")));
            this.copyinvoicestrip.Name = "copyinvoicestrip";
            this.copyinvoicestrip.Size = new System.Drawing.Size(190, 22);
            this.copyinvoicestrip.Text = "Copy Selected Invoice";
            this.copyinvoicestrip.ToolTipText = "Copy selected invoice to new invoice number";
            this.copyinvoicestrip.Click += new System.EventHandler(this.copyinvoicestrip_Click);
            // 
            // matlbl
            // 
            this.matlbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.matlbl.AutoSize = true;
            this.matlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matlbl.ForeColor = System.Drawing.Color.Black;
            this.matlbl.Location = new System.Drawing.Point(9, 428);
            this.matlbl.Name = "matlbl";
            this.matlbl.Size = new System.Drawing.Size(61, 18);
            this.matlbl.TabIndex = 147;
            this.matlbl.Text = "Carriers";
            // 
            // clrfiltersbttn
            // 
            this.clrfiltersbttn.Dock = System.Windows.Forms.DockStyle.Top;
            this.clrfiltersbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clrfiltersbttn.Location = new System.Drawing.Point(0, 0);
            this.clrfiltersbttn.MinimumSize = new System.Drawing.Size(0, 40);
            this.clrfiltersbttn.Name = "clrfiltersbttn";
            this.clrfiltersbttn.Size = new System.Drawing.Size(175, 40);
            this.clrfiltersbttn.TabIndex = 129;
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
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(9, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 18);
            this.label6.TabIndex = 145;
            this.label6.Text = "Created by";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 499);
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
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(9, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 18);
            this.label4.TabIndex = 143;
            this.label4.Text = "Ship To";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(9, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 18);
            this.label3.TabIndex = 142;
            this.label3.Text = "Sold To";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(9, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 141;
            this.label2.Text = "Sales Person";
            // 
            // actcadblk
            // 
            this.actcadblk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actcadblk.AutoSize = true;
            this.actcadblk.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actcadblk.ForeColor = System.Drawing.Color.Black;
            this.actcadblk.Location = new System.Drawing.Point(12, 573);
            this.actcadblk.Name = "actcadblk";
            this.actcadblk.Size = new System.Drawing.Size(125, 18);
            this.actcadblk.TabIndex = 140;
            this.actcadblk.Text = "Customer/Vendor";
            // 
            // ShippingHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1044, 711);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "ShippingHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Shipping Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SPM_Connect_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_Connect_FormClosed);
            this.Load += new System.EventHandler(this.SPM_Connect_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.InvoiceItemsgrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.invoiceitemsdataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ContextMenuStripShipping.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        

        #endregion
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button advsearchbttn;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Label versionlabel;
        private System.Windows.Forms.Button Reload;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox filter4;
        private System.Windows.Forms.TextBox filteroemitem_txtbox;
        private System.Windows.Forms.TextBox filteroem_txtbox;
        private System.Windows.Forms.TextBox Descrip_txtbox;
        private System.Windows.Forms.ComboBox Createdbycombobox;
        private System.Windows.Forms.ComboBox Shiptocomboxbox;
        private System.Windows.Forms.ComboBox Salespersoncomboxbox;
        private System.Windows.Forms.ComboBox lastsavedbycombo;
        private System.Windows.Forms.ComboBox Soldtocombobox;
        private System.Windows.Forms.ComboBox custvendcombobox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label actcadblk;
        private System.Windows.Forms.Button clrfiltersbttn;
        public System.Windows.Forms.DataGridView dataGridView;
        public System.Windows.Forms.Label recordlabel;
        private System.Windows.Forms.Label matlbl;
        private System.Windows.Forms.ComboBox CarrierscomboBox;
        private System.Windows.Forms.Button addnewbttn;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShipTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.GroupBox InvoiceItemsgrp;
        private System.Windows.Forms.DataGridView invoiceitemsdataGridView2;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStripShipping;
        private System.Windows.Forms.ToolStripMenuItem invoiceinfostripmenu;
        private System.Windows.Forms.ToolStripMenuItem copyinvoicestrip;


#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

