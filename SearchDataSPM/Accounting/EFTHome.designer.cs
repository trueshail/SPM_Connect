namespace SearchDataSPM
{
    partial class EFTHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EFTHome));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getWOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailtoolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.frmdatepick = new System.Windows.Forms.DateTimePicker();
            this.todatepic = new System.Windows.Forms.DateTimePicker();
            this.managergroupbox = new System.Windows.Forms.GroupBox();
            this.bttnshowwaiting = new System.Windows.Forms.Button();
            this.bttnshowapproved = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reloadbttn = new System.Windows.Forms.Button();
            this.filterbttn = new System.Windows.Forms.Button();
            this.emailallbttn = new System.Windows.Forms.Button();
            this.SPM = new System.Windows.Forms.Label();
            this.totallbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.managergroupbox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Fax", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView.Location = new System.Drawing.Point(15, 164);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1018, 499);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.dataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getWOToolStripMenuItem,
            this.emailtoolstrip});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(207, 48);
            // 
            // getWOToolStripMenuItem
            // 
            this.getWOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("getWOToolStripMenuItem.Image")));
            this.getWOToolStripMenuItem.Name = "getWOToolStripMenuItem";
            this.getWOToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.getWOToolStripMenuItem.Text = "View EFT Report";
            this.getWOToolStripMenuItem.ToolTipText = "Preview Work Order Details";
            this.getWOToolStripMenuItem.Click += new System.EventHandler(this.GetWOToolStripMenuItem_Click);
            // 
            // emailtoolstrip
            // 
            this.emailtoolstrip.Image = ((System.Drawing.Image)(resources.GetObject("emailtoolstrip.Image")));
            this.emailtoolstrip.Name = "emailtoolstrip";
            this.emailtoolstrip.Size = new System.Drawing.Size(206, 22);
            this.emailtoolstrip.Text = "Send Email Confirmation";
            this.emailtoolstrip.Click += new System.EventHandler(this.emailtoolstrip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "From Payment Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Payment Date :";
            // 
            // frmdatepick
            // 
            this.frmdatepick.Location = new System.Drawing.Point(140, 25);
            this.frmdatepick.MinDate = new System.DateTime(2018, 4, 1, 0, 0, 0, 0);
            this.frmdatepick.Name = "frmdatepick";
            this.frmdatepick.Size = new System.Drawing.Size(213, 20);
            this.frmdatepick.TabIndex = 4;
            // 
            // todatepic
            // 
            this.todatepic.Location = new System.Drawing.Point(140, 61);
            this.todatepic.MinDate = new System.DateTime(2018, 4, 1, 0, 0, 0, 0);
            this.todatepic.Name = "todatepic";
            this.todatepic.Size = new System.Drawing.Size(213, 20);
            this.todatepic.TabIndex = 4;
            // 
            // managergroupbox
            // 
            this.managergroupbox.Controls.Add(this.bttnshowwaiting);
            this.managergroupbox.Controls.Add(this.bttnshowapproved);
            this.managergroupbox.Enabled = false;
            this.managergroupbox.ForeColor = System.Drawing.Color.Gray;
            this.managergroupbox.Location = new System.Drawing.Point(296, 60);
            this.managergroupbox.Name = "managergroupbox";
            this.managergroupbox.Size = new System.Drawing.Size(226, 93);
            this.managergroupbox.TabIndex = 24;
            this.managergroupbox.TabStop = false;
            this.managergroupbox.Text = "Supervisor Controls";
            this.managergroupbox.Visible = false;
            // 
            // bttnshowwaiting
            // 
            this.bttnshowwaiting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnshowwaiting.AutoSize = true;
            this.bttnshowwaiting.BackColor = System.Drawing.Color.Transparent;
            this.bttnshowwaiting.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.bttnshowwaiting.FlatAppearance.BorderSize = 2;
            this.bttnshowwaiting.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.bttnshowwaiting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.bttnshowwaiting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.bttnshowwaiting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttnshowwaiting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnshowwaiting.ForeColor = System.Drawing.Color.DimGray;
            this.bttnshowwaiting.Image = ((System.Drawing.Image)(resources.GetObject("bttnshowwaiting.Image")));
            this.bttnshowwaiting.Location = new System.Drawing.Point(116, 20);
            this.bttnshowwaiting.MaximumSize = new System.Drawing.Size(100, 65);
            this.bttnshowwaiting.MinimumSize = new System.Drawing.Size(100, 65);
            this.bttnshowwaiting.Name = "bttnshowwaiting";
            this.bttnshowwaiting.Size = new System.Drawing.Size(100, 65);
            this.bttnshowwaiting.TabIndex = 21;
            this.bttnshowwaiting.Text = "Show Waiting";
            this.bttnshowwaiting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnshowwaiting.UseVisualStyleBackColor = false;
            this.bttnshowwaiting.Click += new System.EventHandler(this.Bttnshowmydept_Click);
            // 
            // bttnshowapproved
            // 
            this.bttnshowapproved.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnshowapproved.AutoSize = true;
            this.bttnshowapproved.BackColor = System.Drawing.Color.Transparent;
            this.bttnshowapproved.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.bttnshowapproved.FlatAppearance.BorderSize = 2;
            this.bttnshowapproved.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.bttnshowapproved.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.bttnshowapproved.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.bttnshowapproved.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttnshowapproved.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnshowapproved.ForeColor = System.Drawing.Color.DimGray;
            this.bttnshowapproved.Image = ((System.Drawing.Image)(resources.GetObject("bttnshowapproved.Image")));
            this.bttnshowapproved.Location = new System.Drawing.Point(10, 20);
            this.bttnshowapproved.MaximumSize = new System.Drawing.Size(100, 65);
            this.bttnshowapproved.MinimumSize = new System.Drawing.Size(100, 65);
            this.bttnshowapproved.Name = "bttnshowapproved";
            this.bttnshowapproved.Size = new System.Drawing.Size(100, 65);
            this.bttnshowapproved.TabIndex = 21;
            this.bttnshowapproved.Text = "Show  All";
            this.bttnshowapproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bttnshowapproved.UseVisualStyleBackColor = false;
            this.bttnshowapproved.Click += new System.EventHandler(this.Bttnshowapproved_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.reloadbttn);
            this.groupBox1.Controls.Add(this.filterbttn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.frmdatepick);
            this.groupBox1.Controls.Add(this.todatepic);
            this.groupBox1.Location = new System.Drawing.Point(296, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 93);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter by Payment Date";
            // 
            // reloadbttn
            // 
            this.reloadbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadbttn.AutoSize = true;
            this.reloadbttn.BackColor = System.Drawing.Color.Transparent;
            this.reloadbttn.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.reloadbttn.FlatAppearance.BorderSize = 2;
            this.reloadbttn.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.reloadbttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.reloadbttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.reloadbttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reloadbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reloadbttn.ForeColor = System.Drawing.Color.DimGray;
            this.reloadbttn.Image = ((System.Drawing.Image)(resources.GetObject("reloadbttn.Image")));
            this.reloadbttn.Location = new System.Drawing.Point(468, 19);
            this.reloadbttn.MaximumSize = new System.Drawing.Size(100, 65);
            this.reloadbttn.MinimumSize = new System.Drawing.Size(100, 65);
            this.reloadbttn.Name = "reloadbttn";
            this.reloadbttn.Size = new System.Drawing.Size(100, 65);
            this.reloadbttn.TabIndex = 22;
            this.reloadbttn.Text = "Show Waiting";
            this.reloadbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.reloadbttn.UseVisualStyleBackColor = false;
            this.reloadbttn.Click += new System.EventHandler(this.Reloadbttn_Click);
            // 
            // filterbttn
            // 
            this.filterbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterbttn.AutoSize = true;
            this.filterbttn.BackColor = System.Drawing.Color.Transparent;
            this.filterbttn.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.filterbttn.FlatAppearance.BorderSize = 2;
            this.filterbttn.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.filterbttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.filterbttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.filterbttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterbttn.ForeColor = System.Drawing.Color.DimGray;
            this.filterbttn.Image = ((System.Drawing.Image)(resources.GetObject("filterbttn.Image")));
            this.filterbttn.Location = new System.Drawing.Point(359, 19);
            this.filterbttn.MaximumSize = new System.Drawing.Size(100, 65);
            this.filterbttn.MinimumSize = new System.Drawing.Size(100, 65);
            this.filterbttn.Name = "filterbttn";
            this.filterbttn.Size = new System.Drawing.Size(100, 65);
            this.filterbttn.TabIndex = 22;
            this.filterbttn.Text = "Filter";
            this.filterbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.filterbttn.UseVisualStyleBackColor = false;
            this.filterbttn.Click += new System.EventHandler(this.Filterbttn_Click);
            // 
            // emailallbttn
            // 
            this.emailallbttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailallbttn.AutoSize = true;
            this.emailallbttn.BackColor = System.Drawing.Color.Transparent;
            this.emailallbttn.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.emailallbttn.FlatAppearance.BorderSize = 2;
            this.emailallbttn.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.emailallbttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.emailallbttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.emailallbttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emailallbttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailallbttn.ForeColor = System.Drawing.Color.DimGray;
            this.emailallbttn.Image = ((System.Drawing.Image)(resources.GetObject("emailallbttn.Image")));
            this.emailallbttn.Location = new System.Drawing.Point(892, 79);
            this.emailallbttn.MaximumSize = new System.Drawing.Size(100, 65);
            this.emailallbttn.MinimumSize = new System.Drawing.Size(100, 65);
            this.emailallbttn.Name = "emailallbttn";
            this.emailallbttn.Size = new System.Drawing.Size(100, 65);
            this.emailallbttn.TabIndex = 22;
            this.emailallbttn.Text = "Email All";
            this.emailallbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.emailallbttn.UseVisualStyleBackColor = false;
            this.emailallbttn.Click += new System.EventHandler(this.emailallbttn_Click);
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = global::SearchDataSPM.Properties.Resources.spm_smal3;
            this.SPM.Location = new System.Drawing.Point(23, 51);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(241, 105);
            this.SPM.TabIndex = 26;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totallbl
            // 
            this.totallbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totallbl.AutoSize = true;
            this.totallbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totallbl.Location = new System.Drawing.Point(11, 672);
            this.totallbl.Name = "totallbl";
            this.totallbl.Size = new System.Drawing.Size(101, 20);
            this.totallbl.TabIndex = 27;
            this.totallbl.Text = "Total Value : ";
            // 
            // ReportAllRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 700);
            this.Controls.Add(this.totallbl);
            this.Controls.Add(this.SPM);
            this.Controls.Add(this.emailallbttn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.managergroupbox);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "ReportAllRecords";
            this.Style = MetroFramework.MetroColorStyle.Pink;
            this.Text = "SPM Connect Accounting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReportAllRecords_FormClosed);
            this.Load += new System.EventHandler(this.EvictionsHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.managergroupbox.ResumeLayout(false);
            this.managergroupbox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker frmdatepick;
        private System.Windows.Forms.DateTimePicker todatepic;
        private System.Windows.Forms.GroupBox managergroupbox;
        private System.Windows.Forms.Button bttnshowwaiting;
        private System.Windows.Forms.Button bttnshowapproved;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button filterbttn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem getWOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailtoolstrip;
        private System.Windows.Forms.Button emailallbttn;
        private System.Windows.Forms.Button reloadbttn;
        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Label totallbl;
    }
}