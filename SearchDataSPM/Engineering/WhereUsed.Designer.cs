namespace SearchDataSPM
{
    partial class WhereUsed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhereUsed));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDrawingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getBOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelTooltips = new System.Windows.Forms.ToolTip(this.components);
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.Expandchk = new System.Windows.Forms.CheckBox();
            this.Assy_txtbox = new System.Windows.Forms.TextBox();
            this.Descriptiontxtbox = new System.Windows.Forms.TextBox();
            this.oemitemtxtbox = new System.Windows.Forms.TextBox();
            this.oemtxtbox = new System.Windows.Forms.TextBox();
            this.SPM = new System.Windows.Forms.Label();
            this.familytxtbox = new System.Windows.Forms.TextBox();
            this.sparetxtbox = new System.Windows.Forms.TextBox();
            this.ItemTxtBox = new System.Windows.Forms.TextBox();
            this.qtytxtbox = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Assy_label = new System.Windows.Forms.Label();
            this.oemlbl = new System.Windows.Forms.Label();
            this.oemitemlbl = new System.Windows.Forms.Label();
            this.SpareLbl = new System.Windows.Forms.Label();
            this.descriptionlbl = new System.Windows.Forms.Label();
            this.Itemlbl = new System.Windows.Forms.Label();
            this.familylbl = new System.Windows.Forms.Label();
            this.qtylbl = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.foundlabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openModelToolStripMenuItem,
            this.openDrawingToolStripMenuItem,
            this.getBOMToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // openModelToolStripMenuItem
            // 
            this.openModelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openModelToolStripMenuItem.Image")));
            this.openModelToolStripMenuItem.Name = "openModelToolStripMenuItem";
            this.openModelToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openModelToolStripMenuItem.Text = "Open Model";
            this.openModelToolStripMenuItem.ToolTipText = "Open Selected Item Model";
            this.openModelToolStripMenuItem.Click += new System.EventHandler(this.openModelToolStripMenuItem_Click);
            // 
            // openDrawingToolStripMenuItem
            // 
            this.openDrawingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openDrawingToolStripMenuItem.Image")));
            this.openDrawingToolStripMenuItem.Name = "openDrawingToolStripMenuItem";
            this.openDrawingToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openDrawingToolStripMenuItem.Text = "Open Drawing";
            this.openDrawingToolStripMenuItem.ToolTipText = "Opens Selected Item Drawing";
            this.openDrawingToolStripMenuItem.Click += new System.EventHandler(this.openDrawingToolStripMenuItem_Click);
            // 
            // getBOMToolStripMenuItem
            // 
            this.getBOMToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("getBOMToolStripMenuItem.Image")));
            this.getBOMToolStripMenuItem.Name = "getBOMToolStripMenuItem";
            this.getBOMToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.getBOMToolStripMenuItem.Text = "Get BOM";
            this.getBOMToolStripMenuItem.ToolTipText = "Get Selected Assy\'s BOM";
            this.getBOMToolStripMenuItem.Click += new System.EventHandler(this.getBOMToolStripMenuItem_Click);
            // 
            // LabelTooltips
            // 
            this.LabelTooltips.AutoPopDelay = 4000;
            this.LabelTooltips.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.LabelTooltips.InitialDelay = 500;
            this.LabelTooltips.ReshowDelay = 100;
            // 
            // txtSearch
            // 
            this.txtSearch.AccessibleName = "";
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(3, 688);
            this.txtSearch.MaximumSize = new System.Drawing.Size(32767, 25);
            this.txtSearch.MinimumSize = new System.Drawing.Size(25, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(424, 26);
            this.txtSearch.TabIndex = 3;
            this.LabelTooltips.SetToolTip(this.txtSearch, "Enter Search Keyword");
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtSearch_MouseDoubleClick);
            // 
            // Expandchk
            // 
            this.Expandchk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Expandchk.AutoSize = true;
            this.Expandchk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Expandchk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expandchk.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Expandchk.Location = new System.Drawing.Point(431, 44);
            this.Expandchk.MinimumSize = new System.Drawing.Size(30, 0);
            this.Expandchk.Name = "Expandchk";
            this.Expandchk.Size = new System.Drawing.Size(49, 28);
            this.Expandchk.TabIndex = 31;
            this.Expandchk.Text = "+/-";
            this.LabelTooltips.SetToolTip(this.Expandchk, "Expand/Collapse Tree");
            this.Expandchk.UseVisualStyleBackColor = true;
            this.Expandchk.Click += new System.EventHandler(this.Expandchk_Click);
            // 
            // Assy_txtbox
            // 
            this.Assy_txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Assy_txtbox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Assy_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Assy_txtbox.Location = new System.Drawing.Point(116, 16);
            this.Assy_txtbox.MaximumSize = new System.Drawing.Size(120, 25);
            this.Assy_txtbox.MinimumSize = new System.Drawing.Size(110, 25);
            this.Assy_txtbox.Name = "Assy_txtbox";
            this.Assy_txtbox.Size = new System.Drawing.Size(120, 26);
            this.Assy_txtbox.TabIndex = 0;
            this.LabelTooltips.SetToolTip(this.Assy_txtbox, "Enter Item Number");
            this.Assy_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Assy_txtbox_KeyDown);
            this.Assy_txtbox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Assy_txtbox_MouseDoubleClick);
            // 
            // Descriptiontxtbox
            // 
            this.Descriptiontxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Descriptiontxtbox.Location = new System.Drawing.Point(566, 162);
            this.Descriptiontxtbox.MaximumSize = new System.Drawing.Size(180, 50);
            this.Descriptiontxtbox.MinimumSize = new System.Drawing.Size(180, 50);
            this.Descriptiontxtbox.Multiline = true;
            this.Descriptiontxtbox.Name = "Descriptiontxtbox";
            this.Descriptiontxtbox.ReadOnly = true;
            this.Descriptiontxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Descriptiontxtbox.Size = new System.Drawing.Size(180, 50);
            this.Descriptiontxtbox.TabIndex = 35;
            this.LabelTooltips.SetToolTip(this.Descriptiontxtbox, "Item Description");
            // 
            // oemitemtxtbox
            // 
            this.oemitemtxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemitemtxtbox.Location = new System.Drawing.Point(566, 298);
            this.oemitemtxtbox.MaximumSize = new System.Drawing.Size(180, 50);
            this.oemitemtxtbox.MinimumSize = new System.Drawing.Size(180, 50);
            this.oemitemtxtbox.Multiline = true;
            this.oemitemtxtbox.Name = "oemitemtxtbox";
            this.oemitemtxtbox.ReadOnly = true;
            this.oemitemtxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemitemtxtbox.Size = new System.Drawing.Size(180, 50);
            this.oemitemtxtbox.TabIndex = 39;
            this.LabelTooltips.SetToolTip(this.oemitemtxtbox, "Item\'s Manufacturer OEM");
            // 
            // oemtxtbox
            // 
            this.oemtxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemtxtbox.Location = new System.Drawing.Point(566, 229);
            this.oemtxtbox.MaximumSize = new System.Drawing.Size(180, 50);
            this.oemtxtbox.MinimumSize = new System.Drawing.Size(180, 50);
            this.oemtxtbox.Multiline = true;
            this.oemtxtbox.Name = "oemtxtbox";
            this.oemtxtbox.ReadOnly = true;
            this.oemtxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemtxtbox.Size = new System.Drawing.Size(180, 50);
            this.oemtxtbox.TabIndex = 37;
            this.LabelTooltips.SetToolTip(this.oemtxtbox, "Item Manufacturer");
            // 
            // SPM
            // 
            this.SPM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(491, 22);
            this.SPM.MaximumSize = new System.Drawing.Size(288, 100);
            this.SPM.MinimumSize = new System.Drawing.Size(100, 100);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(288, 100);
            this.SPM.TabIndex = 32;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelTooltips.SetToolTip(this.SPM, "SPM Automation Inc.");
            this.SPM.DoubleClick += new System.EventHandler(this.SPM_DoubleClick);
            // 
            // familytxtbox
            // 
            this.familytxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.familytxtbox.Location = new System.Drawing.Point(566, 372);
            this.familytxtbox.MaximumSize = new System.Drawing.Size(180, 20);
            this.familytxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.familytxtbox.Name = "familytxtbox";
            this.familytxtbox.ReadOnly = true;
            this.familytxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.familytxtbox.Size = new System.Drawing.Size(180, 20);
            this.familytxtbox.TabIndex = 41;
            this.LabelTooltips.SetToolTip(this.familytxtbox, "Item Family Group");
            // 
            // sparetxtbox
            // 
            this.sparetxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparetxtbox.Location = new System.Drawing.Point(566, 448);
            this.sparetxtbox.MaximumSize = new System.Drawing.Size(180, 20);
            this.sparetxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.sparetxtbox.Name = "sparetxtbox";
            this.sparetxtbox.ReadOnly = true;
            this.sparetxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sparetxtbox.Size = new System.Drawing.Size(180, 20);
            this.sparetxtbox.TabIndex = 45;
            this.LabelTooltips.SetToolTip(this.sparetxtbox, "Item Spare Property");
            // 
            // ItemTxtBox
            // 
            this.ItemTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemTxtBox.Location = new System.Drawing.Point(566, 125);
            this.ItemTxtBox.MaximumSize = new System.Drawing.Size(180, 20);
            this.ItemTxtBox.MinimumSize = new System.Drawing.Size(180, 20);
            this.ItemTxtBox.Name = "ItemTxtBox";
            this.ItemTxtBox.ReadOnly = true;
            this.ItemTxtBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemTxtBox.Size = new System.Drawing.Size(180, 20);
            this.ItemTxtBox.TabIndex = 33;
            this.LabelTooltips.SetToolTip(this.ItemTxtBox, "Item Number");
            // 
            // qtytxtbox
            // 
            this.qtytxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qtytxtbox.Location = new System.Drawing.Point(566, 410);
            this.qtytxtbox.MaximumSize = new System.Drawing.Size(180, 20);
            this.qtytxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.qtytxtbox.Name = "qtytxtbox";
            this.qtytxtbox.ReadOnly = true;
            this.qtytxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qtytxtbox.Size = new System.Drawing.Size(180, 20);
            this.qtytxtbox.TabIndex = 43;
            this.LabelTooltips.SetToolTip(this.qtytxtbox, "Item Quantities Per Assembly");
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.HotTracking = true;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 51);
            this.treeView1.MinimumSize = new System.Drawing.Size(100, 100);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(424, 615);
            this.treeView1.TabIndex = 1;
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            this.treeView1.Leave += new System.EventHandler(this.treeView1_Leave);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8_bookclose_64.png");
            this.imageList1.Images.SetKeyName(1, "icons8_bookopen_64.png");
            this.imageList1.Images.SetKeyName(2, "icons8_Pageblue_32.png");
            this.imageList1.Images.SetKeyName(3, "icons8_Electricity_64.png");
            this.imageList1.Images.SetKeyName(4, "icons8_Book_Shelf_32.png");
            this.imageList1.Images.SetKeyName(5, "icons8_Money_Bag_32.png");
            this.imageList1.Images.SetKeyName(6, "icons8_Hammer_32.png");
            this.imageList1.Images.SetKeyName(7, "icons8_Screw_32.png");
            this.imageList1.Images.SetKeyName(8, "icons8_Triggering_32.png");
            this.imageList1.Images.SetKeyName(9, "icons8_Pressure_32.png");
            this.imageList1.Images.SetKeyName(10, "icons8_Price_Tag_32.png");
            this.imageList1.Images.SetKeyName(11, "icons8_Design_32.png");
            this.imageList1.Images.SetKeyName(12, "icons8_Repository_32.png");
            // 
            // Assy_label
            // 
            this.Assy_label.AutoSize = true;
            this.Assy_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Assy_label.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Assy_label.Location = new System.Drawing.Point(12, 22);
            this.Assy_label.Name = "Assy_label";
            this.Assy_label.Size = new System.Drawing.Size(98, 15);
            this.Assy_label.TabIndex = 28;
            this.Assy_label.Text = "ItemNumber  :";
            // 
            // oemlbl
            // 
            this.oemlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemlbl.AutoSize = true;
            this.oemlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemlbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.oemlbl.Location = new System.Drawing.Point(457, 229);
            this.oemlbl.Name = "oemlbl";
            this.oemlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemlbl.Size = new System.Drawing.Size(46, 15);
            this.oemlbl.TabIndex = 38;
            this.oemlbl.Text = "OEM :";
            // 
            // oemitemlbl
            // 
            this.oemitemlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemitemlbl.AutoSize = true;
            this.oemitemlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemitemlbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.oemitemlbl.Location = new System.Drawing.Point(457, 301);
            this.oemitemlbl.Name = "oemitemlbl";
            this.oemitemlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemitemlbl.Size = new System.Drawing.Size(102, 30);
            this.oemitemlbl.TabIndex = 40;
            this.oemitemlbl.Text = "OEM \r\nItem Number : ";
            this.oemitemlbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpareLbl
            // 
            this.SpareLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpareLbl.AutoSize = true;
            this.SpareLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpareLbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SpareLbl.Location = new System.Drawing.Point(457, 451);
            this.SpareLbl.MaximumSize = new System.Drawing.Size(57, 15);
            this.SpareLbl.Name = "SpareLbl";
            this.SpareLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SpareLbl.Size = new System.Drawing.Size(57, 15);
            this.SpareLbl.TabIndex = 46;
            this.SpareLbl.Text = "Spare : ";
            // 
            // descriptionlbl
            // 
            this.descriptionlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionlbl.AutoSize = true;
            this.descriptionlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionlbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.descriptionlbl.Location = new System.Drawing.Point(457, 165);
            this.descriptionlbl.Name = "descriptionlbl";
            this.descriptionlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.descriptionlbl.Size = new System.Drawing.Size(80, 15);
            this.descriptionlbl.TabIndex = 36;
            this.descriptionlbl.Text = "Description";
            // 
            // Itemlbl
            // 
            this.Itemlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Itemlbl.AutoSize = true;
            this.Itemlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Itemlbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Itemlbl.Location = new System.Drawing.Point(461, 128);
            this.Itemlbl.Name = "Itemlbl";
            this.Itemlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Itemlbl.Size = new System.Drawing.Size(43, 15);
            this.Itemlbl.TabIndex = 34;
            this.Itemlbl.Text = "Item :";
            // 
            // familylbl
            // 
            this.familylbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.familylbl.AutoSize = true;
            this.familylbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.familylbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.familylbl.Location = new System.Drawing.Point(457, 375);
            this.familylbl.Name = "familylbl";
            this.familylbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.familylbl.Size = new System.Drawing.Size(61, 15);
            this.familylbl.TabIndex = 42;
            this.familylbl.Text = "Family : ";
            // 
            // qtylbl
            // 
            this.qtylbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qtylbl.AutoSize = true;
            this.qtylbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtylbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.qtylbl.Location = new System.Drawing.Point(457, 413);
            this.qtylbl.Name = "qtylbl";
            this.qtylbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qtylbl.Size = new System.Drawing.Size(78, 15);
            this.qtylbl.TabIndex = 44;
            this.qtylbl.Text = "Qty/Assy\'s :";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(433, 542);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(348, 170);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_ItemDrag);
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            this.listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_KeyDown);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList.TransparentColor = System.Drawing.Color.Black;
            // 
            // foundlabel
            // 
            this.foundlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foundlabel.AutoSize = true;
            this.foundlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foundlabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.foundlabel.Location = new System.Drawing.Point(5, 669);
            this.foundlabel.Name = "foundlabel";
            this.foundlabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.foundlabel.Size = new System.Drawing.Size(49, 15);
            this.foundlabel.TabIndex = 47;
            this.foundlabel.Text = "Search:";
            // 
            // WhereUsed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(206)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(784, 716);
            this.Controls.Add(this.foundlabel);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.Descriptiontxtbox);
            this.Controls.Add(this.oemlbl);
            this.Controls.Add(this.oemitemtxtbox);
            this.Controls.Add(this.oemtxtbox);
            this.Controls.Add(this.SPM);
            this.Controls.Add(this.oemitemlbl);
            this.Controls.Add(this.SpareLbl);
            this.Controls.Add(this.descriptionlbl);
            this.Controls.Add(this.familytxtbox);
            this.Controls.Add(this.sparetxtbox);
            this.Controls.Add(this.Itemlbl);
            this.Controls.Add(this.familylbl);
            this.Controls.Add(this.ItemTxtBox);
            this.Controls.Add(this.qtylbl);
            this.Controls.Add(this.qtytxtbox);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.Expandchk);
            this.Controls.Add(this.Assy_label);
            this.Controls.Add(this.Assy_txtbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 700);
            this.Name = "WhereUsed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Where Used - SPM Connect";
            this.Load += new System.EventHandler(this.ParentView_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDrawingToolStripMenuItem;
        private System.Windows.Forms.ToolTip LabelTooltips;
        private System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox Expandchk;
        private System.Windows.Forms.Label Assy_label;
        private System.Windows.Forms.TextBox Assy_txtbox;
        private System.Windows.Forms.TextBox Descriptiontxtbox;
        private System.Windows.Forms.Label oemlbl;
        private System.Windows.Forms.TextBox oemitemtxtbox;
        private System.Windows.Forms.TextBox oemtxtbox;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Label oemitemlbl;
        private System.Windows.Forms.Label SpareLbl;
        private System.Windows.Forms.Label descriptionlbl;
        private System.Windows.Forms.TextBox familytxtbox;
        private System.Windows.Forms.TextBox sparetxtbox;
        private System.Windows.Forms.Label Itemlbl;
        private System.Windows.Forms.Label familylbl;
        private System.Windows.Forms.TextBox ItemTxtBox;
        private System.Windows.Forms.Label qtylbl;
        private System.Windows.Forms.TextBox qtytxtbox;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem getBOMToolStripMenuItem;
        private System.Windows.Forms.Label foundlabel;
        private System.Windows.Forms.ImageList imageList1;
    }
}