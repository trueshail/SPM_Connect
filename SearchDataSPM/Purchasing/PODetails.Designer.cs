namespace SearchDataSPM.Purchasing
{
    partial class PODetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PODetails));
            this.ponumbertxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.savebttn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ponumbertxt
            // 
            this.ponumbertxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ponumbertxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ponumbertxt.Location = new System.Drawing.Point(155, 29);
            this.ponumbertxt.Name = "ponumbertxt";
            this.ponumbertxt.Size = new System.Drawing.Size(200, 26);
            this.ponumbertxt.TabIndex = 0;
            this.ponumbertxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ponumbertxt_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "PO Number :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(155, 88);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DateTimePicker1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "PO Date :";
            // 
            // savebttn
            // 
            this.savebttn.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.savebttn.FlatAppearance.BorderSize = 2;
            this.savebttn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.savebttn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.savebttn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savebttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebttn.Location = new System.Drawing.Point(156, 137);
            this.savebttn.Name = "savebttn";
            this.savebttn.Size = new System.Drawing.Size(104, 34);
            this.savebttn.TabIndex = 11;
            this.savebttn.Text = "Save";
            this.savebttn.UseVisualStyleBackColor = true;
            this.savebttn.Click += new System.EventHandler(this.Savebttn_Click);
            // 
            // PODetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(420, 183);
            this.ControlBox = false;
            this.Controls.Add(this.savebttn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ponumbertxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PODetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PO Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PODetails_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PODetails_FormClosed);
            this.Load += new System.EventHandler(this.ImportFileSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ponumbertxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button savebttn;
    }
}