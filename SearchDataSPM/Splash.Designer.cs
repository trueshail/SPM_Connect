using Microsoft.VisualBasic.PowerPacks;

namespace SearchDataSPM
{
    partial class Splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.SPM = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.CustomBackground = true;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(291, 12);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(37, 36);
            this.metroProgressSpinner1.Speed = 2F;
            this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.White;
            this.metroProgressSpinner1.TabIndex = 1;
            this.metroProgressSpinner1.UseCustomBackColor = true;
            this.metroProgressSpinner1.UseCustomForeColor = true;
            this.metroProgressSpinner1.UseSelectable = true;
            this.metroProgressSpinner1.UseWaitCursor = true;
            this.metroProgressSpinner1.Click += new System.EventHandler(this.metroProgressSpinner1_Click);
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(11, 0);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(307, 161);
            this.SPM.TabIndex = 11;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SPM.UseWaitCursor = true;
            this.SPM.Click += new System.EventHandler(this.SPM_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape2,
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(330, 200);
            this.shapeContainer1.TabIndex = 12;
            this.shapeContainer1.TabStop = false;
            this.shapeContainer1.UseWaitCursor = true;
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.Color.White;
            this.rectangleShape2.BorderColor = System.Drawing.Color.White;
            this.rectangleShape2.BorderWidth = 4;
            this.rectangleShape2.CornerRadius = 4;
            this.rectangleShape2.FillColor = System.Drawing.Color.White;
            this.rectangleShape2.FillGradientColor = System.Drawing.Color.White;
            this.rectangleShape2.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Horizontal;
            this.rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape2.Location = new System.Drawing.Point(21, 170);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.SelectionColor = System.Drawing.Color.White;
            this.rectangleShape2.Size = new System.Drawing.Size(10, 8);
            this.rectangleShape2.UseWaitCursor = true;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BorderColor = System.Drawing.Color.White;
            this.rectangleShape1.BorderWidth = 4;
            this.rectangleShape1.CornerRadius = 8;
            this.rectangleShape1.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Horizontal;
            this.rectangleShape1.Location = new System.Drawing.Point(16, 165);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(300, 18);
            this.rectangleShape1.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 13;
            this.label1.UseWaitCursor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 190);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(330, 10);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.UseWaitCursor = true;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(330, 200);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.SPM);
            this.Controls.Add(this.shapeContainer1);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM_ConnectHome";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.SPM_ConnectHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.Label SPM;
        private ShapeContainer shapeContainer1;
        private RectangleShape rectangleShape2;
        private RectangleShape rectangleShape1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}