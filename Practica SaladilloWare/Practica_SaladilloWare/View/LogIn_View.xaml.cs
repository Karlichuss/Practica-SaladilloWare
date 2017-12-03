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
        LogIn_View_Model ViewModel = new LogIn_View_Model();
        Usuario usuario;

        public LogIn_View()
        {
            InitializeComponent();

            btnLogIn.Clicked += async (sender, args) =>
            {
                await EntrarAsync();
            };
        }

        private async Task EntrarAsync()
        {
            await ViewModel.IniciarSesionAsync(txtNombre.Text, txtContrasenia.Text);

            if (!ViewModel.Error)
            {
                usuario = await ViewModel.GetUsuario(txtNombre.Text);

                if (ViewModel.EsCliente)
                {
                    await Navigation.PushModalAsync(new Cliente_View(usuario));
                }
                else
                {
                    await Navigation.PushModalAsync(new Vendor_View(usuario));
                }
            }
            else
            {
                await DisplayAlert("ERROR", "Usuario y/o contraseña incorrectos.", "OK");
                txtContrasenia.Text = "";
            }
        }
    }
}