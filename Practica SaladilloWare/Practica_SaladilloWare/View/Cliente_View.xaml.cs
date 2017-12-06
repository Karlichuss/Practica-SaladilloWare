using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View_Model;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica_SaladilloWare.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cliente_View : ContentPage
    {
        Cliente_View_Model ViewModel;

        public Cliente_View(Usuario usuario)
        {
            InitializeComponent();

            ViewModel = new Cliente_View_Model(this, Navigation, usuario, picPlacaBase, picProcesador, picChasis, picMemoria, picTarjetaGrafica, lstResumen, lblTotal, btnAceptar, btnConfirmar);

            BindingContext = ViewModel;

            InitViews();

            picPlacaBase.SelectedIndexChanged += (sender, args) =>
            {
                ViewModel.ComprobarSeleccion();
            };

            picProcesador.SelectedIndexChanged += (sender, args) =>
            {
                ViewModel.ComprobarSeleccion();
            };

            picChasis.SelectedIndexChanged += (sender, args) =>
            {
                ViewModel.ComprobarSeleccion();
            };

            picMemoria.SelectedIndexChanged += (sender, args) =>
            {
                ViewModel.ComprobarSeleccion();
            };

            picTarjetaGrafica.SelectedIndexChanged += (sender, args) =>
            {
                ViewModel.ComprobarSeleccion();
            };

            btnAceptar.Clicked += async (sender, args) =>
            {
                lstResumen.ItemsSource = await Cliente_View_Model.RellenarLista(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);

                lblTotal.Text = await Cliente_View_Model.ObtenerTotal(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);
            };

            btnConfirmar.Clicked += (sender, args) =>
            {
                ViewModel.RealizarPedido();
            };

            btnCancelar.Clicked += (sender, args) =>
            {
                ViewModel.LimpiarFormulario();
            };

            btnLogOut.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new LogIn_View());
            };
        }

        private async Task InitViews()
        {
            await DisplayAlert("¿Como realizar un pedido?",
                            "1. Seleccionas un componente de cada tipo para tu PC.\n" +
                            "¡Asegurate de que no has dejado ningun componente sin elegir!\n\n" +
                            "2. Pulsa en el boton Aceptar, y revisa los productos que has seleccionado.\n\n" +
                            "3. Si estas satisfecho con tu seleccion, pulsa Confirmar para realizar el pedido.",
                            "Vale, ¡Entendido!, ¡Empezamos!");

            lblBienvenida.Text = "Bienvenido, " + ViewModel.Usuario.Nombre;

            await ViewModel.RellenarPickers();
        }
    }
}