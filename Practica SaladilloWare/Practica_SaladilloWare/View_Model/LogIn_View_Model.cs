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
        #region Declaracion de variables

        Usuario Usuario; // El usuario que inicia sesion.
        INavigation Navigation; // Necesario para poder navegar entre las vistas.
        Page Page; // Necesario para poder realizar dialogos.

        // Elementos del layout
        Entry txtNombre, txtContrasenia;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Necesita muchos datos de la vista asociada, ya que esta es la parte logica.
        /// </summary>
        /// <param name="page">El code behind de la vista asociada.</param>
        /// <param name="navigation">Necesario para poder navegar entre vistas.</param>
        /// <param name="txtNombre"></param>
        /// <param name="txtContrasenia"></param>
        public LogIn_View_Model(Page page, INavigation navigation, Entry txtNombre, Entry txtContrasenia)
        {
            Page = page;
            Navigation = navigation;
            this.txtNombre = txtNombre;
            this.txtContrasenia = txtContrasenia;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Realiza las comprobaciones de que el formulario esta rellenado correctamente, y comprueba que el usuario y la contraseña son correctos.
        /// </summary>
        /// <returns></returns>
        public async Task IniciarSesion()
        {
            // Primero comprobamos que el usuario no ha dejado algun campo vacio.
            if (string.IsNullOrEmpty(txtNombre.Text.ToString()) || string.IsNullOrEmpty(txtContrasenia.Text.ToString()))
            {
                await Page.DisplayAlert("ERROR", "Por favor rellena correctamente el formulario.", "OK");
            }
            else
            {
                // Luego se comprueba si el usuario existe en la base de datos.
                if (!await Usuario_Repository.ComprobarNombre(txtNombre.Text))
                {
                    await Page.DisplayAlert("ERROR", "No existe un usuario con ese nombre.", "OK");
                }
                else
                {
                    Usuario = await Usuario_Repository.GetUsuario(txtNombre.Text);

                    // Luego comprobamos si la contraseña introducida es correcta.
                    if (Usuario.Contrasenia != txtContrasenia.Text)
                    {
                        await Page.DisplayAlert("ERROR", "Contraseña incorrecta.", "OK");
                    }
                    else
                    {
                        //Si es cliente inicia la sesion de cliente
                        if (Usuario.Tipo == "C")
                        {
                            Cliente_View Cliente_View = new Cliente_View(Usuario);
                            await Navigation.PushModalAsync(Cliente_View);
                        }
                        //Si no inicia la sesion de vendor
                        else
                        {
                            Vendor_View Vendor_View = new Vendor_View(Usuario);
                            await Navigation.PushModalAsync(Vendor_View);
                        }
                    }
                }
            }
        }

        #endregion

    }
}