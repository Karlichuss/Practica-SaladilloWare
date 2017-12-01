using APP12_TiendaInformatica.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP12_TiendaInformatica.Asset
{
    public class UsuarioRepository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        /// <summary>
        /// Crea la tabla y la conexion 
        /// </summary>
        /// <param name="dbPath">Ruta di¡onde se aloja la bdd</param>
        public UsuarioRepository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // TODO: Create the Person table
            //Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Usuario>().Wait();
        }

        /// <summary>
        /// Devuelve todos los usuarios
        /// </summary>
        /// <returns></returns>
        public async Task<List<Usuario>> GetAllUsuariossync()
        {
            //Creamos la lista de personas
            List<Usuario> lst = new List<Usuario>();
            try
            {
                // TODO: return a list of people saved to the Person table in the database7
                lst = await conn.Table<Usuario>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe un usuario por parametro
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>Producto o null</returns>
        public static async Task<Usuario> ComprobarId(Usuario user)
        {
            Usuario usuario;

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.usuarioRepository.GetAllUsuariossync());
            usuario = usuarios.SingleOrDefault(p => p.Id == user.Id);

            return usuario;
        }

        /// <summary>
        /// Comprueba si existe un usuario por parametro
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>Usuario o null</returns>
        public static async Task<Usuario> ComprobarNombre(String nombre)
        {
            Usuario usuario;

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.usuarioRepository.GetAllUsuariossync());
            usuario = usuarios.SingleOrDefault(p => p.Nombre == nombre);

            return usuario;
        }

        /// <summary>
        /// Devuelve la contrasenia de un usuario en concreto
        /// No hace falta comprobar que existe porq ya ha sido comprobado en la linea anterior a llamar a este metodo
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>Usuario o null</returns>
        public static async Task<String> dameContrasenia(Usuario user)
        {
            Usuario usuario;

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.usuarioRepository.GetAllUsuariossync());
            usuario = usuarios.SingleOrDefault(p => p.Contrasenia == user.Contrasenia);

            return usuario.Contrasenia;
        }

        /// <summary>
        /// Comprueba si la contraseña y el nombre de usuario son correctos
        /// </summary>
        /// <param name="user"></param>
        /// <param name="contra"></param>
        /// <returns></returns>
        public static async Task<Boolean> ComprobarLogin (String user, String contra) {
            Boolean valido = false;
            Usuario usuario = await ComprobarNombre(user);

            //Si usuario existe comprobamos que la contraseña es correcta
            if (!usuario.Equals(null))
            {

                if (contra == await dameContrasenia(usuario))
                {
                    valido = true;
                }
            }

            return valido;

        } 
    }
}
