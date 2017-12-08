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
        #region Declaracion de variables

        // ViewModel asociado a la vista.
        Cliente_View_Model ViewModel;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Le pasa el usuario del parametro a su ViewModel.
        /// </summary>
        /// <param name="usuario">El usuario que inicia la sesión.</param>
        public Cliente_View(Usuario usuario)
        {
            InitializeComponent();

            // Inicializa el ViewModel.
            ViewModel = new Cliente_View_Model(this, Navigation, usuario, picPlacaBase, picProcesador, picChasis, picMemoria, picTarjetaGrafica, lstResumen, lblTotal, btnAceptar, btnConfirmar);

            // Define el BindingContext al ViewModel.
            BindingContext = ViewModel;

            // Hacemos las primeras operaciones para configurar la vista.
            InitViews();

            #region Acciones

            // El metodo ComprobarSeleccion() es llamado cada vez que tocamos algún valor de cualquiera de los Pickers.
            // Hasta que no esten todos los pickers con algún valor seleccionado, no podremos usar los botones Aceptar y Confirmar.
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

            // Cuando hacemos click en Aceptar, generamos el contenido de la lista Resumen.
            btnAceptar.Clicked += (sender, args) =>
            {
                ViewModel.RellenarResumenAsync();
            };

            // Cuando hacemos click en Confirmar, introducimos el pedido en la base de datos y se le notifica al usuario.
            btnConfirmar.Clicked += (sender, args) =>
            {
                ViewModel.RealizarPedido();
            };

            // Cuando hacemos click en Cancelar, reseteamos la vista.
            btnCancelar.Clicked += (sender, args) =>
            {
                ViewModel.LimpiarFormulario();
            };

            // Cuando hacemos click en Cerrar Sesión, volvemos a la vista de LogIn.
            btnLogOut.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new LogIn_View());
            };

            #endregion

        }

        #endregion

        #region Metodos

        /// <summary>
        /// Realiza las operaciones de configuración de la vista para que cuando se abra, esta esté personalizada.
        /// Mostramos un mensaje de bienvenida y rellenamos los menús deplegables con el catálogo.
        /// </summary>
        /// <returns></returns>
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

        #endregion
    }
}