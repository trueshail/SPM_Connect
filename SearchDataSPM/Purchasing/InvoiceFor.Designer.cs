namespace SearchDataSPM.Purchasing
{
    partial class InvoiceFor
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
            this.customer = new MetroFramework.Controls.MetroTile();
            this.vendor = new MetroFramework.Controls.MetroTile();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // customer
            // 
            this.customer.ActiveControl = null;
            this.customer.BackColor = System.Drawing.Color.CornflowerBlue;
            this.customer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.customer.Location = new System.Drawing.Point(19, 63);
            this.customer.Margin = new System.Windows.Forms.Padding(5);
            this.customer.MinimumSize = new System.Drawing.Size(100, 100);
            this.customer.Name = "customer";
            this.customer.Size = new System.Drawing.Size(106, 100);
            this.customer.TabIndex = 4;
            this.customer.Text = "Customer";
            this.customer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.customer.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.customer.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.customer.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.toolTip1.SetToolTip(this.customer, "Create Job folder with regular project template");
            this.customer.UseSelectable = true;
            this.customer.Click += new System.EventHandler(this.MetroTile1_Click);
            // 
            // vendor
            // 
            this.vendor.ActiveControl = null;
            this.vendor.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.vendor.Location = new System.Drawing.Point(155, 63);
            this.vendor.Margin = new System.Windows.Forms.Padding(5);
            this.vendor.MinimumSize = new System.Drawing.Size(100, 100);
            this.vendor.Name = "vendor";
            this.vendor.Size = new System.Drawing.Size(113, 100);
            this.vendor.TabIndex = 4;
            this.vendor.Text = "Vendor";
            this.vendor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.vendor.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.vendor.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.toolTip1.SetToolTip(this.vendor, "Create Job folder with spare project template");
            this.vendor.UseSelectable = true;
            this.vendor.Click += new System.EventHandler(this.MetroTile2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Shipping Request For";
            // 
            // InvoiceFor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(293, 185);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vendor);
            this.Controls.Add(this.customer);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "InvoiceFor";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.JobType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTile customer;
        private MetroFramework.Controls.MetroTile vendor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}