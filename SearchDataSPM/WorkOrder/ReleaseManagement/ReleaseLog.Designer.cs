using System;

namespace SearchDataSPM.WorkOrder.ReleaseManagement
{
    partial class ReleaseLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseLog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Reload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.advsearchbttn = new System.Windows.Forms.Button();
            this.Prioritycombox = new System.Windows.Forms.ComboBox();
            this.CheckedBycomboxbox = new System.Windows.Forms.ComboBox();
            this.ReleasedBycombox = new System.Windows.Forms.ComboBox();
            this.ApprovedBycombobox = new System.Windows.Forms.ComboBox();
            this.LastSavedcombobox = new System.Windows.Forms.ComboBox();
            this.Createdbycomboxbox = new System.Windows.Forms.ComboBox();
            this.ByStatuscomboBox = new System.Windows.Forms.ComboBox();
            this.SPM = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.recordlabel = new System.Windows.Forms.Label();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewReleaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCurrentJobReleaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getBOMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // Reload
            // 
            this.Reload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.Location = new System.Drawing.Point(612, 12);
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
            this.txtSearch.Location = new System.Drawing.Point(209, 14);
            this.txtSearch.MaximumSize = new System.Drawing.Size(32767, 25);
            this.txtSearch.MinimumSize = new System.Drawing.Size(4, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(398, 25);
            this.txtSearch.TabIndex = 106;
            this.TreeViewToolTip.SetToolTip(this.txtSearch, "Enter Search Keyword.\r\n");
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // filter4
            // 
            this.filter4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filter4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filter4.Location = new System.Drawing.Point(675, 52);
            this.filter4.MaximumSize = new System.Drawing.Size(150, 26);
            this.filter4.MinimumSize = new System.Drawing.Size(150, 25);
            this.filter4.Name = "filter4";
            this.filter4.Size = new System.Drawing.Size(150, 26);
            this.filter4.TabIndex = 110;
            this.TreeViewToolTip.SetToolTip(this.filter4, "Enter Keyword 5");
            this.filter4.Visible = false;
            this.filter4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filter4_KeyDown);
            // 
            // filteroemitem_txtbox
            // 
            this.filteroemitem_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filteroemitem_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filteroemitem_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteroemitem_txtbox.Location = new System.Drawing.Point(520, 52);
            this.filteroemitem_txtbox.MaximumSize = new System.Drawing.Size(150, 26);
            this.filteroemitem_txtbox.MinimumSize = new System.Drawing.Size(150, 25);
            this.filteroemitem_txtbox.Name = "filteroemitem_txtbox";
            this.filteroemitem_txtbox.Size = new System.Drawing.Size(150, 26);
            this.filteroemitem_txtbox.TabIndex = 109;
            this.TreeViewToolTip.SetToolTip(this.filteroemitem_txtbox, "Enter keyworkd 4");
            this.filteroemitem_txtbox.Visible = false;
            this.filteroemitem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filteroemitem_txtbox_KeyDown);
            // 
            // filteroem_txtbox
            // 
            this.filteroem_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filteroem_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filteroem_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filteroem_txtbox.Location = new System.Drawing.Point(365, 52);
            this.filteroem_txtbox.MaximumSize = new System.Drawing.Size(150, 26);
            this.filteroem_txtbox.MinimumSize = new System.Drawing.Size(150, 26);
            this.filteroem_txtbox.Name = "filteroem_txtbox";
            this.filteroem_txtbox.Size = new System.Drawing.Size(150, 26);
            this.filteroem_txtbox.TabIndex = 108;
            this.TreeViewToolTip.SetToolTip(this.filteroem_txtbox, "Enter Keyword 3");
            this.filteroem_txtbox.Visible = false;
            this.filteroem_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filteroem_txtbox_KeyDown);
            // 
            // Descrip_txtbox
            // 
            this.Descrip_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Descrip_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Descrip_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descrip_txtbox.Location = new System.Drawing.Point(209, 52);
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
            this.advsearchbttn.Location = new System.Drawing.Point(826, 53);
            this.advsearchbttn.MaximumSize = new System.Drawing.Size(35, 25);
            this.advsearchbttn.MinimumSize = new System.Drawing.Size(35, 25);
            this.advsearchbttn.Name = "advsearchbttn";
            this.advsearchbttn.Size = new System.Drawing.Size(35, 25);
            this.advsearchbttn.TabIndex = 121;
            this.advsearchbttn.Text = ">>";
            this.TreeViewToolTip.SetToolTip(this.advsearchbttn, "Show Advance Filters");
            this.advsearchbttn.UseVisualStyleBackColor = true;
            this.advsearchbttn.Click += new System.EventHandler(this.Advsearchbttn_Click);
            // 
            // Prioritycombox
            // 
            this.Prioritycombox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Prioritycombox.AutoCompleteCustomSource.AddRange(new string[] {
            "Normal",
            "High"});
            this.Prioritycombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Prioritycombox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Prioritycombox.FormattingEnabled = true;
            this.Prioritycombox.Items.AddRange(new object[] {
            "Normal",
            "Urgent"});
            this.Prioritycombox.Location = new System.Drawing.Point(17, 523);
            this.Prioritycombox.Name = "Prioritycombox";
            this.Prioritycombox.Size = new System.Drawing.Size(147, 21);
            this.Prioritycombox.TabIndex = 125;
            this.TreeViewToolTip.SetToolTip(this.Prioritycombox, "Filter by priority");
            this.Prioritycombox.SelectedIndexChanged += new System.EventHandler(this.LastSavedcombobox_SelectedIndexChanged);
            this.Prioritycombox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lastsavedbycombo_KeyDown);
            // 
            // CheckedBycomboxbox
            // 
            this.CheckedBycomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckedBycomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.CheckedBycomboxbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CheckedBycomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CheckedBycomboxbox.FormattingEnabled = true;
            this.CheckedBycomboxbox.Location = new System.Drawing.Point(17, 230);
            this.CheckedBycomboxbox.Name = "CheckedBycomboxbox";
            this.CheckedBycomboxbox.Size = new System.Drawing.Size(147, 21);
            this.CheckedBycomboxbox.TabIndex = 123;
            this.TreeViewToolTip.SetToolTip(this.CheckedBycomboxbox, "Filter checked by");
            this.CheckedBycomboxbox.SelectedIndexChanged += new System.EventHandler(this.LastSavedcombobox_SelectedIndexChanged);
            this.CheckedBycomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lastsavedbycombo_KeyDown);
            // 
            // ReleasedBycombox
            // 
            this.ReleasedBycombox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ReleasedBycombox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ReleasedBycombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReleasedBycombox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ReleasedBycombox.FormattingEnabled = true;
            this.ReleasedBycombox.Location = new System.Drawing.Point(17, 379);
            this.ReleasedBycombox.Name = "ReleasedBycombox";
            this.ReleasedBycombox.Size = new System.Drawing.Size(147, 21);
            this.ReleasedBycombox.TabIndex = 127;
            this.TreeViewToolTip.SetToolTip(this.ReleasedBycombox, "Filter released by");
            this.ReleasedBycombox.SelectedIndexChanged += new System.EventHandler(this.LastSavedcombobox_SelectedIndexChanged);
            this.ReleasedBycombox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lastsavedbycombo_KeyDown);
            // 
            // ApprovedBycombobox
            // 
            this.ApprovedBycombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ApprovedBycombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ApprovedBycombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ApprovedBycombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ApprovedBycombobox.FormattingEnabled = true;
            this.ApprovedBycombobox.Items.AddRange(new object[] {
            "Festo",
            "SPM AUTOMATION (Canada) INC."});
            this.ApprovedBycombobox.Location = new System.Drawing.Point(17, 304);
            this.ApprovedBycombobox.Name = "ApprovedBycombobox";
            this.ApprovedBycombobox.Size = new System.Drawing.Size(147, 21);
            this.ApprovedBycombobox.TabIndex = 124;
            this.TreeViewToolTip.SetToolTip(this.ApprovedBycombobox, "Filter approved by");
            this.ApprovedBycombobox.SelectedIndexChanged += new System.EventHandler(this.LastSavedcombobox_SelectedIndexChanged);
            this.ApprovedBycombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lastsavedbycombo_KeyDown);
            // 
            // LastSavedcombobox
            // 
            this.LastSavedcombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LastSavedcombobox.AutoCompleteCustomSource.AddRange(new string[] {
            "0 - Vendor",
            "1 - Customer"});
            this.LastSavedcombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.LastSavedcombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LastSavedcombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LastSavedcombobox.FormattingEnabled = true;
            this.LastSavedcombobox.Location = new System.Drawing.Point(17, 594);
            this.LastSavedcombobox.Name = "LastSavedcombobox";
            this.LastSavedcombobox.Size = new System.Drawing.Size(147, 21);
            this.LastSavedcombobox.TabIndex = 128;
            this.TreeViewToolTip.SetToolTip(this.LastSavedcombobox, "Filter last saved by");
            this.LastSavedcombobox.SelectedIndexChanged += new System.EventHandler(this.LastSavedcombobox_SelectedIndexChanged);
            this.LastSavedcombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lastsavedbycombo_KeyDown);
            // 
            // Createdbycomboxbox
            // 
            this.Createdbycomboxbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Createdbycomboxbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Createdbycomboxbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Createdbycomboxbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Createdbycomboxbox.FormattingEnabled = true;
            this.Createdbycomboxbox.Items.AddRange(new object[] {
            "Shailkumar Patel",
            "Scott Reid",
            "Joel Goldsmith"});
            this.Createdbycomboxbox.Location = new System.Drawing.Point(17, 153);
            this.Createdbycomboxbox.Name = "Createdbycomboxbox";
            this.Createdbycomboxbox.Size = new System.Drawing.Size(147, 21);
            this.Createdbycomboxbox.TabIndex = 122;
            this.TreeViewToolTip.SetToolTip(this.Createdbycomboxbox, "Filter created by");
            this.Createdbycomboxbox.SelectedIndexChanged += new System.EventHandler(this.LastSavedcombobox_SelectedIndexChanged);
            this.Createdbycomboxbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lastsavedbycombo_KeyDown);
            // 
            // ByStatuscomboBox
            // 
            this.ByStatuscomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ByStatuscomboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Not Submitted",
            "For Checking",
            "For Approval",
            "For Release",
            "Inactive"});
            this.ByStatuscomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ByStatuscomboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ByStatuscomboBox.FormattingEnabled = true;
            this.ByStatuscomboBox.Items.AddRange(new object[] {
            "Not Submitted",
            "For Checking",
            "For Approval",
            "For Release",
            "InActive"});
            this.ByStatuscomboBox.Location = new System.Drawing.Point(17, 449);
            this.ByStatuscomboBox.Name = "ByStatuscomboBox";
            this.ByStatuscomboBox.Size = new System.Drawing.Size(147, 21);
            this.ByStatuscomboBox.TabIndex = 126;
            this.TreeViewToolTip.SetToolTip(this.ByStatuscomboBox, "Filter by status");
            this.ByStatuscomboBox.SelectedIndexChanged += new System.EventHandler(this.LastSavedcombobox_SelectedIndexChanged);
            this.ByStatuscomboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lastsavedbycombo_KeyDown);
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
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(750, 600);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(80)))));
            this.splitContainer1.Panel1.Controls.Add(this.advsearchbttn);
            this.splitContainer1.Panel1.Controls.Add(this.SPM);
            this.splitContainer1.Panel1.Controls.Add(this.recordlabel);
            this.splitContainer1.Panel1.Controls.Add(this.DataGridView);
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
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.splitContainer1.Panel2.Controls.Add(this.matlbl);
            this.splitContainer1.Panel2.Controls.Add(this.ByStatuscomboBox);
            this.splitContainer1.Panel2.Controls.Add(this.clrfiltersbttn);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.actcadblk);
            this.splitContainer1.Panel2.Controls.Add(this.Prioritycombox);
            this.splitContainer1.Panel2.Controls.Add(this.CheckedBycomboxbox);
            this.splitContainer1.Panel2.Controls.Add(this.ReleasedBycombox);
            this.splitContainer1.Panel2.Controls.Add(this.ApprovedBycombobox);
            this.splitContainer1.Panel2.Controls.Add(this.LastSavedcombobox);
            this.splitContainer1.Panel2.Controls.Add(this.Createdbycomboxbox);
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
            this.recordlabel.ForeColor = System.Drawing.Color.White;
            this.recordlabel.Location = new System.Drawing.Point(657, 80);
            this.recordlabel.Name = "recordlabel";
            this.recordlabel.Size = new System.Drawing.Size(0, 14);
            this.recordlabel.TabIndex = 119;
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.AllowUserToOrderColumns = true;
            this.DataGridView.AllowUserToResizeRows = false;
            this.DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(48)))), ((int)(((byte)(80)))));
            this.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.DataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.DataGridView.Location = new System.Drawing.Point(1, 98);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.Size = new System.Drawing.Size(863, 610);
            this.DataGridView.StandardTab = true;
            this.DataGridView.TabIndex = 111;
            this.DataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            this.DataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView_CellFormatting);
            this.DataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
            this.DataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellMouseLeave);
            this.DataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseMove);
            this.DataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridView_CellPainting_1);
            this.DataGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            this.DataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewReleaseToolStripMenuItem,
            this.viewCurrentJobReleaseToolStripMenuItem,
            this.getBOMToolStripMenuItem1,
            this.openModelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // viewReleaseToolStripMenuItem
            // 
            this.viewReleaseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewReleaseToolStripMenuItem.Image")));
            this.viewReleaseToolStripMenuItem.Name = "viewReleaseToolStripMenuItem";
            this.viewReleaseToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.viewReleaseToolStripMenuItem.Text = "View Selected Release";
            this.viewReleaseToolStripMenuItem.Click += new System.EventHandler(this.ViewReleaseToolStripMenuItem_Click);
            // 
            // viewCurrentJobReleaseToolStripMenuItem
            // 
            this.viewCurrentJobReleaseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewCurrentJobReleaseToolStripMenuItem.Image")));
            this.viewCurrentJobReleaseToolStripMenuItem.Name = "viewCurrentJobReleaseToolStripMenuItem";
            this.viewCurrentJobReleaseToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.viewCurrentJobReleaseToolStripMenuItem.Text = "View Current Job Releases";
            this.viewCurrentJobReleaseToolStripMenuItem.Click += new System.EventHandler(this.ViewCurrentJobReleaseToolStripMenuItem_Click);
            // 
            // getBOMToolStripMenuItem1
            // 
            this.getBOMToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("getBOMToolStripMenuItem1.Image")));
            this.getBOMToolStripMenuItem1.Name = "getBOMToolStripMenuItem1";
            this.getBOMToolStripMenuItem1.Size = new System.Drawing.Size(210, 22);
            this.getBOMToolStripMenuItem1.Text = "Get BOM";
            this.getBOMToolStripMenuItem1.Click += new System.EventHandler(this.GetBOMToolStripMenuItem1_Click);
            // 
            // openModelToolStripMenuItem
            // 
            this.openModelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openModelToolStripMenuItem.Image")));
            this.openModelToolStripMenuItem.Name = "openModelToolStripMenuItem";
            this.openModelToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.openModelToolStripMenuItem.Text = "Open Model";
            this.openModelToolStripMenuItem.ToolTipText = "Open Selected Item Model";
            this.openModelToolStripMenuItem.Click += new System.EventHandler(this.OpenModelToolStripMenuItem_Click);
            // 
            // matlbl
            // 
            this.matlbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.matlbl.AutoSize = true;
            this.matlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matlbl.ForeColor = System.Drawing.Color.Black;
            this.matlbl.Location = new System.Drawing.Point(21, 428);
            this.matlbl.Name = "matlbl";
            this.matlbl.Size = new System.Drawing.Size(71, 18);
            this.matlbl.TabIndex = 147;
            this.matlbl.Text = "By Status";
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
            this.clrfiltersbttn.Click += new System.EventHandler(this.Clrfiltersbttn_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(21, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.TabIndex = 145;
            this.label6.Text = "Created By";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(18, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 18);
            this.label5.TabIndex = 144;
            this.label5.Text = "Released By";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(24, 502);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 18);
            this.label4.TabIndex = 143;
            this.label4.Text = "Priority";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 18);
            this.label3.TabIndex = 142;
            this.label3.Text = "Approved By";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(21, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 18);
            this.label2.TabIndex = 141;
            this.label2.Text = "Checked By";
            // 
            // actcadblk
            // 
            this.actcadblk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actcadblk.AutoSize = true;
            this.actcadblk.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actcadblk.ForeColor = System.Drawing.Color.Black;
            this.actcadblk.Location = new System.Drawing.Point(24, 573);
            this.actcadblk.Name = "actcadblk";
            this.actcadblk.Size = new System.Drawing.Size(102, 18);
            this.actcadblk.TabIndex = 140;
            this.actcadblk.Text = "Last Saved By";
            // 
            // ReleaseLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1044, 711);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "ReleaseLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Release Logs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SPM_Connect_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_Connect_FormClosed);
            this.Load += new System.EventHandler(this.SPM_Connect_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button advsearchbttn;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Button Reload;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox filter4;
        private System.Windows.Forms.TextBox filteroemitem_txtbox;
        private System.Windows.Forms.TextBox filteroem_txtbox;
        private System.Windows.Forms.TextBox Descrip_txtbox;
        private System.Windows.Forms.ComboBox Createdbycomboxbox;
        private System.Windows.Forms.ComboBox Prioritycombox;
        private System.Windows.Forms.ComboBox CheckedBycomboxbox;
        private System.Windows.Forms.ComboBox ReleasedBycombox;
        private System.Windows.Forms.ComboBox ApprovedBycombobox;
        private System.Windows.Forms.ComboBox LastSavedcombobox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label actcadblk;
        private System.Windows.Forms.Button clrfiltersbttn;
        public System.Windows.Forms.DataGridView DataGridView;
        public System.Windows.Forms.Label recordlabel;
        private System.Windows.Forms.Label matlbl;
        private System.Windows.Forms.ComboBox ByStatuscomboBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewReleaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCurrentJobReleaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getBOMToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openModelToolStripMenuItem;


#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SPM_Connect.txtSearch'
    }
}

