namespace SearchDataSPM.Purchasing
{
    partial class InvoiceAddItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceAddItem));
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.Addnewbttn = new System.Windows.Forms.Button();
            this.oemitemnotxt = new System.Windows.Forms.TextBox();
            this.qtytxt = new System.Windows.Forms.TextBox();
            this.pricetxt = new System.Windows.Forms.TextBox();
            this.oemtxt = new System.Windows.Forms.TextBox();
            this.ItemTxtBox = new System.Windows.Forms.TextBox();
            this.ItemsCombobox = new System.Windows.Forms.ComboBox();
            this.ItemsGrpBox = new System.Windows.Forms.GroupBox();
            this.showspmchk = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tarifftxt = new System.Windows.Forms.TextBox();
            this.origintxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.totaltxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Descriptiontxtbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Itemlbl = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ItemsGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(301, 370);
            this.btnCancel.MaximumSize = new System.Drawing.Size(80, 25);
            this.btnCancel.MinimumSize = new System.Drawing.Size(80, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Clear";
            this.TreeViewToolTip.SetToolTip(this.btnCancel, "Cancel");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // Addnewbttn
            // 
            this.Addnewbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Addnewbttn.Enabled = false;
            this.Addnewbttn.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Addnewbttn.ForeColor = System.Drawing.Color.Black;
            this.Addnewbttn.Location = new System.Drawing.Point(190, 370);
            this.Addnewbttn.MaximumSize = new System.Drawing.Size(80, 25);
            this.Addnewbttn.MinimumSize = new System.Drawing.Size(80, 25);
            this.Addnewbttn.Name = "Addnewbttn";
            this.Addnewbttn.Size = new System.Drawing.Size(80, 25);
            this.Addnewbttn.TabIndex = 10;
            this.Addnewbttn.Text = "&Add ";
            this.TreeViewToolTip.SetToolTip(this.Addnewbttn, "Add Item To Purchase Req");
            this.Addnewbttn.UseVisualStyleBackColor = true;
            this.Addnewbttn.Click += new System.EventHandler(this.Addnewbttn_Click);
            // 
            // oemitemnotxt
            // 
            this.oemitemnotxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemitemnotxt.BackColor = System.Drawing.Color.White;
            this.oemitemnotxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.oemitemnotxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemitemnotxt.Location = new System.Drawing.Point(117, 164);
            this.oemitemnotxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.oemitemnotxt.Name = "oemitemnotxt";
            this.oemitemnotxt.ReadOnly = true;
            this.oemitemnotxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemitemnotxt.Size = new System.Drawing.Size(368, 20);
            this.oemitemnotxt.TabIndex = 4;
            this.TreeViewToolTip.SetToolTip(this.oemitemnotxt, "Manufacturer Item No");
            this.oemitemnotxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oemitemnotxt_KeyDown);
            // 
            // qtytxt
            // 
            this.qtytxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qtytxt.BackColor = System.Drawing.Color.White;
            this.qtytxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtytxt.Location = new System.Drawing.Point(117, 192);
            this.qtytxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.qtytxt.Name = "qtytxt";
            this.qtytxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qtytxt.Size = new System.Drawing.Size(368, 22);
            this.qtytxt.TabIndex = 5;
            this.TreeViewToolTip.SetToolTip(this.qtytxt, "Quantity required");
            this.qtytxt.TextChanged += new System.EventHandler(this.Qtytxt_TextChanged);
            this.qtytxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Qtytxt_KeyDown);
            this.qtytxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Qtytxt_KeyPress);
            // 
            // pricetxt
            // 
            this.pricetxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pricetxt.BackColor = System.Drawing.Color.White;
            this.pricetxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pricetxt.Location = new System.Drawing.Point(117, 224);
            this.pricetxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.pricetxt.Name = "pricetxt";
            this.pricetxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pricetxt.Size = new System.Drawing.Size(368, 22);
            this.pricetxt.TabIndex = 6;
            this.pricetxt.Text = "$0.00";
            this.TreeViewToolTip.SetToolTip(this.pricetxt, "Unit price for the item");
            this.pricetxt.TextChanged += new System.EventHandler(this.Pricetxt_TextChanged);
            this.pricetxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pricetxt_KeyDown);
            // 
            // oemtxt
            // 
            this.oemtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemtxt.BackColor = System.Drawing.Color.White;
            this.oemtxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.oemtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemtxt.Location = new System.Drawing.Point(117, 135);
            this.oemtxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.oemtxt.Name = "oemtxt";
            this.oemtxt.ReadOnly = true;
            this.oemtxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemtxt.Size = new System.Drawing.Size(368, 20);
            this.oemtxt.TabIndex = 3;
            this.TreeViewToolTip.SetToolTip(this.oemtxt, "Item Manufacturer");
            this.oemtxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oemtxt_KeyDown);
            // 
            // ItemTxtBox
            // 
            this.ItemTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemTxtBox.BackColor = System.Drawing.Color.White;
            this.ItemTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemTxtBox.Location = new System.Drawing.Point(117, 70);
            this.ItemTxtBox.MinimumSize = new System.Drawing.Size(180, 20);
            this.ItemTxtBox.Name = "ItemTxtBox";
            this.ItemTxtBox.ReadOnly = true;
            this.ItemTxtBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemTxtBox.Size = new System.Drawing.Size(368, 20);
            this.ItemTxtBox.TabIndex = 1;
            this.TreeViewToolTip.SetToolTip(this.ItemTxtBox, "Item Number");
            // 
            // ItemsCombobox
            // 
            this.ItemsCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemsCombobox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ItemsCombobox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ItemsCombobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ItemsCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemsCombobox.FormattingEnabled = true;
            this.ItemsCombobox.ItemHeight = 13;
            this.ItemsCombobox.Location = new System.Drawing.Point(12, 39);
            this.ItemsCombobox.Name = "ItemsCombobox";
            this.ItemsCombobox.Size = new System.Drawing.Size(474, 21);
            this.ItemsCombobox.TabIndex = 0;
            this.TreeViewToolTip.SetToolTip(this.ItemsCombobox, "Select Sold to Customer/Vendor");
            this.ItemsCombobox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemsCombobox_KeyDown);
            this.ItemsCombobox.Leave += new System.EventHandler(this.ItemsCombobox_Leave);
            this.ItemsCombobox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ItemsCombobox_PreviewKeyDown);
            // 
            // ItemsGrpBox
            // 
            this.ItemsGrpBox.BackColor = System.Drawing.Color.Transparent;
            this.ItemsGrpBox.Controls.Add(this.showspmchk);
            this.ItemsGrpBox.Controls.Add(this.ItemsCombobox);
            this.ItemsGrpBox.Controls.Add(this.btnCancel);
            this.ItemsGrpBox.Controls.Add(this.Addnewbttn);
            this.ItemsGrpBox.Controls.Add(this.label5);
            this.ItemsGrpBox.Controls.Add(this.oemitemnotxt);
            this.ItemsGrpBox.Controls.Add(this.label7);
            this.ItemsGrpBox.Controls.Add(this.label1);
            this.ItemsGrpBox.Controls.Add(this.label4);
            this.ItemsGrpBox.Controls.Add(this.tarifftxt);
            this.ItemsGrpBox.Controls.Add(this.origintxt);
            this.ItemsGrpBox.Controls.Add(this.qtytxt);
            this.ItemsGrpBox.Controls.Add(this.label8);
            this.ItemsGrpBox.Controls.Add(this.label3);
            this.ItemsGrpBox.Controls.Add(this.totaltxt);
            this.ItemsGrpBox.Controls.Add(this.pricetxt);
            this.ItemsGrpBox.Controls.Add(this.label2);
            this.ItemsGrpBox.Controls.Add(this.Descriptiontxtbox);
            this.ItemsGrpBox.Controls.Add(this.oemtxt);
            this.ItemsGrpBox.Controls.Add(this.label6);
            this.ItemsGrpBox.Controls.Add(this.Itemlbl);
            this.ItemsGrpBox.Controls.Add(this.ItemTxtBox);
            this.ItemsGrpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsGrpBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ItemsGrpBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemsGrpBox.ForeColor = System.Drawing.Color.White;
            this.ItemsGrpBox.Location = new System.Drawing.Point(0, 0);
            this.ItemsGrpBox.Margin = new System.Windows.Forms.Padding(1);
            this.ItemsGrpBox.Name = "ItemsGrpBox";
            this.ItemsGrpBox.Size = new System.Drawing.Size(491, 401);
            this.ItemsGrpBox.TabIndex = 15;
            this.ItemsGrpBox.TabStop = false;
            this.ItemsGrpBox.Text = "Add Item to order";
            this.ItemsGrpBox.UseCompatibleTextRendering = true;
            // 
            // showspmchk
            // 
            this.showspmchk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showspmchk.AutoSize = true;
            this.showspmchk.Location = new System.Drawing.Point(353, 13);
            this.showspmchk.Name = "showspmchk";
            this.showspmchk.Size = new System.Drawing.Size(141, 20);
            this.showspmchk.TabIndex = 66;
            this.showspmchk.Text = "Show SPM Items";
            this.showspmchk.UseVisualStyleBackColor = true;
            this.showspmchk.Visible = false;
            this.showspmchk.CheckedChanged += new System.EventHandler(this.Showspmchk_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 164);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 65;
            this.label5.Text = "OEM Item No :";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(14, 300);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 63;
            this.label7.Text = "Tarriff Code :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(49, 260);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 63;
            this.label1.Text = "Origin :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(36, 195);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 63;
            this.label4.Text = "Quantity :";
            // 
            // tarifftxt
            // 
            this.tarifftxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tarifftxt.BackColor = System.Drawing.Color.White;
            this.tarifftxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tarifftxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tarifftxt.Location = new System.Drawing.Point(117, 297);
            this.tarifftxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.tarifftxt.Name = "tarifftxt";
            this.tarifftxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tarifftxt.Size = new System.Drawing.Size(368, 22);
            this.tarifftxt.TabIndex = 8;
            // 
            // origintxt
            // 
            this.origintxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.origintxt.BackColor = System.Drawing.Color.White;
            this.origintxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.origintxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.origintxt.Location = new System.Drawing.Point(117, 260);
            this.origintxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.origintxt.Name = "origintxt";
            this.origintxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.origintxt.Size = new System.Drawing.Size(368, 22);
            this.origintxt.TabIndex = 7;
            this.origintxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.origintxt_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(51, 332);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(47, 15);
            this.label8.TabIndex = 61;
            this.label8.Text = "Total :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 226);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 61;
            this.label3.Text = "Price Each :";
            // 
            // totaltxt
            // 
            this.totaltxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totaltxt.BackColor = System.Drawing.Color.White;
            this.totaltxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totaltxt.Location = new System.Drawing.Point(117, 332);
            this.totaltxt.MinimumSize = new System.Drawing.Size(180, 20);
            this.totaltxt.Name = "totaltxt";
            this.totaltxt.ReadOnly = true;
            this.totaltxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.totaltxt.Size = new System.Drawing.Size(368, 22);
            this.totaltxt.TabIndex = 9;
            this.totaltxt.Text = "$0.00";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 135);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "Manufacturer :";
            // 
            // Descriptiontxtbox
            // 
            this.Descriptiontxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Descriptiontxtbox.BackColor = System.Drawing.Color.White;
            this.Descriptiontxtbox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Descriptiontxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descriptiontxtbox.Location = new System.Drawing.Point(117, 103);
            this.Descriptiontxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.Descriptiontxtbox.Name = "Descriptiontxtbox";
            this.Descriptiontxtbox.ReadOnly = true;
            this.Descriptiontxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Descriptiontxtbox.Size = new System.Drawing.Size(368, 20);
            this.Descriptiontxtbox.TabIndex = 2;
            this.Descriptiontxtbox.TextChanged += new System.EventHandler(this.Descriptiontxtbox_TextChanged);
            this.Descriptiontxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Descriptiontxtbox_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 103);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(92, 15);
            this.label6.TabIndex = 57;
            this.label6.Text = "Description : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Itemlbl
            // 
            this.Itemlbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Itemlbl.AutoSize = true;
            this.Itemlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Itemlbl.ForeColor = System.Drawing.Color.White;
            this.Itemlbl.Location = new System.Drawing.Point(55, 74);
            this.Itemlbl.Name = "Itemlbl";
            this.Itemlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Itemlbl.Size = new System.Drawing.Size(43, 15);
            this.Itemlbl.TabIndex = 56;
            this.Itemlbl.Text = "Item :";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // InvoiceAddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(491, 401);
            this.Controls.Add(this.ItemsGrpBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1025, 2000);
            this.MinimizeBox = false;
            this.Name = "InvoiceAddItem";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Details  - Add Item";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuoteDetails_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InvoiceAddItem_FormClosed);
            this.Load += new System.EventHandler(this.QuoteDetails_Load);
            this.ItemsGrpBox.ResumeLayout(false);
            this.ItemsGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.GroupBox ItemsGrpBox;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button Addnewbttn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox oemitemnotxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox qtytxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pricetxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox oemtxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Itemlbl;
        private System.Windows.Forms.TextBox ItemTxtBox;
        private System.Windows.Forms.ComboBox ItemsCombobox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tarifftxt;
        private System.Windows.Forms.TextBox origintxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox totaltxt;
        private System.Windows.Forms.TextBox Descriptiontxtbox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox showspmchk;
    }
}