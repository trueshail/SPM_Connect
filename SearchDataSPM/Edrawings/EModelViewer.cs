using eDrawingHostControl;
using System;
using System.IO;
using System.Windows.Forms;

namespace SearchDataSPM.Edrawings
{
    public partial class EModelViewer : Form
    {
        eDrawingControl ctrl = null;
        string filename = "";

        public EModelViewer()
        {
            InitializeComponent();
            try
            {
                if (null == ctrl)
                {
                    ctrl = new eDrawingControl();
                }

                this.Controls.Add(ctrl);
                this.ctrl.BackColor = System.Drawing.Color.White;
                this.ctrl.Location = new System.Drawing.Point(46, 56);
                this.ctrl.Name = "eDrawingControl1";
                this.ctrl.Size = new System.Drawing.Size(479, 314);
                this.ctrl.TabIndex = 0;
                this.ctrl.Dock = DockStyle.Fill;
                ctrl.EnableFeatures = 16;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Emodel Viewer Failed to Launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            
        }

        public string filetoopen(string item)
        {
            if (item.Length > 0)
                return filename = item;
            return null;
        }

        void openfile(string filename)
        {
            try
            {
                if (File.Exists(filename)) { }
                ctrl.eDrawingControlWrapper.OpenDoc(filename, false, false, true, "");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Emodel Viewer Failed to File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
          
        }

        private void EModelViewer_Load(object sender, System.EventArgs e)
        {
            try
            {
                //string filename = @"\\spm-adfs\CAD Data\AAACAD\A78\A78001.sldasm";
                if (File.Exists(filename)) { }
                ctrl.eDrawingControlWrapper.OpenDoc(filename, false, false, true, "");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "SPM Connect - Emodel Viewer Failed to Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
           
        }
    }
}
