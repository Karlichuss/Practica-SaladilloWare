using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View_Model;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace Practica_SaladilloWare.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn_View : ContentPage
    {
        #region Declaracion de variables

        // ViewModel asociado a la vista.
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

            InitViews();

            #region Acciones

            // Cuando hacemos click en Iniciar Sesión, realiza las comprobaciones de que el usuario y la contraseña son correctas y realiza la navegación a la vista que corresponda.
            btnLogIn.Clicked += async (sender, args) =>
            {
                await ViewModel.IniciarSesion();
            };

            // Cuando escribimos algo en el campo de texto, tenemos que controlar que no se metan mas de 4 caracteres.
            txtNombre.TextChanged += (sender, args) =>
            {
                ViewModel.ImpedirMaxCaracteres(4, txtNombre);
            };

            // Cuando escribimos algo en el campo de texto, tenemos que controlar que no se metan mas de 10 caracteres.
            txtContrasenia.TextChanged += (sender, args) =>
            {
                ViewModel.ImpedirMaxCaracteres(10, txtContrasenia);
            };

            #endregion

        }

        #endregion

        #region Metodos

        /// <summary>
        /// Realiza las operaciones de configuración de la vista para que cuando se abra, esta esté personalizada.
        /// </summary>
        private void InitViews()
        {
            // Impedimos que salte el null exception dandole valor inicial a cadena vacía a los campos.
            txtNombre.Text = "";
            txtContrasenia.Text = "";
        }

        #endregion

    }
}