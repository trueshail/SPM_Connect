using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            var parametersa = reportViewer1.ServerReport.GetParameters();

            foreach (var para in parametersa)
            {

                if (para.Name == "pCode")
                {
                    Microsoft.Reporting.WinForms.ReportParameterCollection reportParameters = new Microsoft.Reporting.WinForms.ReportParameterCollection();
                    reportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter(para.Name, ""));
                    this.reportViewer1.ServerReport.SetParameters(reportParameters);
                    this.reportViewer1.RefreshReport();
                }

            }
        }

    }
}
