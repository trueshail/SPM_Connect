using System;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class lettergame : Form
    {
        private Random random = new Random();
        private Stats stats = new Stats();

        public lettergame()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Add a random key to the listBox

            letterBox.Items.Add((Keys)random.Next(65, 90));
            if (letterBox.Items.Count > 7)
            {
                letterBox.Items.Clear();
                letterBox.Items.Add("Game Over!");
                timer.Stop();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //If the user pressed a key that's in the letterBox, remove it
            //and then make the game move faster

            if (letterBox.Items.Contains(e.KeyCode))
            {
                letterBox.Items.Remove(e.KeyCode);
                letterBox.Refresh();
                if (timer.Interval > 400)
                    timer.Interval = timer.Interval - 10;
                if (timer.Interval > 250)
                    timer.Interval = timer.Interval - 7;
                if (timer.Interval > 100)
                    timer.Interval = timer.Interval - 2;
                progressBar.Value = 800 - timer.Interval;

                //User pressed a correct key, so update the stats object
                //by calling its update() method with argument true
                stats.Update(true);
            }
            else
            {
                //The user pressed incorrect key, so update the stats object
                //by calling its update() method with arguement false
                stats.Update(false);
            }

            //Update status strip labels
            correctLabel.Text = "Correct: " + stats.Correct;
            missedLabel.Text = "Missed: " + stats.Missed;
            accuracyLabel.Text = "Accuracry: " + stats.Accurate + "%";
        }

        private void correctLabel_Click(object sender, EventArgs e)
        {
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
    }
}