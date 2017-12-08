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
        #region Declaracion de variables

        LogIn_View_Model ViewModel;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Realiza el enlace con el ViewModel asociado.
        /// </summary>
        public LogIn_View()
        {
            InitializeComponent();

            // Inicializa el ViewModel.
            ViewModel = new LogIn_View_Model(this, Navigation, txtNombre, txtContrasenia);

            // Define el BindingContext al ViewModel.
            BindingContext = ViewModel;

            #region Acciones

            // Cuando hacemos click en Iniciar Sesion, realiza las comprobaciones de que el usuario y la contraseña son correctas y realiza la navegacion a la vista que corresponda.
            btnLogIn.Clicked += async (sender, args) =>
            {
                await ViewModel.IniciarSesion();
            };

            #endregion

        }

        #endregion

    }
}