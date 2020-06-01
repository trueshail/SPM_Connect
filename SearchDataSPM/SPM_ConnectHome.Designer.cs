using Microsoft.VisualBasic.PowerPacks;

namespace SearchDataSPM
{
    partial class SPMConnectHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPMConnectHome));
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SPM = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.spmconnectlbl = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.SuspendLayout();
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.CustomBackground = true;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(285, 7);
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
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 45;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // SPM
            // 
            this.SPM.BackColor = System.Drawing.Color.Transparent;
            this.SPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SPM.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SPM.Image = ((System.Drawing.Image)(resources.GetObject("SPM.Image")));
            this.SPM.Location = new System.Drawing.Point(11, 28);
            this.SPM.Name = "SPM";
            this.SPM.Size = new System.Drawing.Size(306, 96);
            this.SPM.TabIndex = 11;
            this.SPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SPM.UseWaitCursor = true;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape2,
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(330, 196);
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
            this.rectangleShape2.Location = new System.Drawing.Point(21, 163);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.SelectionColor = System.Drawing.Color.White;
            this.rectangleShape2.Size = new System.Drawing.Size(20, 8);
            this.rectangleShape2.UseWaitCursor = true;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BorderColor = System.Drawing.Color.White;
            this.rectangleShape1.BorderWidth = 4;
            this.rectangleShape1.CornerRadius = 8;
            this.rectangleShape1.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Horizontal;
            this.rectangleShape1.Location = new System.Drawing.Point(16, 158);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(300, 18);
            this.rectangleShape1.UseWaitCursor = true;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 25;
            this.bunifuElipse1.TargetControl = this;
            // 
            // spmconnectlbl
            // 
            this.spmconnectlbl.AutoSize = true;
            this.spmconnectlbl.Font = new System.Drawing.Font("Cascadia Mono", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spmconnectlbl.ForeColor = System.Drawing.Color.White;
            this.spmconnectlbl.Location = new System.Drawing.Point(39, 124);
            this.spmconnectlbl.Name = "spmconnectlbl";
            this.spmconnectlbl.Size = new System.Drawing.Size(254, 25);
            this.spmconnectlbl.TabIndex = 13;
            this.spmconnectlbl.Text = "SPM Connect V7.20.5.26";
            this.spmconnectlbl.UseWaitCursor = true;
            // 
            // SPMConnectHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(330, 196);
            this.Controls.Add(this.spmconnectlbl);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.SPM);
            this.Controls.Add(this.shapeContainer1);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SPMConnectHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPM_ConnectHome";
            this.UseWaitCursor = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SPM_ConnectHome_FormClosed);
            this.Load += new System.EventHandler(this.SPM_ConnectHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label SPM;
        private ShapeContainer shapeContainer1;
        private RectangleShape rectangleShape2;
        private RectangleShape rectangleShape1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuCustomLabel spmconnectlbl;
    }
}