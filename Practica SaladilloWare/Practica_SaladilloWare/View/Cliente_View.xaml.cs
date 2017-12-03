
using System;
using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View_Model;
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

            InitViews();

            btnAceptar.Clicked += (sender, args) =>
            {
                RellenarLista();
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

        private void InitViews()
        {
            DisplayAlert("¿Como realizar un pedido?",
                            "1. Seleccionas un componente de cada tipo para tu PC.\n" +
                            "¡Asegurate de que No has dejado ningun componente sin elegir!\n\n" +
                            "2. Pulsa en el boton Aceptar, y revisa los productos que has seleccionado.\n\n" +
                            "3. Si estas satisfecho con tu seleccion, pulsa Confirmar para realizar el pedido.",
                            "Vale, ¡Entendido!, ¡Empezamos!");

            lblBienvenida.Text = "Bienvenido, " + ViewModel.usuario.Nombre;

            //RellenarPickers();
        }

        private void RellenarPickers()
        {
            throw new NotImplementedException();
        }

        private void LimpiarFormulario()
        {
            // Vuelves a vaciar el contenido de todos los Picker y la lista de Productos.
            throw new NotImplementedException();
        }

        private void RealizarPedido()
        {
            // Introduces el pedido en la base de datos.
            throw new NotImplementedException();
        }

        private void RellenarLista()
        {
            // Rellenas la lista de Productos con la seleccion de todos los Picker.
            throw new NotImplementedException();
        }
    }
}