using eDrawingHostControl;
using System.IO;
using System.Windows.Forms;

namespace SearchDataSPM.Edrawings
{
    public partial class EModelViewer : Form
    {
        eDrawingControl ctrl = null;
        public EModelViewer()
        {
            InitializeComponent();
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

        void openfile(string filename)
        {
            if (File.Exists(filename)) { }
            ctrl.eDrawingControlWrapper.OpenDoc(filename, false, false, true, "");
        }

        private void EModelViewer_Load(object sender, System.EventArgs e)
        {
            string filename = @"\\spm-adfs\CAD Data\AAACAD\A78\A78001.sldasm";
            if (File.Exists(filename)) { }
            ctrl.eDrawingControlWrapper.OpenDoc(filename, false, false, true, "");
        }
    }
}
