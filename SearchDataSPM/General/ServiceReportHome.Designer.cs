using System;

namespace SearchDataSPM
{
    partial class ServiceReportHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceReportHome));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.versionlabel = new System.Windows.Forms.Label();
            this.Reload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.advsearchbttn = new System.Windows.Forms.Button();
            this.technamecomboxbox = new System.Windows.Forms.ComboBox();
            this.customercomboxbox = new System.Windows.Forms.ComboBox();
            this.authorizedbycombo = new System.Windows.Forms.ComboBox();
            this.projectmangercombobox = new System.Windows.Forms.ComboBox();
            this.jobnumbercombobox = new System.Windows.Forms.ComboBox();
            this.equipmentcomboBox = new System.Windows.Forms.ComboBox();
            this.refjobcombobox = new System.Windows.Forms.ComboBox();
            this.SPM = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.recordlabel = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ContextMenuStripShipping = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.invoiceinfostripmenu = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Reload.Location = new System.Drawing.Point(675, 20);
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
            this.txtSearch.Size = new System.Drawing.Size(448, 25);
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
            // technamecomboxbox
            // 
            this.technamecomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.technamecomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.technamecomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.technamecomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.technamecomboxbox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.technamecomboxbox.FormattingEnabled = true;
            this.technamecomboxbox.Location = new System.Drawing.Point(8, 379);
            this.technamecomboxbox.Name = "technamecomboxbox";
            this.technamecomboxbox.Size = new System.Drawing.Size(161, 22);
            this.technamecomboxbox.TabIndex = 125;
            this.TreeViewToolTip.SetToolTip(this.technamecomboxbox, "Filter by Technician");
            this.technamecomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Manufactureritemcomboxbox_KeyDown);
            this.technamecomboxbox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.requestedbycomboxbox_PreviewKeyDown);
            // 
            // customercomboxbox
            // 
            this.customercomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customercomboxbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.customercomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.customercomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.customercomboxbox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customercomboxbox.FormattingEnabled = true;
            this.customercomboxbox.Location = new System.Drawing.Point(8, 230);
            this.customercomboxbox.Name = "customercomboxbox";
            this.customercomboxbox.Size = new System.Drawing.Size(161, 22);
            this.customercomboxbox.TabIndex = 123;
            this.TreeViewToolTip.SetToolTip(this.customercomboxbox, "Filter by Customer");
            this.customercomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.familycomboxbox_KeyDown);
            this.customercomboxbox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.deptrequestedcomboxbox_PreviewKeyDown);
            // 
            // authorizedbycombo
            // 
            this.authorizedbycombo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorizedbycombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.authorizedbycombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.authorizedbycombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.authorizedbycombo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authorizedbycombo.FormattingEnabled = true;
            this.authorizedbycombo.Location = new System.Drawing.Point(8, 520);
            this.authorizedbycombo.Name = "authorizedbycombo";
            this.authorizedbycombo.Size = new System.Drawing.Size(161, 22);
            this.authorizedbycombo.TabIndex = 127;
            this.TreeViewToolTip.SetToolTip(this.authorizedbycombo, "Filter by Authorized By");
            this.authorizedbycombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lastsavedbycombo_KeyDown);
            this.authorizedbycombo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.approvedbycombo_PreviewKeyDown);
            // 
            // projectmangercombobox
            // 
            this.projectmangercombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectmangercombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.projectmangercombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.projectmangercombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.projectmangercombobox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectmangercombobox.FormattingEnabled = true;
            this.projectmangercombobox.Items.AddRange(new object[] {
            "Festo",
            "SPM AUTOMATION (Canada) INC."});
            this.projectmangercombobox.Location = new System.Drawing.Point(8, 304);
            this.projectmangercombobox.Name = "projectmangercombobox";
            this.projectmangercombobox.Size = new System.Drawing.Size(161, 22);
            this.projectmangercombobox.TabIndex = 124;
            this.TreeViewToolTip.SetToolTip(this.projectmangercombobox, "Filter by Project Manager");
            this.projectmangercombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oemitemcombobox_KeyDown);
            this.projectmangercombobox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ecrstatuscombobox_PreviewKeyDown);
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
            this.jobnumbercombobox.Size = new System.Drawing.Size(161, 22);
            this.jobnumbercombobox.TabIndex = 122;
            this.TreeViewToolTip.SetToolTip(this.jobnumbercombobox, "Filter by Job Number");
            this.jobnumbercombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.designedbycombobox_KeyDown);
            this.jobnumbercombobox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.jobnumbercombobox_PreviewKeyDown);
            // 
            // equipmentcomboBox
            // 
            this.equipmentcomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.equipmentcomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.equipmentcomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.equipmentcomboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.equipmentcomboBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equipmentcomboBox.FormattingEnabled = true;
            this.equipmentcomboBox.Location = new System.Drawing.Point(8, 450);
            this.equipmentcomboBox.Name = "equipmentcomboBox";
            this.equipmentcomboBox.Size = new System.Drawing.Size(161, 22);
            this.equipmentcomboBox.TabIndex = 126;
            this.TreeViewToolTip.SetToolTip(this.equipmentcomboBox, "Filter by Equipment");
            this.equipmentcomboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaterialcomboBox_KeyDown);
            this.equipmentcomboBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.supervicsorcomboBox_PreviewKeyDown);
            // 
            // refjobcombobox
            // 
            this.refjobcombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refjobcombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.refjobcombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.refjobcombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.refjobcombobox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refjobcombobox.FormattingEnabled = true;
            this.refjobcombobox.Location = new System.Drawing.Point(8, 594);
            this.refjobcombobox.Name = "refjobcombobox";
            this.refjobcombobox.Size = new System.Drawing.Size(161, 22);
            this.refjobcombobox.TabIndex = 148;
            this.TreeViewToolTip.SetToolTip(this.refjobcombobox, "Filter by Ref. Job");
            this.refjobcombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActiveCadblockcombobox_KeyDown);
            this.refjobcombobox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.completedbycombobox_PreviewKeyDown);
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.RosyBrown;
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
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.SkyBlue;
            this.splitContainer1.Panel2.Controls.Add(this.refjobcombobox);
            this.splitContainer1.Panel2.Controls.Add(this.matlbl);
            this.splitContainer1.Panel2.Controls.Add(this.equipmentcomboBox);
            this.splitContainer1.Panel2.Controls.Add(this.clrfiltersbttn);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.actcadblk);
            this.splitContainer1.Panel2.Controls.Add(this.technamecomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.customercomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.authorizedbycombo);
            this.splitContainer1.Panel2.Controls.Add(this.projectmangercombobox);
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
            this.dataGridView.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ContextMenuStrip = this.ContextMenuStripShipping;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.GridColor = System.Drawing.Color.White;
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
            // ContextMenuStripShipping
            // 
            this.ContextMenuStripShipping.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoiceinfostripmenu});
            this.ContextMenuStripShipping.Name = "contextMenuStrip1";
            this.ContextMenuStripShipping.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ContextMenuStripShipping.Size = new System.Drawing.Size(178, 26);
            this.ContextMenuStripShipping.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripShipping_Opening);
            // 
            // invoiceinfostripmenu
            // 
            this.invoiceinfostripmenu.Image = ((System.Drawing.Image)(resources.GetObject("invoiceinfostripmenu.Image")));
            this.invoiceinfostripmenu.Name = "invoiceinfostripmenu";
            this.invoiceinfostripmenu.Size = new System.Drawing.Size(180, 22);
            this.invoiceinfostripmenu.Text = "View Service Report";
            this.invoiceinfostripmenu.ToolTipText = "View Service Report";
            this.invoiceinfostripmenu.Click += new System.EventHandler(this.invoiceinfostripmenu_Click);
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
            this.matlbl.Size = new System.Drawing.Size(64, 18);
            this.matlbl.TabIndex = 147;
            this.matlbl.Text = "Ref. Job";
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
            this.label6.Size = new System.Drawing.Size(123, 18);
            this.label6.TabIndex = 145;
            this.label6.Text = "Technician Name";
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
            this.label5.Size = new System.Drawing.Size(78, 18);
            this.label5.TabIndex = 144;
            this.label5.Text = "Equipment";
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
            this.label4.Size = new System.Drawing.Size(74, 18);
            this.label4.TabIndex = 143;
            this.label4.Text = "Customer";
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
            this.label3.Size = new System.Drawing.Size(117, 18);
            this.label3.TabIndex = 142;
            this.label3.Text = "Project Manager";
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
            this.actcadblk.Size = new System.Drawing.Size(99, 18);
            this.actcadblk.TabIndex = 140;
            this.actcadblk.Text = "Authorized By";
            // 
            // ServiceReportHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RosyBrown;
            this.ClientSize = new System.Drawing.Size(1044, 711);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "ServiceReportHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Service Report";
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
        private System.Windows.Forms.ComboBox technamecomboxbox;
        private System.Windows.Forms.ComboBox customercomboxbox;
        private System.Windows.Forms.ComboBox authorizedbycombo;
        private System.Windows.Forms.ComboBox projectmangercombobox;
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
        private System.Windows.Forms.ComboBox equipmentcomboBox;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStripShipping;
        private System.Windows.Forms.ToolStripMenuItem invoiceinfostripmenu;
        private System.Windows.Forms.ComboBox refjobcombobox;


#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

