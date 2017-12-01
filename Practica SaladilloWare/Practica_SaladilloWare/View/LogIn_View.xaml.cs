using Practica_SaladilloWare.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica_SaladilloWare.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn_View : ContentPage
    {
        LogIn_View_Model ViewModel = new LogIn_View_Model();

        public LogIn_View()
        {
            InitializeComponent();

            btnLogIn.Clicked += async (sender, args) =>
            {
                await ViewModel.IniciarSesionAsync(txtNombre.Text, txtContrasenia.Text);

                Entrar();
            };
        }

        private void Entrar()
        {
            if (!ViewModel.Error)
            {
                if (ViewModel.EsCliente)
                {
                    Navigation.PushModalAsync(new Cliente_View());
                }
                else
                {
                    Navigation.PushModalAsync(new Vendor_View());
                }
            }
            else
            {
                txtNombre.Text = "";
                txtContrasenia.Text = "";
            }
        }
    }
}