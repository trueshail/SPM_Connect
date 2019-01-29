using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM.Engineering
{
    public partial class WaitFormLoading : MetroFramework.Forms.MetroForm
    {
       

        public WaitFormLoading()
        {
            InitializeComponent();
            
        }


        private void metroProgressSpinner1_Click(object sender, EventArgs e)
        {

        }

        private void WaitFormOpening_Load(object sender, EventArgs e)
        {
           
        }

        private void WaitFormOpening_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}