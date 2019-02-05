using System;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace SearchDataSPM
{
    public partial class Splash : Form
    {
        public Splash()
        {

            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 4;
        }

        private void SPM_ConnectHome_Load(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void metroProgressSpinner1_Click(object sender, EventArgs e)
        {

        }

        private void SPM_Click(object sender, EventArgs e)
        {

        }

        private delegate void CloseDelegate();
        private delegate void UpdateDelegate(string txt);

        private Splash _splashInstance;

        //The initializer for the SplashScreen Class
       

        //Private method used to display the splash creen form.
        private void ShowForm()
        {
            _splashInstance = new Splash();
            Application.Run(_splashInstance);
        }

        //Public method that spawns a new thread and calls the ShowForm() method to lauch the splash screen in the new thread.
        public void ShowSplashScreen()
        {
            if (_splashInstance != null)
                return;
            Thread thread = new Thread(ShowForm);
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
           
        }

        //Public bool to check if the splash screen is ready.
        public bool Ready()
        {
            return (_splashInstance != null) && (_splashInstance.Created);
        }

        //Public method used to update the progress bar progress and text from your main application thread.
        public void UpdateProgress(string txt)
        {
            _splashInstance.Invoke(new UpdateDelegate(UpdateProgressInternal), txt);

        }

        //Public method used to close the splash screen from your main application thread.
        public void CloseForm()
        {
            _splashInstance.Invoke(new CloseDelegate(CloseFormInternal));
        }

        //The private method invoked by the delegate to update the progress bar.
        private void UpdateProgressInternal(string txt)
        {
            _splashInstance.progressBar1.Value++;
            _splashInstance.rectangleShape2.Width += 70;
           // rectangleShape2.Width = rectangleShape2.Width + 80;
            _splashInstance.label1.Text = txt;
        }

        //The private method invoked by the delegate to close the splash screen.
        private void CloseFormInternal()
        {
            _splashInstance.Close();
        }



    }
}
