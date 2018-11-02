namespace SearchDataSPM
{
    partial class PurchaseReqform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseReqform));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.NewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewPurchaseReqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PurchaseReqSearchTxt = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ecitbttn = new System.Windows.Forms.Button();
            this.editbttn = new System.Windows.Forms.Button();
            this.savebttn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PreviewTabPage = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.Addnewbttn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.oemitemnotxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.qtytxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pricetxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.oemtxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Descriptiontxtbox = new System.Windows.Forms.TextBox();
            this.Itemlbl = new System.Windows.Forms.Label();
            this.ItemTxtBox = new System.Windows.Forms.TextBox();
            this.itemsearchtxtbox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Preqlbl = new System.Windows.Forms.Label();
            this.purchreqtxt = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OEMItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.PreviewTabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(49, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip2";
            // 
            // NewItem
            // 
            this.NewItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewPurchaseReqToolStripMenuItem});
            this.NewItem.Name = "NewItem";
            this.NewItem.Size = new System.Drawing.Size(37, 20);
            this.NewItem.Text = "File";
            // 
            // createNewPurchaseReqToolStripMenuItem
            // 
            this.createNewPurchaseReqToolStripMenuItem.Name = "createNewPurchaseReqToolStripMenuItem";
            this.createNewPurchaseReqToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.createNewPurchaseReqToolStripMenuItem.Text = "Create new Purchase Req";
            this.createNewPurchaseReqToolStripMenuItem.Click += new System.EventHandler(this.createNewPurchaseReqToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PurchaseReqSearchTxt);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer1.Panel2.Controls.Add(this.ecitbttn);
            this.splitContainer1.Panel2.Controls.Add(this.editbttn);
            this.splitContainer1.Panel2.Controls.Add(this.savebttn);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2MinSize = 500;
            this.splitContainer1.Size = new System.Drawing.Size(984, 687);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 2;
            // 
            // PurchaseReqSearchTxt
            // 
            this.PurchaseReqSearchTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PurchaseReqSearchTxt.Location = new System.Drawing.Point(0, 0);
            this.PurchaseReqSearchTxt.Name = "PurchaseReqSearchTxt";
            this.PurchaseReqSearchTxt.Size = new System.Drawing.Size(300, 20);
            this.PurchaseReqSearchTxt.TabIndex = 8;
            this.PurchaseReqSearchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PurchaseReqSearchTxt_KeyDown);
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
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dataGridView.Location = new System.Drawing.Point(0, 22);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(300, 665);
            this.dataGridView.TabIndex = 7;
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // ecitbttn
            // 
            this.ecitbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ecitbttn.Font = new System.Drawing.Font("Magneto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecitbttn.Location = new System.Drawing.Point(283, 653);
            this.ecitbttn.MaximumSize = new System.Drawing.Size(100, 25);
            this.ecitbttn.MinimumSize = new System.Drawing.Size(100, 25);
            this.ecitbttn.Name = "ecitbttn";
            this.ecitbttn.Size = new System.Drawing.Size(100, 25);
            this.ecitbttn.TabIndex = 74;
            this.ecitbttn.Text = "Close";
            this.TreeViewToolTip.SetToolTip(this.ecitbttn, "Close this Purchase Req");
            this.ecitbttn.UseVisualStyleBackColor = true;
            this.ecitbttn.Visible = false;
            this.ecitbttn.Click += new System.EventHandler(this.ecitbttn_Click);
            // 
            // editbttn
            // 
            this.editbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editbttn.Font = new System.Drawing.Font("Magneto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editbttn.Location = new System.Drawing.Point(18, 653);
            this.editbttn.MaximumSize = new System.Drawing.Size(100, 25);
            this.editbttn.MinimumSize = new System.Drawing.Size(100, 25);
            this.editbttn.Name = "editbttn";
            this.editbttn.Size = new System.Drawing.Size(100, 25);
            this.editbttn.TabIndex = 73;
            this.editbttn.Text = "Edit";
            this.TreeViewToolTip.SetToolTip(this.editbttn, "Edit Selected Purchase Req");
            this.editbttn.UseVisualStyleBackColor = true;
            this.editbttn.Visible = false;
            this.editbttn.Click += new System.EventHandler(this.editbttn_Click);
            // 
            // savebttn
            // 
            this.savebttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.savebttn.Font = new System.Drawing.Font("Magneto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebttn.Location = new System.Drawing.Point(150, 653);
            this.savebttn.MaximumSize = new System.Drawing.Size(100, 25);
            this.savebttn.MinimumSize = new System.Drawing.Size(100, 25);
            this.savebttn.Name = "savebttn";
            this.savebttn.Size = new System.Drawing.Size(100, 25);
            this.savebttn.TabIndex = 72;
            this.savebttn.Text = "Save";
            this.TreeViewToolTip.SetToolTip(this.savebttn, "Save Purchase Req");
            this.savebttn.UseVisualStyleBackColor = true;
            this.savebttn.Visible = false;
            this.savebttn.Click += new System.EventHandler(this.savebttn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.PreviewTabPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(680, 651);
            this.tabControl1.TabIndex = 0;
            // 
            // PreviewTabPage
            // 
            this.PreviewTabPage.BackColor = System.Drawing.Color.LightSteelBlue;
            this.PreviewTabPage.Controls.Add(this.groupBox3);
            this.PreviewTabPage.Controls.Add(this.groupBox4);
            this.PreviewTabPage.Controls.Add(this.dataGridView1);
            this.PreviewTabPage.Location = new System.Drawing.Point(4, 22);
            this.PreviewTabPage.Name = "PreviewTabPage";
            this.PreviewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PreviewTabPage.Size = new System.Drawing.Size(672, 625);
            this.PreviewTabPage.TabIndex = 1;
            this.PreviewTabPage.Text = "Preview";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.PapayaWhip;
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.Addnewbttn);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.oemitemnotxt);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.qtytxt);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.pricetxt);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.oemtxt);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.Descriptiontxtbox);
            this.groupBox3.Controls.Add(this.Itemlbl);
            this.groupBox3.Controls.Add(this.ItemTxtBox);
            this.groupBox3.Controls.Add(this.itemsearchtxtbox);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(344, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(323, 285);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Add Item to order";
            this.groupBox3.UseCompatibleTextRendering = true;
            this.groupBox3.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(228, 256);
            this.btnCancel.MaximumSize = new System.Drawing.Size(80, 25);
            this.btnCancel.MinimumSize = new System.Drawing.Size(80, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 66;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(135, 256);
            this.btnDelete.MaximumSize = new System.Drawing.Size(80, 25);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 25);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 25);
            this.btnDelete.TabIndex = 66;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Addnewbttn
            // 
            this.Addnewbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Addnewbttn.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Addnewbttn.Location = new System.Drawing.Point(39, 256);
            this.Addnewbttn.MaximumSize = new System.Drawing.Size(80, 25);
            this.Addnewbttn.MinimumSize = new System.Drawing.Size(80, 25);
            this.Addnewbttn.Name = "Addnewbttn";
            this.Addnewbttn.Size = new System.Drawing.Size(80, 25);
            this.Addnewbttn.TabIndex = 66;
            this.Addnewbttn.Text = "Add ";
            this.Addnewbttn.UseVisualStyleBackColor = true;
            this.Addnewbttn.Click += new System.EventHandler(this.Addnewbttn_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(3, 171);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 65;
            this.label5.Text = "OEM Item No :";
            // 
            // oemitemnotxt
            // 
            this.oemitemnotxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemitemnotxt.BackColor = System.Drawing.Color.White;
            this.oemitemnotxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemitemnotxt.Location = new System.Drawing.Point(117, 171);
            this.oemitemnotxt.MaximumSize = new System.Drawing.Size(200, 20);
            this.oemitemnotxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.oemitemnotxt.Name = "oemitemnotxt";
            this.oemitemnotxt.ReadOnly = true;
            this.oemitemnotxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemitemnotxt.Size = new System.Drawing.Size(200, 20);
            this.oemitemnotxt.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(36, 230);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 63;
            this.label4.Text = "Quantity :";
            // 
            // qtytxt
            // 
            this.qtytxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qtytxt.BackColor = System.Drawing.Color.White;
            this.qtytxt.Location = new System.Drawing.Point(117, 230);
            this.qtytxt.MaximumSize = new System.Drawing.Size(200, 20);
            this.qtytxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.qtytxt.Name = "qtytxt";
            this.qtytxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qtytxt.Size = new System.Drawing.Size(200, 22);
            this.qtytxt.TabIndex = 62;
            this.qtytxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qtytxt_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(55, 197);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 61;
            this.label3.Text = "Price :";
            // 
            // pricetxt
            // 
            this.pricetxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pricetxt.BackColor = System.Drawing.Color.White;
            this.pricetxt.Location = new System.Drawing.Point(117, 197);
            this.pricetxt.MaximumSize = new System.Drawing.Size(200, 20);
            this.pricetxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.pricetxt.Name = "pricetxt";
            this.pricetxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pricetxt.Size = new System.Drawing.Size(200, 22);
            this.pricetxt.TabIndex = 60;
            this.pricetxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pricetxt_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(3, 141);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 59;
            this.label1.Text = "Manufacturer :";
            // 
            // oemtxt
            // 
            this.oemtxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemtxt.BackColor = System.Drawing.Color.White;
            this.oemtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemtxt.Location = new System.Drawing.Point(117, 141);
            this.oemtxt.MaximumSize = new System.Drawing.Size(200, 20);
            this.oemtxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.oemtxt.Name = "oemtxt";
            this.oemtxt.ReadOnly = true;
            this.oemtxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemtxt.Size = new System.Drawing.Size(200, 20);
            this.oemtxt.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(11, 105);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "Description : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Descriptiontxtbox
            // 
            this.Descriptiontxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Descriptiontxtbox.BackColor = System.Drawing.Color.White;
            this.Descriptiontxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descriptiontxtbox.Location = new System.Drawing.Point(117, 85);
            this.Descriptiontxtbox.MaximumSize = new System.Drawing.Size(200, 50);
            this.Descriptiontxtbox.MinimumSize = new System.Drawing.Size(200, 50);
            this.Descriptiontxtbox.Multiline = true;
            this.Descriptiontxtbox.Name = "Descriptiontxtbox";
            this.Descriptiontxtbox.ReadOnly = true;
            this.Descriptiontxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Descriptiontxtbox.Size = new System.Drawing.Size(200, 50);
            this.Descriptiontxtbox.TabIndex = 55;
            // 
            // Itemlbl
            // 
            this.Itemlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Itemlbl.AutoSize = true;
            this.Itemlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Itemlbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Itemlbl.Location = new System.Drawing.Point(55, 64);
            this.Itemlbl.Name = "Itemlbl";
            this.Itemlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Itemlbl.Size = new System.Drawing.Size(43, 15);
            this.Itemlbl.TabIndex = 56;
            this.Itemlbl.Text = "Item :";
            // 
            // ItemTxtBox
            // 
            this.ItemTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemTxtBox.BackColor = System.Drawing.Color.White;
            this.ItemTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemTxtBox.Location = new System.Drawing.Point(117, 59);
            this.ItemTxtBox.MaximumSize = new System.Drawing.Size(200, 20);
            this.ItemTxtBox.MinimumSize = new System.Drawing.Size(180, 20);
            this.ItemTxtBox.Name = "ItemTxtBox";
            this.ItemTxtBox.ReadOnly = true;
            this.ItemTxtBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemTxtBox.Size = new System.Drawing.Size(200, 20);
            this.ItemTxtBox.TabIndex = 54;
            // 
            // itemsearchtxtbox
            // 
            this.itemsearchtxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.itemsearchtxtbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.itemsearchtxtbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.itemsearchtxtbox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.itemsearchtxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemsearchtxtbox.Location = new System.Drawing.Point(6, 31);
            this.itemsearchtxtbox.MaximumSize = new System.Drawing.Size(650, 25);
            this.itemsearchtxtbox.MinimumSize = new System.Drawing.Size(200, 20);
            this.itemsearchtxtbox.Name = "itemsearchtxtbox";
            this.itemsearchtxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.itemsearchtxtbox.Size = new System.Drawing.Size(311, 22);
            this.itemsearchtxtbox.TabIndex = 2;
            this.itemsearchtxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemsearchtxtbox_KeyDown);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.Khaki;
            this.groupBox4.Controls.Add(this.Preqlbl);
            this.groupBox4.Controls.Add(this.purchreqtxt);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(6, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(336, 285);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Purchase Requisition Details";
            this.groupBox4.UseCompatibleTextRendering = true;
            // 
            // Preqlbl
            // 
            this.Preqlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Preqlbl.AutoSize = true;
            this.Preqlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Preqlbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Preqlbl.Location = new System.Drawing.Point(15, 47);
            this.Preqlbl.Name = "Preqlbl";
            this.Preqlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Preqlbl.Size = new System.Drawing.Size(110, 15);
            this.Preqlbl.TabIndex = 58;
            this.Preqlbl.Text = "Requisition No :";
            // 
            // purchreqtxt
            // 
            this.purchreqtxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.purchreqtxt.BackColor = System.Drawing.Color.White;
            this.purchreqtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purchreqtxt.Location = new System.Drawing.Point(130, 46);
            this.purchreqtxt.MaximumSize = new System.Drawing.Size(200, 20);
            this.purchreqtxt.MinimumSize = new System.Drawing.Size(150, 20);
            this.purchreqtxt.Name = "purchreqtxt";
            this.purchreqtxt.ReadOnly = true;
            this.purchreqtxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.purchreqtxt.Size = new System.Drawing.Size(150, 20);
            this.purchreqtxt.TabIndex = 57;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersHeight = 50;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.OrderId,
            this.Item,
            this.Qty,
            this.Description,
            this.Manufacturer,
            this.OEMItemNumber,
            this.Price,
            this.Notes});
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dataGridView1.Location = new System.Drawing.Point(3, 293);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(666, 326);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // OrderId
            // 
            this.OrderId.DataPropertyName = "OrderId";
            this.OrderId.FillWeight = 50F;
            this.OrderId.HeaderText = "OrderId";
            this.OrderId.Name = "OrderId";
            this.OrderId.ReadOnly = true;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "Item";
            this.Item.FillWeight = 60F;
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.FillWeight = 50F;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 79.47787F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Manufacturer
            // 
            this.Manufacturer.DataPropertyName = "Manufacturer";
            this.Manufacturer.FillWeight = 79.47787F;
            this.Manufacturer.HeaderText = "Manufacturer";
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.ReadOnly = true;
            // 
            // OEMItemNumber
            // 
            this.OEMItemNumber.DataPropertyName = "OEMItemNumber";
            this.OEMItemNumber.FillWeight = 79.47787F;
            this.OEMItemNumber.HeaderText = "OEMItemNumber";
            this.OEMItemNumber.Name = "OEMItemNumber";
            this.OEMItemNumber.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.FillWeight = 60F;
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // Notes
            // 
            this.Notes.DataPropertyName = "Notes";
            this.Notes.FillWeight = 79.47787F;
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            this.Notes.ReadOnly = true;
            // 
            // PurchaseReqform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(984, 711);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 750);
            this.Name = "PurchaseReqform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Requisition";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PurchaseReq_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.PreviewTabPage.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem NewItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox PurchaseReqSearchTxt;
        private System.Windows.Forms.ToolStripMenuItem createNewPurchaseReqToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PreviewTabPage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button ecitbttn;
        public System.Windows.Forms.Button editbttn;
        public System.Windows.Forms.Button savebttn;
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.TextBox itemsearchtxtbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox oemitemnotxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox qtytxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pricetxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oemtxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Descriptiontxtbox;
        private System.Windows.Forms.Label Itemlbl;
        private System.Windows.Forms.TextBox ItemTxtBox;
        private System.Windows.Forms.Button Addnewbttn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label Preqlbl;
        private System.Windows.Forms.TextBox purchreqtxt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Manufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn OEMItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
    }
}