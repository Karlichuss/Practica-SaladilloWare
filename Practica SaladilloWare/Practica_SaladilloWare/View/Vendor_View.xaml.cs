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

        #region Declaracion de variables.

        // ViewModel asociado a la vista.
        Vendor_View_Model ViewModel;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Le pasa el usuario del parametro a su ViewModel.
        /// </summary>
        /// <param name="usuario">El usuario que inicia la sesion.</param>
        public Vendor_View(Usuario usuario)
        {
            InitializeComponent();

            // Inicializa el ViewModel.
            ViewModel = new Vendor_View_Model(this, Navigation, usuario, lstResumen);

            // Define el BindingContext al ViewModel.
            BindingContext = ViewModel;

            // Hacemos las primeras operaciones para configurar la vista.
            InitViews();

            // Cuando hacemos click en Actualizar, actualizamos el catalogo a los nuevos preductos que hay en un archivo XML.
            btnActualizar.Clicked += async (sender, args) =>
            {
                await ViewModel.ActualizarPreciosAsync();
            };

            // Cuando hacemos click en Cerrar Sesion, volvemos a la vista de LogIn.
            btnLogOut.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new LogIn_View());
            };
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Realiza las operaciones de configuracion de la vista para cuando abrimos la vista.
        /// Mostramos un mensaje de bienvenida y rellenamos la lista con los pedidos.
        /// </summary>
        /// <returns></returns>
        private async Task InitViews()
        {
            await DisplayAlert("¿Qué puedo hacer?",
                            "1. Consultar los pedidos realizados.\n\n" +
                            "2. Importar los nuevos precios del catalogo desde un XML.",
                            "Vale, ¡Entendido!, ¡Empezamos!");

            lblBienvenida.Text = "Bienvenido, " + ViewModel.Usuario.Nombre;

            lstResumen.ItemsSource = await ViewModel.CargarPedidos();
        }

        #endregion

    }
}