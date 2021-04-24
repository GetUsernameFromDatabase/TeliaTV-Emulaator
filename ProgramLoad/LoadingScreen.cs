using System.Threading;
using System.Windows.Forms;

namespace TTVTL_Nuppudega
{
    public partial class LoadingScreen : Form
    {
        public LoadingScreen()
        {
            InitializeComponent();
            this.BackColor = Program.BackColour;
            this.Text = Program.name;
        }
    }
    public class SplashForm : Form
    {
        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static LoadingScreen splashForm;

        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.    
            if (splashForm != null) return;
            splashForm = new LoadingScreen();
            Thread thread = new Thread(new ThreadStart(ShowForm)) { IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            if (splashForm != null) Application.Run(splashForm);
        }

        static public void CloseForm()
        {
            splashForm?.Invoke(new CloseDelegate(CloseFormInternal));
        }

        static private void CloseFormInternal()
        {
            splashForm?.Close();
            splashForm = null; // For garbage collection
        }
    }
}
