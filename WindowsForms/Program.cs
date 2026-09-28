using Microsoft.VisualBasic.Logging;

namespace WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new Home());
        }
    }
}