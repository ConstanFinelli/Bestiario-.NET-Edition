using Microsoft.VisualBasic.Logging;

namespace WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            LoginForm.UsuarioLogueado = null;
            API.Clients.BaseApiClient.ClearAuthToken();
            Application.Run(new Home());
        }
    }
}