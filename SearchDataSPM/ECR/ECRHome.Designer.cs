using System;

namespace SearchDataSPM
{
	partial class ECRHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ECRHome));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.versionlabel = new System.Windows.Forms.Label();
            this.Reload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.advsearchbttn = new System.Windows.Forms.Button();
            this.requestedbycomboxbox = new System.Windows.Forms.ComboBox();
            this.deptrequestedcomboxbox = new System.Windows.Forms.ComboBox();
            this.approvedbycombo = new System.Windows.Forms.ComboBox();
            this.ecrstatuscombobox = new System.Windows.Forms.ComboBox();
            this.jobnumbercombobox = new System.Windows.Forms.ComboBox();
            this.supervicsorcomboBox = new System.Windows.Forms.ComboBox();
            this.completedbycombobox = new System.Windows.Forms.ComboBox();
            this.addnewbttn = new System.Windows.Forms.Button();
            this.SPM = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.recordlabel = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ECRNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SANo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContextMenuStripShipping = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.invoiceinfostripmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.attentionbttn = new System.Windows.Forms.Button();
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
            // versionlabel
            // 
            this.versionlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.versionlabel.AutoSize = true;
            this.versionlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionlabel.ForeColor = System.Drawing.Color.Black;
            this.versionlabel.Location = new System.Drawing.Point(833, 6);
            this.versionlabel.MaximumSize = new System.Drawing.Size(35, 8);
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
            this.Reload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.ForeColor = System.Drawing.Color.Black;
            this.Reload.Location = new System.Drawing.Point(484, 22);
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
            this.txtSearch.Size = new System.Drawing.Size(271, 25);
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
            // requestedbycomboxbox
            // 
            this.requestedbycomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requestedbycomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.requestedbycomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.requestedbycomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.requestedbycomboxbox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestedbycomboxbox.FormattingEnabled = true;
            this.requestedbycomboxbox.Location = new System.Drawing.Point(8, 379);
            this.requestedbycomboxbox.Name = "requestedbycomboxbox";
            this.requestedbycomboxbox.Size = new System.Drawing.Size(157, 22);
            this.requestedbycomboxbox.TabIndex = 125;
            this.TreeViewToolTip.SetToolTip(this.requestedbycomboxbox, "Filter by ship to");
            this.requestedbycomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Manufactureritemcomboxbox_KeyDown);
            this.requestedbycomboxbox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.requestedbycomboxbox_PreviewKeyDown);
            // 
            // deptrequestedcomboxbox
            // 
            this.deptrequestedcomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deptrequestedcomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.deptrequestedcomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.deptrequestedcomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deptrequestedcomboxbox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deptrequestedcomboxbox.FormattingEnabled = true;
            this.deptrequestedcomboxbox.Location = new System.Drawing.Point(8, 230);
            this.deptrequestedcomboxbox.Name = "deptrequestedcomboxbox";
            this.deptrequestedcomboxbox.Size = new System.Drawing.Size(157, 22);
            this.deptrequestedcomboxbox.TabIndex = 123;
            this.TreeViewToolTip.SetToolTip(this.deptrequestedcomboxbox, "Filter by sales person");
            this.deptrequestedcomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.familycomboxbox_KeyDown);
            this.deptrequestedcomboxbox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.deptrequestedcomboxbox_PreviewKeyDown);
            // 
            // approvedbycombo
            // 
            this.approvedbycombo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.approvedbycombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.approvedbycombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.approvedbycombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.approvedbycombo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.approvedbycombo.FormattingEnabled = true;
            this.approvedbycombo.Location = new System.Drawing.Point(8, 520);
            this.approvedbycombo.Name = "approvedbycombo";
            this.approvedbycombo.Size = new System.Drawing.Size(157, 22);
            this.approvedbycombo.TabIndex = 127;
            this.TreeViewToolTip.SetToolTip(this.approvedbycombo, "Filter by last saved");
            this.approvedbycombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lastsavedbycombo_KeyDown);
            this.approvedbycombo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.approvedbycombo_PreviewKeyDown);
            // 
            // ecrstatuscombobox
            // 
            this.ecrstatuscombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ecrstatuscombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ecrstatuscombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ecrstatuscombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ecrstatuscombobox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecrstatuscombobox.FormattingEnabled = true;
            this.ecrstatuscombobox.Items.AddRange(new object[] {
            "Festo",
            "SPM AUTOMATION (Canada) INC."});
            this.ecrstatuscombobox.Location = new System.Drawing.Point(8, 304);
            this.ecrstatuscombobox.Name = "ecrstatuscombobox";
            this.ecrstatuscombobox.Size = new System.Drawing.Size(157, 22);
            this.ecrstatuscombobox.TabIndex = 124;
            this.TreeViewToolTip.SetToolTip(this.ecrstatuscombobox, "Filter by sold to");
            this.ecrstatuscombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oemitemcombobox_KeyDown);
            this.ecrstatuscombobox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ecrstatuscombobox_PreviewKeyDown);
            // 
            // jobnumbercombobox
            // 
            this.jobnumbercombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jobnumbercombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.jobnumbercombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.jobnumbercombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.jobnumbercombobox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobnumbercombobox.FormattingEnabled = true;
            this.jobnumbercombobox.Items.AddRange(new object[] {
            "Shailkumar Patel",
            "Scott Reid",
            "Joel Goldsmith"});
            this.jobnumbercombobox.Location = new System.Drawing.Point(8, 153);
            this.jobnumbercombobox.Name = "jobnumbercombobox";
            this.jobnumbercombobox.Size = new System.Drawing.Size(157, 22);
            this.jobnumbercombobox.TabIndex = 122;
            this.TreeViewToolTip.SetToolTip(this.jobnumbercombobox, "Filter Created by");
            this.jobnumbercombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.designedbycombobox_KeyDown);
            this.jobnumbercombobox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.jobnumbercombobox_PreviewKeyDown);
            // 
            // supervicsorcomboBox
            // 
            this.supervicsorcomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.supervicsorcomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.supervicsorcomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.supervicsorcomboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.supervicsorcomboBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supervicsorcomboBox.FormattingEnabled = true;
            this.supervicsorcomboBox.Location = new System.Drawing.Point(8, 450);
            this.supervicsorcomboBox.Name = "supervicsorcomboBox";
            this.supervicsorcomboBox.Size = new System.Drawing.Size(157, 22);
            this.supervicsorcomboBox.TabIndex = 126;
            this.TreeViewToolTip.SetToolTip(this.supervicsorcomboBox, "Filter by carrier");
            this.supervicsorcomboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaterialcomboBox_KeyDown);
            this.supervicsorcomboBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.supervicsorcomboBox_PreviewKeyDown);
            // 
            // completedbycombobox
            // 
            this.completedbycombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.completedbycombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.completedbycombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.completedbycombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.completedbycombobox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.completedbycombobox.FormattingEnabled = true;
            this.completedbycombobox.Location = new System.Drawing.Point(8, 594);
            this.completedbycombobox.Name = "completedbycombobox";
            this.completedbycombobox.Size = new System.Drawing.Size(157, 22);
            this.completedbycombobox.TabIndex = 148;
            this.TreeViewToolTip.SetToolTip(this.completedbycombobox, "Filter by last saved");
            this.completedbycombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActiveCadblockcombobox_KeyDown);
            this.completedbycombobox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.completedbycombobox_PreviewKeyDown);
            // 
            // addnewbttn
            // 
            this.addnewbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addnewbttn.BackColor = System.Drawing.Color.Transparent;
            this.addnewbttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addnewbttn.ForeColor = System.Drawing.Color.Black;
            this.addnewbttn.Image = ((System.Drawing.Image)(resources.GetObject("addnewbttn.Image")));
            this.addnewbttn.Location = new System.Drawing.Point(759, 5);
            this.addnewbttn.MaximumSize = new System.Drawing.Size(65, 56);
            this.addnewbttn.MinimumSize = new System.Drawing.Size(65, 56);
            this.addnewbttn.Name = "addnewbttn";
            this.addnewbttn.Size = new System.Drawing.Size(65, 56);
            this.addnewbttn.TabIndex = 122;
            this.addnewbttn.Text = "Add New";
            this.addnewbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.addnewbttn, "Create New Invoice");
            this.addnewbttn.UseVisualStyleBackColor = false;
            this.addnewbttn.Visible = false;
            this.addnewbttn.Click += new System.EventHandler(this.addnewbttn_Click);
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Thistle;
            this.splitContainer1.Panel1.Controls.Add(this.addnewbttn);
            this.splitContainer1.Panel1.Controls.Add(this.advsearchbttn);
            this.splitContainer1.Panel1.Controls.Add(this.SPM);
            this.splitContainer1.Panel1.Controls.Add(this.recordlabel);
            this.splitContainer1.Panel1.Controls.Add(this.versionlabel);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.attentionbttn);
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
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Lavender;
            this.splitContainer1.Panel2.Controls.Add(this.completedbycombobox);
            this.splitContainer1.Panel2.Controls.Add(this.matlbl);
            this.splitContainer1.Panel2.Controls.Add(this.supervicsorcomboBox);
            this.splitContainer1.Panel2.Controls.Add(this.clrfiltersbttn);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.actcadblk);
            this.splitContainer1.Panel2.Controls.Add(this.requestedbycomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.deptrequestedcomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.approvedbycombo);
            this.splitContainer1.Panel2.Controls.Add(this.ecrstatuscombobox);
            this.splitContainer1.Panel2.Controls.Add(this.jobnumbercombobox);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel2MinSize = 175;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1044, 711);
            this.splitContainer1.SplitterDistance = 867;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 105;
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
            this.dataGridView.BackgroundColor = System.Drawing.Color.Thistle;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ECRNo,
            this.JobNo,
            this.JobName,
            this.SANo,
            this.SAName,
            this.RequestedBy,
            this.DateCreated});
            this.dataGridView.ContextMenuStrip = this.ContextMenuStripShipping;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dataGridView.Location = new System.Drawing.Point(1, 98);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(863, 610);
            this.dataGridView.StandardTab = true;
            this.dataGridView.TabIndex = 111;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseMove);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting_1);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // ECRNo
            // 
            this.ECRNo.DataPropertyName = "ECRNo";
            this.ECRNo.FillWeight = 54.65411F;
            this.ECRNo.HeaderText = "ECR No";
            this.ECRNo.Name = "ECRNo";
            this.ECRNo.ReadOnly = true;
            // 
            // JobNo
            // 
            this.JobNo.DataPropertyName = "JobNo";
            this.JobNo.FillWeight = 157.483F;
            this.JobNo.HeaderText = "JobNo";
            this.JobNo.Name = "JobNo";
            this.JobNo.ReadOnly = true;
            // 
            // JobName
            // 
            this.JobName.DataPropertyName = "JobName";
            this.JobName.FillWeight = 91.09017F;
            this.JobName.HeaderText = "Job Name";
            this.JobName.Name = "JobName";
            this.JobName.ReadOnly = true;
            // 
            // SANo
            // 
            this.SANo.DataPropertyName = "SANo";
            this.SANo.FillWeight = 68.48032F;
            this.SANo.HeaderText = "Sub Assy No";
            this.SANo.Name = "SANo";
            this.SANo.ReadOnly = true;
            // 
            // SAName
            // 
            this.SAName.DataPropertyName = "SAName";
            this.SAName.FillWeight = 91.09017F;
            this.SAName.HeaderText = "Sub Assy Name";
            this.SAName.Name = "SAName";
            this.SAName.ReadOnly = true;
            // 
            // RequestedBy
            // 
            this.RequestedBy.DataPropertyName = "RequestedBy";
            this.RequestedBy.FillWeight = 64.51484F;
            this.RequestedBy.HeaderText = "Requested By";
            this.RequestedBy.Name = "RequestedBy";
            this.RequestedBy.ReadOnly = true;
            // 
            // DateCreated
            // 
            this.DateCreated.DataPropertyName = "DateCreated";
            this.DateCreated.FillWeight = 93.17049F;
            this.DateCreated.HeaderText = "Created On";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            // 
            // ContextMenuStripShipping
            // 
            this.ContextMenuStripShipping.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoiceinfostripmenu});
            this.ContextMenuStripShipping.Name = "contextMenuStrip1";
            this.ContextMenuStripShipping.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ContextMenuStripShipping.Size = new System.Drawing.Size(158, 26);
            this.ContextMenuStripShipping.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripShipping_Opening);
            // 
            // invoiceinfostripmenu
            // 
            this.invoiceinfostripmenu.Image = ((System.Drawing.Image)(resources.GetObject("invoiceinfostripmenu.Image")));
            this.invoiceinfostripmenu.Name = "invoiceinfostripmenu";
            this.invoiceinfostripmenu.Size = new System.Drawing.Size(157, 22);
            this.invoiceinfostripmenu.Text = "Get Invoice Info";
            this.invoiceinfostripmenu.ToolTipText = "Get Selected Invoice Info.";
            this.invoiceinfostripmenu.Click += new System.EventHandler(this.invoiceinfostripmenu_Click);
            // 
            // attentionbttn
            // 
            this.attentionbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attentionbttn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.attentionbttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attentionbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attentionbttn.ForeColor = System.Drawing.Color.Black;
            this.attentionbttn.Location = new System.Drawing.Point(628, 22);
            this.attentionbttn.MaximumSize = new System.Drawing.Size(120, 30);
            this.attentionbttn.MinimumSize = new System.Drawing.Size(120, 30);
            this.attentionbttn.Name = "attentionbttn";
            this.attentionbttn.Size = new System.Drawing.Size(120, 30);
            this.attentionbttn.TabIndex = 113;
            this.attentionbttn.Text = "Req. Attention";
            this.attentionbttn.UseVisualStyleBackColor = true;
            this.attentionbttn.Click += new System.EventHandler(this.attentionbttn_Click);
            // 
            // matlbl
            // 
            this.matlbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.matlbl.AutoSize = true;
            this.matlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matlbl.ForeColor = System.Drawing.Color.Black;
            this.matlbl.Location = new System.Drawing.Point(10, 573);
            this.matlbl.Name = "matlbl";
            this.matlbl.Size = new System.Drawing.Size(101, 18);
            this.matlbl.TabIndex = 147;
            this.matlbl.Text = "Completed By";
            // 
            // clrfiltersbttn
            // 
            this.clrfiltersbttn.Dock = System.Windows.Forms.DockStyle.Top;
            this.clrfiltersbttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.label6.Location = new System.Drawing.Point(10, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 18);
            this.label6.TabIndex = 145;
            this.label6.Text = "Requested by";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 429);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 18);
            this.label5.TabIndex = 144;
            this.label5.Text = "Supervisor";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(8, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 18);
            this.label4.TabIndex = 143;
            this.label4.Text = "Dept. Requested";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 18);
            this.label3.TabIndex = 142;
            this.label3.Text = "ECR Status";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 141;
            this.label2.Text = "Job Number";
            // 
            // actcadblk
            // 
            this.actcadblk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actcadblk.AutoSize = true;
            this.actcadblk.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actcadblk.ForeColor = System.Drawing.Color.Black;
            this.actcadblk.Location = new System.Drawing.Point(8, 499);
            this.actcadblk.Name = "actcadblk";
            this.actcadblk.Size = new System.Drawing.Size(91, 18);
            this.actcadblk.TabIndex = 140;
            this.actcadblk.Text = "Approved By";
            // 
            // ECRHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1044, 711);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "ECRHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Engineering Change Request";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SPM_Connect_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_Connect_FormClosed);
            this.Load += new System.EventHandler(this.SPM_Connect_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox jobnumbercombobox;
        private System.Windows.Forms.ComboBox requestedbycomboxbox;
        private System.Windows.Forms.ComboBox deptrequestedcomboxbox;
        private System.Windows.Forms.ComboBox approvedbycombo;
        private System.Windows.Forms.ComboBox ecrstatuscombobox;
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
        private System.Windows.Forms.ComboBox supervicsorcomboBox;
        private System.Windows.Forms.Button addnewbttn;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStripShipping;
        private System.Windows.Forms.ToolStripMenuItem invoiceinfostripmenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn ECRNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SANo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.Button attentionbttn;
        private System.Windows.Forms.ComboBox completedbycombobox;


#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

