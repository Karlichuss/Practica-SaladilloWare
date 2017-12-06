using Practica_SaladilloWare.Assets;
using System;
using System.Threading.Tasks;
using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.View;
using Xamarin.Forms;

namespace Practica_SaladilloWare.View_Model
{
    public class LogIn_View_Model
    {
        public Boolean Error { get; set; }
        public Boolean EsCliente { get; set; }
        public Usuario Usuario;
        INavigation Navigation;
        Page Page;
        Entry txtNombre, txtContrasenia;

        public LogIn_View_Model(Page page, INavigation navigation, Entry txtNombre, Entry txtContrasenia)
        {
            Page = page;
            Navigation = navigation;
            this.txtNombre = txtNombre;
            this.txtContrasenia = txtContrasenia;
            Error = false;
            EsCliente = false;
        }

        public async Task IniciarSesionAsync()
        {
            Cliente_View Cliente_View;
            Vendor_View Vendor_View;

            if (string.IsNullOrEmpty(txtNombre.Text.ToString()) || string.IsNullOrEmpty(txtContrasenia.Text.ToString()))
            {
                await Page.DisplayAlert("ERROR", "Por favor rellena correctamente el formulario.", "OK");
            }
            else
            {
                if (!await Usuario_Repository.ComprobarNombre(txtNombre.Text))
                {
                    await Page.DisplayAlert("ERROR", "No existe un usuario con ese nombre.", "OK");
                }
                else
                {
                    Usuario = await Usuario_Repository.GetUsuario(txtNombre.Text);

                    if (Usuario.Contrasenia != txtContrasenia.Text)
                    {
                        await Page.DisplayAlert("ERROR", "Contraseña incorrecta.", "OK");
                    }
                    else
                    {
                        //Si es cliente inicia la interfaz de cliente
                        if (Usuario.Tipo == "C")
                        {
                            Cliente_View = new Cliente_View(Usuario);
                            await Navigation.PushModalAsync(Cliente_View);
                        }
                        //Si no inicia la interfaz de dueño
                        else
                        {
                            Vendor_View = new Vendor_View(Usuario);
                            await Navigation.PushModalAsync(Vendor_View);
                        }
                    }
                }
            }
        }
    }
}