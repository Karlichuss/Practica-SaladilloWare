using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View_Model;
using System;
using System.Collections.Generic;
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

            btnAceptar.Clicked += async (sender, args) =>
            {
                lstResumen.ItemsSource = await Cliente_View_Model.RellenarLista(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);

                lblTotal.Text = await Cliente_View_Model.ObtenerTotal(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);
            };

            btnConfirmar.Clicked += (sender, args) =>
            {
                RealizarPedido();
            };

            btnCancelar.Clicked += (sender, args) =>
            {
                LimpiarFormulario();
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

            RellenarPickers();
        }

        private async Task RellenarPickers()
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
            picPlacaBase.SelectedItem = -1;
            picProcesador.SelectedItem = -1;
            picChasis.SelectedItem = -1;
            picMemoria.SelectedItem = -1;
            picTarjetaGrafica.SelectedItem = -1;

            lstResumen.ItemsSource = new List<String>();
        }

        private void RealizarPedido()
        {
            // Introduces en la base de datos un nuevo pedido con los IDs de los productos seleccionados y el ID del usuario. Luego limpiamos el formulario.
            ViewModel.GenerarPedido(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);

            DisplayAlert("¡Gracias por su confianza!",
                            "Pedido realizado con éxito.",
                            "Limpiar formulario y realizar otro pedido");

            LimpiarFormulario();
        }
    }
}