namespace SearchDataSPM.General
{
    partial class ShippingManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShippingManagement));
            this.SPM = new System.Windows.Forms.Label();
            this.Reload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.filter4 = new System.Windows.Forms.TextBox();
            this.filteroemitem_txtbox = new System.Windows.Forms.TextBox();
            this.filteroem_txtbox = new System.Windows.Forms.TextBox();
            this.Descrip_txtbox = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.TreeViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.addnewbttn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = global::SearchDataSPM.Properties.Resources.spm_smal3;
            this.SPM.Location = new System.Drawing.Point(10, 5);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(241, 89);
            this.SPM.TabIndex = 11;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Reload
            // 
            this.Reload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reload.Location = new System.Drawing.Point(766, 22);
            this.Reload.MaximumSize = new System.Drawing.Size(140, 30);
            this.Reload.MinimumSize = new System.Drawing.Size(140, 30);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(140, 30);
            this.Reload.TabIndex = 17;
            this.Reload.Text = "Refresh / Show All";
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
            this.txtSearch.Location = new System.Drawing.Point(257, 25);
            this.txtSearch.MaximumSize = new System.Drawing.Size(32767, 25);
            this.txtSearch.MinimumSize = new System.Drawing.Size(4, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(492, 25);
            this.txtSearch.TabIndex = 12;
            this.TreeViewToolTip.SetToolTip(this.txtSearch, "Search keyword");
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // filter4
            // 
            this.filter4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filter4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filter4.Location = new System.Drawing.Point(815, 66);
            this.filter4.MaximumSize = new System.Drawing.Size(180, 26);
            this.filter4.MinimumSize = new System.Drawing.Size(120, 25);
            this.filter4.Name = "filter4";
            this.filter4.Size = new System.Drawing.Size(180, 26);
            this.filter4.TabIndex = 16;
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
            this.filteroemitem_txtbox.Location = new System.Drawing.Point(629, 66);
            this.filteroemitem_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.filteroemitem_txtbox.MinimumSize = new System.Drawing.Size(120, 25);
            this.filteroemitem_txtbox.Name = "filteroemitem_txtbox";
            this.filteroemitem_txtbox.Size = new System.Drawing.Size(180, 26);
            this.filteroemitem_txtbox.TabIndex = 15;
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
            this.filteroem_txtbox.Location = new System.Drawing.Point(443, 66);
            this.filteroem_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.MinimumSize = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.Name = "filteroem_txtbox";
            this.filteroem_txtbox.Size = new System.Drawing.Size(180, 26);
            this.filteroem_txtbox.TabIndex = 14;
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
            this.Descrip_txtbox.Location = new System.Drawing.Point(257, 65);
            this.Descrip_txtbox.MaximumSize = new System.Drawing.Size(180, 26);
            this.Descrip_txtbox.MinimumSize = new System.Drawing.Size(180, 26);
            this.Descrip_txtbox.Name = "Descrip_txtbox";
            this.Descrip_txtbox.Size = new System.Drawing.Size(180, 26);
            this.Descrip_txtbox.TabIndex = 13;
            this.Descrip_txtbox.Visible = false;
            this.Descrip_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Descrip_txtbox_KeyDown);
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
            this.dataGridView.Location = new System.Drawing.Point(2, 98);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(996, 599);
            this.dataGridView.TabIndex = 18;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseMove);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting);
            this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_DataBindingComplete);
            // 
            // TreeViewToolTip
            // 
            this.TreeViewToolTip.AutoPopDelay = 4000;
            this.TreeViewToolTip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TreeViewToolTip.InitialDelay = 500;
            this.TreeViewToolTip.ReshowDelay = 100;
            // 
            // addnewbttn
            // 
            this.addnewbttn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addnewbttn.BackColor = System.Drawing.Color.Transparent;
            this.addnewbttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addnewbttn.ForeColor = System.Drawing.Color.Black;
            this.addnewbttn.Image = ((System.Drawing.Image)(resources.GetObject("addnewbttn.Image")));
            this.addnewbttn.Location = new System.Drawing.Point(919, 4);
            this.addnewbttn.MaximumSize = new System.Drawing.Size(65, 56);
            this.addnewbttn.MinimumSize = new System.Drawing.Size(65, 56);
            this.addnewbttn.Name = "addnewbttn";
            this.addnewbttn.Size = new System.Drawing.Size(65, 56);
            this.addnewbttn.TabIndex = 20;
            this.addnewbttn.Text = "Add New";
            this.addnewbttn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TreeViewToolTip.SetToolTip(this.addnewbttn, "Create New Quote");
            this.addnewbttn.UseVisualStyleBackColor = false;
            this.addnewbttn.Click += new System.EventHandler(this.addnewbttn_Click);
            // 
            // ShippingManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.addnewbttn);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.Reload);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.filter4);
            this.Controls.Add(this.filteroemitem_txtbox);
            this.Controls.Add(this.filteroem_txtbox);
            this.Controls.Add(this.Descrip_txtbox);
            this.Controls.Add(this.SPM);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "ShippingManagement";
            this.Text = "Shipping Management";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_ConnectJobs_FormClosed);
            this.Load += new System.EventHandler(this.SPM_ConnectQuoteManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SPM;
        private System.Windows.Forms.Button Reload;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox filter4;
        private System.Windows.Forms.TextBox filteroemitem_txtbox;
        private System.Windows.Forms.TextBox filteroem_txtbox;
        private System.Windows.Forms.TextBox Descrip_txtbox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolTip TreeViewToolTip;
        private System.Windows.Forms.Button addnewbttn;
    }
}