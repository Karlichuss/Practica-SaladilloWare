using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View_Model;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica_SaladilloWare.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vendor_View : ContentPage
    {
        Vendor_View_Model ViewModel;

        public Vendor_View(Usuario usuario)
        {
            InitializeComponent();

            ViewModel = new Vendor_View_Model(usuario);

            BindingContext = ViewModel;

            InitViewsAsync();

            btnActualizar.Clicked += async (sender, args) =>
            {
                await ViewModel.ActualizarPrecios();
            };

            btnLogOut.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new LogIn_View());
            };
        }

        private async Task InitViewsAsync()
        {
            await DisplayAlert("¿Qué puedo hacer?",
                            "1. Consultar los pedidos realizados.\n\n" +
                            "2. Importar los nuevos precios del catalogo desde un XML.",
                            "Vale, ¡Entendido!, ¡Empezamos!");

            lblBienvenida.Text = "Bienvenido, " + ViewModel.usuario.Nombre;

            lstResumen.ItemsSource = await ViewModel.CargarPedidosAsync();
        }
    }
}