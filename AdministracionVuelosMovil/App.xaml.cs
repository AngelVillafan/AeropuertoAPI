using AdministracionVuelosMovil.Views;

namespace AdministracionVuelosMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomeView();
        }
    }
}