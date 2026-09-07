using Microsoft.VisualBasic.Logging;

namespace WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {

                Application.Run(new Home());
            }
            else
            {
                Application.Exit();
            }
            Application.Run(new LoginForm());
        }
    }
}