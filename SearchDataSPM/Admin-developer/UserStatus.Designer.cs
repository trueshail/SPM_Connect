namespace SearchDataSPM.Admin_developer
{
    partial class UserStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserStatus));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Listviewcontextmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.freeuser = new System.Windows.Forms.ToolStripMenuItem();
            this.updateAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutDownAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Listviewcontextmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersHeight = 35;
            this.dataGridView1.ContextMenuStrip = this.Listviewcontextmenu;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 80;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(584, 461);
            this.dataGridView1.TabIndex = 0;
            // 
            // Listviewcontextmenu
            // 
            this.Listviewcontextmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.freeuser,
            this.updateAllToolStripMenuItem,
            this.shutDownAllToolStripMenuItem});
            this.Listviewcontextmenu.Name = "contextMenuStrip1";
            this.Listviewcontextmenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.Listviewcontextmenu.Size = new System.Drawing.Size(181, 92);
            // 
            // freeuser
            // 
            this.freeuser.Image = ((System.Drawing.Image)(resources.GetObject("freeuser.Image")));
            this.freeuser.Name = "freeuser";
            this.freeuser.Size = new System.Drawing.Size(180, 22);
            this.freeuser.Text = "Free Up User";
            this.freeuser.ToolTipText = "Free User Licence";
            this.freeuser.Click += new System.EventHandler(this.freeuser_Click);
            // 
            // updateAllToolStripMenuItem
            // 
            this.updateAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateAllToolStripMenuItem.Image")));
            this.updateAllToolStripMenuItem.Name = "updateAllToolStripMenuItem";
            this.updateAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateAllToolStripMenuItem.Text = "Update All";
            this.updateAllToolStripMenuItem.Click += new System.EventHandler(this.updateAllToolStripMenuItem_Click);
            // 
            // shutDownAllToolStripMenuItem
            // 
            this.shutDownAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("shutDownAllToolStripMenuItem.Image")));
            this.shutDownAllToolStripMenuItem.Name = "shutDownAllToolStripMenuItem";
            this.shutDownAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.shutDownAllToolStripMenuItem.Text = "Shut Down All";
            this.shutDownAllToolStripMenuItem.Click += new System.EventHandler(this.shutDownAllToolStripMenuItem_Click);
            // 
            // UserStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Status";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserStatus_FormClosed);
            this.Load += new System.EventHandler(this.UserStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Listviewcontextmenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip Listviewcontextmenu;
        private System.Windows.Forms.ToolStripMenuItem freeuser;
        private System.Windows.Forms.ToolStripMenuItem updateAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shutDownAllToolStripMenuItem;
    }
}