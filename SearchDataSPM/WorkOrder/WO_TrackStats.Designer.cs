namespace SearchDataSPM
{
    partial class WO_TrackStats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WO_TrackStats));
            this.panel1 = new System.Windows.Forms.Panel();
            this.reloadbttn = new System.Windows.Forms.Button();
            this.woid_txtbox = new MetroFramework.Controls.MetroTextBox();
            this.wodetgroupbox = new System.Windows.Forms.GroupBox();
            this.descriptionlabel = new System.Windows.Forms.Label();
            this.completelabel = new System.Windows.Forms.Label();
            this.inbuiltlabel = new System.Windows.Forms.Label();
            this.qtylabel = new System.Windows.Forms.Label();
            this.itemlabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wolistbox = new System.Windows.Forms.ListBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.prodout = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.purwaiting = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.cribwaiting = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.purbox = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.cribbox = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.prodbox = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.prodwaiting = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.engbox = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.wodetgroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.reloadbttn);
            this.panel1.Controls.Add(this.woid_txtbox);
            this.panel1.Controls.Add(this.wodetgroupbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MaximumSize = new System.Drawing.Size(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 80);
            this.panel1.TabIndex = 0;
            // 
            // reloadbttn
            // 
            this.reloadbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadbttn.BackColor = System.Drawing.Color.Transparent;
            this.reloadbttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reloadbttn.ForeColor = System.Drawing.Color.White;
            this.reloadbttn.Image = ((System.Drawing.Image)(resources.GetObject("reloadbttn.Image")));
            this.reloadbttn.Location = new System.Drawing.Point(5, 5);
            this.reloadbttn.MaximumSize = new System.Drawing.Size(24, 24);
            this.reloadbttn.MinimumSize = new System.Drawing.Size(24, 24);
            this.reloadbttn.Name = "reloadbttn";
            this.reloadbttn.Size = new System.Drawing.Size(24, 24);
            this.reloadbttn.TabIndex = 123;
            this.reloadbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.reloadbttn.UseVisualStyleBackColor = false;
            this.reloadbttn.Click += new System.EventHandler(this.reloadbttn_Click);
            // 
            // woid_txtbox
            // 
            // 
            // 
            // 
            this.woid_txtbox.CustomButton.Image = null;
            this.woid_txtbox.CustomButton.Location = new System.Drawing.Point(161, 1);
            this.woid_txtbox.CustomButton.Name = "";
            this.woid_txtbox.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.woid_txtbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.woid_txtbox.CustomButton.TabIndex = 1;
            this.woid_txtbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.woid_txtbox.CustomButton.UseSelectable = true;
            this.woid_txtbox.CustomButton.Visible = false;
            this.woid_txtbox.DisplayIcon = true;
            this.woid_txtbox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.woid_txtbox.Icon = ((System.Drawing.Image)(resources.GetObject("woid_txtbox.Icon")));
            this.woid_txtbox.Lines = new string[0];
            this.woid_txtbox.Location = new System.Drawing.Point(5, 35);
            this.woid_txtbox.MaxLength = 32767;
            this.woid_txtbox.Name = "woid_txtbox";
            this.woid_txtbox.PasswordChar = '\0';
            this.woid_txtbox.PromptText = "Scan Work Order";
            this.woid_txtbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.woid_txtbox.SelectedText = "";
            this.woid_txtbox.SelectionLength = 0;
            this.woid_txtbox.SelectionStart = 0;
            this.woid_txtbox.ShortcutsEnabled = true;
            this.woid_txtbox.ShowClearButton = true;
            this.woid_txtbox.Size = new System.Drawing.Size(195, 35);
            this.woid_txtbox.TabIndex = 1;
            this.woid_txtbox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.woid_txtbox.UseSelectable = true;
            this.woid_txtbox.WaterMark = "Scan Work Order";
            this.woid_txtbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.woid_txtbox.WaterMarkFont = new System.Drawing.Font("Adobe Heiti Std R", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.woid_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.woid_txtbox_KeyDown);
            // 
            // wodetgroupbox
            // 
            this.wodetgroupbox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.wodetgroupbox.Controls.Add(this.descriptionlabel);
            this.wodetgroupbox.Controls.Add(this.completelabel);
            this.wodetgroupbox.Controls.Add(this.inbuiltlabel);
            this.wodetgroupbox.Controls.Add(this.qtylabel);
            this.wodetgroupbox.Controls.Add(this.itemlabel);
            this.wodetgroupbox.Location = new System.Drawing.Point(204, 3);
            this.wodetgroupbox.Name = "wodetgroupbox";
            this.wodetgroupbox.Size = new System.Drawing.Size(677, 71);
            this.wodetgroupbox.TabIndex = 0;
            this.wodetgroupbox.TabStop = false;
            this.wodetgroupbox.Text = "WO Details";
            // 
            // descriptionlabel
            // 
            this.descriptionlabel.AutoSize = true;
            this.descriptionlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionlabel.Location = new System.Drawing.Point(6, 47);
            this.descriptionlabel.Name = "descriptionlabel";
            this.descriptionlabel.Size = new System.Drawing.Size(82, 16);
            this.descriptionlabel.TabIndex = 0;
            this.descriptionlabel.Text = "Description :";
            // 
            // completelabel
            // 
            this.completelabel.AutoSize = true;
            this.completelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.completelabel.Location = new System.Drawing.Point(488, 21);
            this.completelabel.Name = "completelabel";
            this.completelabel.Size = new System.Drawing.Size(80, 16);
            this.completelabel.TabIndex = 0;
            this.completelabel.Text = "Completed :";
            // 
            // inbuiltlabel
            // 
            this.inbuiltlabel.AutoSize = true;
            this.inbuiltlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inbuiltlabel.Location = new System.Drawing.Point(352, 21);
            this.inbuiltlabel.Name = "inbuiltlabel";
            this.inbuiltlabel.Size = new System.Drawing.Size(53, 16);
            this.inbuiltlabel.TabIndex = 0;
            this.inbuiltlabel.Text = "In-Built :";
            // 
            // qtylabel
            // 
            this.qtylabel.AutoSize = true;
            this.qtylabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtylabel.Location = new System.Drawing.Point(210, 21);
            this.qtylabel.Name = "qtylabel";
            this.qtylabel.Size = new System.Drawing.Size(34, 16);
            this.qtylabel.TabIndex = 0;
            this.qtylabel.Text = "Qty :";
            // 
            // itemlabel
            // 
            this.itemlabel.AutoSize = true;
            this.itemlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemlabel.Location = new System.Drawing.Point(6, 21);
            this.itemlabel.Name = "itemlabel";
            this.itemlabel.Size = new System.Drawing.Size(92, 16);
            this.itemlabel.TabIndex = 0;
            this.itemlabel.Text = "SPM Item No :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 80);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.wolistbox);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkedListBox1);
            this.splitContainer1.Panel2.Controls.Add(this.shapeContainer1);
            this.splitContainer1.Panel2MinSize = 680;
            this.splitContainer1.Size = new System.Drawing.Size(884, 581);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 1;
            // 
            // wolistbox
            // 
            this.wolistbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wolistbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wolistbox.FormattingEnabled = true;
            this.wolistbox.ItemHeight = 18;
            this.wolistbox.Location = new System.Drawing.Point(0, 0);
            this.wolistbox.Name = "wolistbox";
            this.wolistbox.Size = new System.Drawing.Size(200, 581);
            this.wolistbox.TabIndex = 0;
            this.wolistbox.Click += new System.EventHandler(this.wolistbox_Click);
            this.wolistbox.SelectedIndexChanged += new System.EventHandler(this.wolistbox_SelectedIndexChanged);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.prodout,
            this.purwaiting,
            this.cribwaiting,
            this.purbox,
            this.lineShape5,
            this.cribbox,
            this.lineShape3,
            this.lineShape4,
            this.lineShape2,
            this.prodbox,
            this.lineShape1,
            this.prodwaiting,
            this.engbox});
            this.shapeContainer1.Size = new System.Drawing.Size(680, 581);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // prodout
            // 
            this.prodout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.prodout.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.prodout.Cursor = System.Windows.Forms.Cursors.Default;
            this.prodout.FillColor = System.Drawing.SystemColors.AppWorkspace;
            this.prodout.Location = new System.Drawing.Point(329, 374);
            this.prodout.Name = "prodout";
            this.prodout.Size = new System.Drawing.Size(22, 22);
            // 
            // purwaiting
            // 
            this.purwaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.purwaiting.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.purwaiting.Cursor = System.Windows.Forms.Cursors.Default;
            this.purwaiting.FillColor = System.Drawing.SystemColors.AppWorkspace;
            this.purwaiting.Location = new System.Drawing.Point(399, 441);
            this.purwaiting.Name = "purwaiting";
            this.purwaiting.Size = new System.Drawing.Size(22, 22);
            // 
            // cribwaiting
            // 
            this.cribwaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cribwaiting.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.cribwaiting.Cursor = System.Windows.Forms.Cursors.Default;
            this.cribwaiting.FillColor = System.Drawing.SystemColors.AppWorkspace;
            this.cribwaiting.Location = new System.Drawing.Point(264, 441);
            this.cribwaiting.Name = "cribwaiting";
            this.cribwaiting.Size = new System.Drawing.Size(22, 22);
            // 
            // purbox
            // 
            this.purbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.purbox.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.purbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.purbox.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.purbox.Location = new System.Drawing.Point(472, 384);
            this.purbox.Name = "purbox";
            this.purbox.Size = new System.Drawing.Size(194, 130);
            // 
            // lineShape5
            // 
            this.lineShape5.BorderWidth = 2;
            this.lineShape5.Cursor = System.Windows.Forms.Cursors.Default;
            this.lineShape5.Name = "lineShape3";
            this.lineShape5.X1 = 471;
            this.lineShape5.X2 = 341;
            this.lineShape5.Y1 = 452;
            this.lineShape5.Y2 = 452;
            // 
            // cribbox
            // 
            this.cribbox.BackColor = System.Drawing.Color.Cyan;
            this.cribbox.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.cribbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.cribbox.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.cribbox.Location = new System.Drawing.Point(17, 384);
            this.cribbox.Name = "cribbox";
            this.cribbox.Size = new System.Drawing.Size(194, 130);
            // 
            // lineShape3
            // 
            this.lineShape3.BorderWidth = 2;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 340;
            this.lineShape3.X2 = 210;
            this.lineShape3.Y1 = 452;
            this.lineShape3.Y2 = 452;
            // 
            // lineShape4
            // 
            this.lineShape4.BorderWidth = 2;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 340;
            this.lineShape4.X2 = 340;
            this.lineShape4.Y1 = 450;
            this.lineShape4.Y2 = 328;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderWidth = 2;
            this.lineShape2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lineShape2.Name = "lineShape1";
            this.lineShape2.X1 = 340;
            this.lineShape2.X2 = 340;
            this.lineShape2.Y1 = 197;
            this.lineShape2.Y2 = 157;
            // 
            // prodbox
            // 
            this.prodbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.prodbox.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.prodbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.prodbox.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.prodbox.Location = new System.Drawing.Point(243, 198);
            this.prodbox.Name = "prodbox";
            this.prodbox.Size = new System.Drawing.Size(194, 130);
            // 
            // lineShape1
            // 
            this.lineShape1.BorderWidth = 2;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 340;
            this.lineShape1.X2 = 340;
            this.lineShape1.Y1 = 135;
            this.lineShape1.Y2 = 95;
            // 
            // prodwaiting
            // 
            this.prodwaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.prodwaiting.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.prodwaiting.FillColor = System.Drawing.SystemColors.AppWorkspace;
            this.prodwaiting.Location = new System.Drawing.Point(329, 136);
            this.prodwaiting.Name = "prodwaiting";
            this.prodwaiting.Size = new System.Drawing.Size(22, 22);
            // 
            // engbox
            // 
            this.engbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.engbox.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.engbox.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Central;
            this.engbox.Location = new System.Drawing.Point(243, 25);
            this.engbox.Name = "engbox";
            this.engbox.Size = new System.Drawing.Size(194, 70);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Engineering"});
            this.checkedListBox1.Location = new System.Drawing.Point(469, 56);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(198, 124);
            this.checkedListBox1.TabIndex = 1;
            // 
            // WO_TrackStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "WO_TrackStats";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Inventory Bin Status";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BinLog_Load);
            this.panel1.ResumeLayout(false);
            this.wodetgroupbox.ResumeLayout(false);
            this.wodetgroupbox.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox wolistbox;
        private System.Windows.Forms.GroupBox wodetgroupbox;
        private MetroFramework.Controls.MetroTextBox woid_txtbox;
        private System.Windows.Forms.Label descriptionlabel;
        private System.Windows.Forms.Label itemlabel;
        private System.Windows.Forms.Label qtylabel;
        private System.Windows.Forms.Label completelabel;
        private System.Windows.Forms.Label inbuiltlabel;
        private System.Windows.Forms.Button reloadbttn;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape prodbox;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape prodwaiting;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape engbox;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape purbox;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape cribbox;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.OvalShape purwaiting;
        private Microsoft.VisualBasic.PowerPacks.OvalShape cribwaiting;
        private Microsoft.VisualBasic.PowerPacks.OvalShape prodout;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}