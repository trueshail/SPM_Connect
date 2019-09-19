namespace SearchDataSPM
{
    partial class ScanEmpId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanEmpId));
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.empid_txtbox = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scan Your Employee ID";
            // 
            // empid_txtbox
            // 
            // 
            // 
            // 
            this.empid_txtbox.CustomButton.Image = null;
            this.empid_txtbox.CustomButton.Location = new System.Drawing.Point(170, 1);
            this.empid_txtbox.CustomButton.Name = "";
            this.empid_txtbox.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.empid_txtbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.empid_txtbox.CustomButton.TabIndex = 1;
            this.empid_txtbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.empid_txtbox.CustomButton.UseSelectable = true;
            this.empid_txtbox.CustomButton.Visible = false;
            this.empid_txtbox.DisplayIcon = true;
            this.empid_txtbox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.empid_txtbox.Icon = ((System.Drawing.Image)(resources.GetObject("empid_txtbox.Icon")));
            this.empid_txtbox.Lines = new string[0];
            this.empid_txtbox.Location = new System.Drawing.Point(38, 91);
            this.empid_txtbox.MaxLength = 32767;
            this.empid_txtbox.Name = "empid_txtbox";
            this.empid_txtbox.PasswordChar = '*';
            this.empid_txtbox.WaterMark = "Scan Emp Id";
            this.empid_txtbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.empid_txtbox.SelectedText = "";
            this.empid_txtbox.SelectionLength = 0;
            this.empid_txtbox.SelectionStart = 0;
            this.empid_txtbox.ShortcutsEnabled = false;
            this.empid_txtbox.ShowClearButton = true;
            this.empid_txtbox.Size = new System.Drawing.Size(204, 35);
            this.empid_txtbox.TabIndex = 0;
            this.empid_txtbox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.toolTip1.SetToolTip(this.empid_txtbox, "Please scan your employee id");
            this.empid_txtbox.UseSelectable = true;
            this.empid_txtbox.WaterMark = "Scan Emp Id";
            this.empid_txtbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.empid_txtbox.WaterMarkFont = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.empid_txtbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.empid_txtbox_KeyPress);
            // 
            // ScanEmpId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(293, 185);
            this.Controls.Add(this.empid_txtbox);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "ScanEmpId";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanEmpId_FormClosing);
            this.Load += new System.EventHandler(this.JobType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private MetroFramework.Controls.MetroTextBox empid_txtbox;
    }
}