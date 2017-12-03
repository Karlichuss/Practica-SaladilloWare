using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View_Model;
using System;
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

            ViewModel = new Cliente_View_Model(usuario);

            InitViewsAsync();

            btnAceptar.Clicked += (sender, args) =>
            {
                // RellenarLista();
            };

            btnConfirmar.Clicked += (sender, args) =>
            {
                // RealizarPedido();
            };

            btnCancelar.Clicked += (sender, args) =>
            {
               // LimpiarFormulario();
            };

            btnLogOut.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new LogIn_View());
            };
        }

        private void InitViewsAsync()
        {
            DisplayAlert("¿Como realizar un pedido?",
                            "1. Seleccionas un componente de cada tipo para tu PC.\n" +
                            "¡Asegurate de que No has dejado ningun componente sin elegir!\n\n" +
                            "2. Pulsa en el boton Aceptar, y revisa los productos que has seleccionado.\n\n" +
                            "3. Si estas satisfecho con tu seleccion, pulsa Confirmar para realizar el pedido.",
                            "Vale, ¡Entendido!, ¡Empezamos!");

            lblBienvenida.Text = "Bienvenido, " + ViewModel.usuario.Nombre;

            RellenarPickersAsync();
        }

        private async Task RellenarPickersAsync()
        {
            // Rellenas cada Picker con los productos que hay en la base de datos.
            picPlacaBase.ItemsSource = await ViewModel.GetPlacasBase();
            picProcesador.ItemsSource = await ViewModel.GetProcesadores();
            picChasis.ItemsSource = await ViewModel.GetChasis();
            picMemoria.ItemsSource = await ViewModel.GetRAMs();
            picTarjetaGrafica.ItemsSource = await ViewModel.GetTarjetasGraficas();

        }

        private void LimpiarFormulario()
        {
            // Vuelves a vaciar el contenido de todos los Picker y la lista de Productos.
            picPlacaBase.SelectedItem = "";
            picProcesador.SelectedItem = "";
            picChasis.SelectedItem = "";
            picMemoria.SelectedItem = "";
            picTarjetaGrafica.SelectedItem = "";
            lstResumen.ItemsSource = "";
        }

        private void RellenarLista()
        {
            // Rellenas la lista de Productos con la seleccion de todos los Picker.
            throw new NotImplementedException();
        }

        private void RealizarPedido()
        {
            // Introduces el pedido en la base de datos.
            throw new NotImplementedException();
        }

       
    }
}