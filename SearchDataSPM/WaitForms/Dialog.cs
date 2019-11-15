using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class Dialog : Form
    {
        private List<Color> colors = new List<Color>();
        private int cur_color = 0;

        public string Message
        {
            set { this.bunifuCustomLabel1.Text = value; }
        }

        public Dialog()
        {
            InitializeComponent();
            colors.Add(Color.FromArgb(0, 150, 136));
            colors.Add(Color.FromArgb(0, 188, 212));
            colors.Add(Color.FromArgb(63, 81, 181));
            colors.Add(Color.FromArgb(156, 39, 176));

            bunifuCircleProgressbar1.ProgressColor = colors[cur_color];
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dialog_Load(object sender, EventArgs e)
        {
        }

        private void bunifuColorTransition1_OnValueChange(object sender, EventArgs e)
        {
        }

        private void color_transition_Tick(object sender, EventArgs e)
        {
            if (bunifuColorTransition1.ProgessValue < 100)
            {
                bunifuColorTransition1.ProgessValue++;
                bunifuCircleProgressbar1.ProgressColor = bunifuColorTransition1.Value;
            }
            else
            {
                color_transition.Stop();
                bunifuColorTransition1.Color1 = bunifuColorTransition1.Color2;
                bunifuColorTransition1.ProgessValue = 0;
            }
        }

        private int dir = 1;

        private void stretch_Tick(object sender, EventArgs e)
        {
            if (bunifuCircleProgressbar1.Value == 90)
            {
                dir = -1;  // reduce
                bunifuCircleProgressbar1.animationIterval = 4;
                SwitchColor();
            }
            else if (bunifuCircleProgressbar1.Value == 10)
            {
                dir = +1; //expand
                bunifuCircleProgressbar1.animationIterval = 2;
                SwitchColor();
            }

            bunifuCircleProgressbar1.Value += dir;
        }

        private void SwitchColor()
        {
            bunifuColorTransition1.Color1 = colors[cur_color];

            if (cur_color < colors.Count - 1)
            {
                cur_color++;
            }
            else
            {
                cur_color = 0;
            }
            bunifuColorTransition1.Color2 = colors[cur_color];

            color_transition.Start();
        }
    }
}