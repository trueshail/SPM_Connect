namespace SearchDataSPM.WorkOrder
{
    partial class ScanWO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanWO));
            this.wo_label = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.time_lbl = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.woid_txtbox = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // wo_label
            // 
            this.wo_label.BackColor = System.Drawing.Color.Transparent;
            this.wo_label.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wo_label.ForeColor = System.Drawing.Color.White;
            this.wo_label.Location = new System.Drawing.Point(74, 133);
            this.wo_label.Name = "wo_label";
            this.wo_label.Size = new System.Drawing.Size(113, 30);
            this.wo_label.TabIndex = 1;
            this.wo_label.Text = "Work Order :";
            this.wo_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // time_lbl
            // 
            this.time_lbl.AutoSize = true;
            this.time_lbl.BackColor = System.Drawing.Color.Transparent;
            this.time_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.time_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_lbl.ForeColor = System.Drawing.Color.White;
            this.time_lbl.Location = new System.Drawing.Point(3, 6);
            this.time_lbl.Name = "time_lbl";
            this.time_lbl.Size = new System.Drawing.Size(35, 13);
            this.time_lbl.TabIndex = 5;
            this.time_lbl.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 209);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(494, 48);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.Visible = false;
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(135, 2);
            this.label1.MaximumSize = new System.Drawing.Size(220, 100);
            this.label1.MinimumSize = new System.Drawing.Size(220, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 100);
            this.label1.TabIndex = 16;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // woid_txtbox
            // 
            // 
            // 
            // 
            this.woid_txtbox.CustomButton.Image = null;
            this.woid_txtbox.CustomButton.Location = new System.Drawing.Point(170, 1);
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
            this.woid_txtbox.Location = new System.Drawing.Point(183, 128);
            this.woid_txtbox.MaxLength = 32767;
            this.woid_txtbox.Name = "woid_txtbox";
            this.woid_txtbox.PasswordChar = '*';
            this.woid_txtbox.WaterMark = "Scan Work Order";
            this.woid_txtbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.woid_txtbox.SelectedText = "";
            this.woid_txtbox.SelectionLength = 0;
            this.woid_txtbox.SelectionStart = 0;
            this.woid_txtbox.ShortcutsEnabled = false;
            this.woid_txtbox.ShowClearButton = true;
            this.woid_txtbox.Size = new System.Drawing.Size(204, 35);
            this.woid_txtbox.TabIndex = 0;
            this.woid_txtbox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.woid_txtbox.UseSelectable = true;
            this.woid_txtbox.WaterMark = "Scan Work Order";
            this.woid_txtbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.woid_txtbox.WaterMarkFont = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.woid_txtbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.woid_txtbox_KeyPress);
            // 
            // ScanWO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(494, 257);
            this.Controls.Add(this.woid_txtbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.time_lbl);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.wo_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.Name = "ScanWO";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM Connect - Work Order Scanning";
            this.Activated += new System.EventHandler(this.ScanWO_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScanWO_FormClosed);
            this.Load += new System.EventHandler(this.Home_Load);
            this.Move += new System.EventHandler(this.ScanWO_Move);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label wo_label;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label time_lbl;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox woid_txtbox;
    }
}

