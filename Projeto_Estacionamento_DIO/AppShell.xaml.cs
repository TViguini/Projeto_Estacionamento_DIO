using Projeto_Estacionamento_DIO.Pages;

namespace Projeto_Estacionamento_DIO
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        public static void InitializeRoute()
        {
            Routing.RegisterRoute("Init_Page", typeof(Init_Page));
            Routing.RegisterRoute("Cadastro_Page", typeof(Cadastro_Page));
        }
    }
}
