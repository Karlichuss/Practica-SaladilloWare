using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View_Model;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica_SaladilloWare.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn_View : ContentPage
    {
        LogIn_View_Model ViewModel;

        public LogIn_View()
        {
            InitializeComponent();

            ViewModel = new LogIn_View_Model(this, Navigation, txtNombre, txtContrasenia);

            BindingContext = ViewModel;

            btnLogIn.Clicked += async (sender, args) =>
            {
                await ViewModel.IniciarSesion();
            };
        }
    }
}