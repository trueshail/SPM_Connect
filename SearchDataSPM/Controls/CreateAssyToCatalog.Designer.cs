namespace SearchDataSPM
{
    partial class CreateAssyToCatalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAssyToCatalog));
            this.LabelTooltips = new System.Windows.Forms.ToolTip(this.components);
            this.Expandchk = new System.Windows.Forms.CheckBox();
            this.Descriptiontxtbox = new System.Windows.Forms.TextBox();
            this.oemitemtxtbox = new System.Windows.Forms.TextBox();
            this.oemtxtbox = new System.Windows.Forms.TextBox();
            this.familytxtbox = new System.Windows.Forms.TextBox();
            this.ItemTxtBox = new System.Windows.Forms.TextBox();
            this.qtytxtbox = new System.Windows.Forms.TextBox();
            this.SPM = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.Additembttn = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oemlbl = new System.Windows.Forms.Label();
            this.oemitemlbl = new System.Windows.Forms.Label();
            this.descriptionlbl = new System.Windows.Forms.Label();
            this.Itemlbl = new System.Windows.Forms.Label();
            this.familylbl = new System.Windows.Forms.Label();
            this.qtylbl = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelTooltips
            // 
            this.LabelTooltips.AutoPopDelay = 4000;
            this.LabelTooltips.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.LabelTooltips.InitialDelay = 500;
            this.LabelTooltips.ReshowDelay = 100;
            // 
            // Expandchk
            // 
            this.Expandchk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Expandchk.AutoSize = true;
            this.Expandchk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Expandchk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expandchk.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Expandchk.Location = new System.Drawing.Point(477, 22);
            this.Expandchk.MinimumSize = new System.Drawing.Size(30, 0);
            this.Expandchk.Name = "Expandchk";
            this.Expandchk.Size = new System.Drawing.Size(49, 28);
            this.Expandchk.TabIndex = 31;
            this.Expandchk.Text = "+/-";
            this.LabelTooltips.SetToolTip(this.Expandchk, "Expand/Collapse Tree");
            this.Expandchk.UseVisualStyleBackColor = true;
            this.Expandchk.Click += new System.EventHandler(this.Expandchk_Click);
            // 
            // Descriptiontxtbox
            // 
            this.Descriptiontxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Descriptiontxtbox.Location = new System.Drawing.Point(589, 214);
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
            this.oemitemtxtbox.Location = new System.Drawing.Point(589, 350);
            this.oemitemtxtbox.MaximumSize = new System.Drawing.Size(180, 50);
            this.oemitemtxtbox.MinimumSize = new System.Drawing.Size(180, 50);
            this.oemitemtxtbox.Multiline = true;
            this.oemitemtxtbox.Name = "oemitemtxtbox";
            this.oemitemtxtbox.ReadOnly = true;
            this.oemitemtxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemitemtxtbox.Size = new System.Drawing.Size(180, 50);
            this.oemitemtxtbox.TabIndex = 39;
            this.LabelTooltips.SetToolTip(this.oemitemtxtbox, "Item Manufacturer OEM");
            // 
            // oemtxtbox
            // 
            this.oemtxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemtxtbox.Location = new System.Drawing.Point(589, 281);
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
            // familytxtbox
            // 
            this.familytxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.familytxtbox.Location = new System.Drawing.Point(589, 424);
            this.familytxtbox.MaximumSize = new System.Drawing.Size(180, 20);
            this.familytxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.familytxtbox.Name = "familytxtbox";
            this.familytxtbox.ReadOnly = true;
            this.familytxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.familytxtbox.Size = new System.Drawing.Size(180, 20);
            this.familytxtbox.TabIndex = 41;
            this.LabelTooltips.SetToolTip(this.familytxtbox, "Item Family Group");
            // 
            // ItemTxtBox
            // 
            this.ItemTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemTxtBox.Location = new System.Drawing.Point(589, 177);
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
            this.qtytxtbox.Location = new System.Drawing.Point(589, 462);
            this.qtytxtbox.MaximumSize = new System.Drawing.Size(180, 20);
            this.qtytxtbox.MinimumSize = new System.Drawing.Size(180, 20);
            this.qtytxtbox.Name = "qtytxtbox";
            this.qtytxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qtytxtbox.Size = new System.Drawing.Size(180, 20);
            this.qtytxtbox.TabIndex = 43;
            this.LabelTooltips.SetToolTip(this.qtytxtbox, "Item Quantities Per Assembly");
            this.qtytxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.qtytxtbox_KeyDown);
            this.qtytxtbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qtytxtbox_KeyPress);
            // 
            // SPM
            // 
            this.SPM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(484, 22);
            this.SPM.MaximumSize = new System.Drawing.Size(288, 100);
            this.SPM.MinimumSize = new System.Drawing.Size(100, 100);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(288, 100);
            this.SPM.TabIndex = 32;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelTooltips.SetToolTip(this.SPM, "SPM Automation Inc.");
            this.SPM.DoubleClick += new System.EventHandler(this.SPM_DoubleClick);
            // 
            // save
            // 
            this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save.Location = new System.Drawing.Point(256, 674);
            this.save.MaximumSize = new System.Drawing.Size(120, 25);
            this.save.MinimumSize = new System.Drawing.Size(120, 25);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(120, 25);
            this.save.TabIndex = 46;
            this.save.Text = "Save to Catalog";
            this.LabelTooltips.SetToolTip(this.save, "Create assembly in Autocad Catallog");
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // Additembttn
            // 
            this.Additembttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Additembttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Additembttn.Location = new System.Drawing.Point(8, 5);
            this.Additembttn.MaximumSize = new System.Drawing.Size(80, 25);
            this.Additembttn.MinimumSize = new System.Drawing.Size(80, 25);
            this.Additembttn.Name = "Additembttn";
            this.Additembttn.Size = new System.Drawing.Size(80, 25);
            this.Additembttn.TabIndex = 47;
            this.Additembttn.Text = "Add Item";
            this.LabelTooltips.SetToolTip(this.Additembttn, "Create assembly in Autocad Catallog");
            this.Additembttn.UseVisualStyleBackColor = true;
            this.Additembttn.Click += new System.EventHandler(this.Additembttn_Click);
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.HotTracking = true;
            this.treeView1.Location = new System.Drawing.Point(8, 35);
            this.treeView1.MinimumSize = new System.Drawing.Size(100, 100);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(442, 635);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addItemToolStripMenuItem,
            this.removeItemToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // addItemToolStripMenuItem
            // 
            this.addItemToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addItemToolStripMenuItem.Image")));
            this.addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
            this.addItemToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.addItemToolStripMenuItem.Text = "Add Item";
            this.addItemToolStripMenuItem.Click += new System.EventHandler(this.addItemToolStripMenuItem_Click);
            // 
            // removeItemToolStripMenuItem
            // 
            this.removeItemToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeItemToolStripMenuItem.Image")));
            this.removeItemToolStripMenuItem.Name = "removeItemToolStripMenuItem";
            this.removeItemToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.removeItemToolStripMenuItem.Text = "Remove Item";
            this.removeItemToolStripMenuItem.Click += new System.EventHandler(this.removeItemToolStripMenuItem_Click);
            // 
            // oemlbl
            // 
            this.oemlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemlbl.AutoSize = true;
            this.oemlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemlbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.oemlbl.Location = new System.Drawing.Point(480, 297);
            this.oemlbl.Name = "oemlbl";
            this.oemlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemlbl.Size = new System.Drawing.Size(100, 15);
            this.oemlbl.TabIndex = 38;
            this.oemlbl.Text = "Manufacturer :";
            // 
            // oemitemlbl
            // 
            this.oemitemlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oemitemlbl.AutoSize = true;
            this.oemitemlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oemitemlbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.oemitemlbl.Location = new System.Drawing.Point(480, 353);
            this.oemitemlbl.Name = "oemitemlbl";
            this.oemitemlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.oemitemlbl.Size = new System.Drawing.Size(102, 30);
            this.oemitemlbl.TabIndex = 40;
            this.oemitemlbl.Text = "OEM \r\nItem Number : ";
            this.oemitemlbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descriptionlbl
            // 
            this.descriptionlbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionlbl.AutoSize = true;
            this.descriptionlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionlbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.descriptionlbl.Location = new System.Drawing.Point(480, 217);
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
            this.Itemlbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Itemlbl.Location = new System.Drawing.Point(484, 180);
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
            this.familylbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.familylbl.Location = new System.Drawing.Point(480, 427);
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
            this.qtylbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.qtylbl.Location = new System.Drawing.Point(480, 465);
            this.qtylbl.Name = "qtylbl";
            this.qtylbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qtylbl.Size = new System.Drawing.Size(78, 15);
            this.qtylbl.TabIndex = 44;
            this.qtylbl.Text = "Qty/Assy\'s :";
            // 
            // CreateAssyToCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(784, 711);
            this.Controls.Add(this.Additembttn);
            this.Controls.Add(this.save);
            this.Controls.Add(this.Expandchk);
            this.Controls.Add(this.Descriptiontxtbox);
            this.Controls.Add(this.oemlbl);
            this.Controls.Add(this.oemitemtxtbox);
            this.Controls.Add(this.oemtxtbox);
            this.Controls.Add(this.SPM);
            this.Controls.Add(this.oemitemlbl);
            this.Controls.Add(this.descriptionlbl);
            this.Controls.Add(this.familytxtbox);
            this.Controls.Add(this.Itemlbl);
            this.Controls.Add(this.familylbl);
            this.Controls.Add(this.ItemTxtBox);
            this.Controls.Add(this.qtylbl);
            this.Controls.Add(this.qtytxtbox);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "CreateAssyToCatalog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Assy To Catalog - SPM Connect";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateAssyToCatalog_FormClosed);
            this.Load += new System.EventHandler(this.ParentView_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip LabelTooltips;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox Expandchk;
        private System.Windows.Forms.TextBox Descriptiontxtbox;
        private System.Windows.Forms.Label oemlbl;
        private System.Windows.Forms.TextBox oemitemtxtbox;
        private System.Windows.Forms.TextBox oemtxtbox;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Label oemitemlbl;
        private System.Windows.Forms.Label descriptionlbl;
        private System.Windows.Forms.TextBox familytxtbox;
        private System.Windows.Forms.Label Itemlbl;
        private System.Windows.Forms.Label familylbl;
        private System.Windows.Forms.TextBox ItemTxtBox;
        private System.Windows.Forms.Label qtylbl;
        private System.Windows.Forms.TextBox qtytxtbox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeItemToolStripMenuItem;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button Additembttn;
    }
}