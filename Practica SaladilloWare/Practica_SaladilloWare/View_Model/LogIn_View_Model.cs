using Practica_SaladilloWare.Assets;
using System;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.View_Model
{
    public class LogIn_View_Model
    {
        public Boolean Error { get; set; }
        public Boolean EsCliente { get; set; }

        public LogIn_View_Model()
        {
            Error = false;
            EsCliente = false;
        }

        public async Task IniciarSesionAsync(String nombre, String contrasenia)
        {
            //Si la contraseña y el nombre son correctos se determina que tipo de usuario es
            if (await Usuario_Repository.ComprobarLogin(nombre, contrasenia))
            {
                //Si es cliente inicia la interfaz de cliente
                Error = false;
                EsCliente = await Usuario_Repository.EsCliente(nombre);
            }
            else
            {
                Error = true;
            }
        }
    }
}