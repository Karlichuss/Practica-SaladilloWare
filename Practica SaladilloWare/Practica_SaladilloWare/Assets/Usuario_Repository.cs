using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.Assets
{
    public class Usuario_Repository
    {
        public string StatusMessage { get; set; }
        public const string TIPO_CLIENTE = "C";

        private SQLiteAsyncConnection conn;

        /// <summary>
        /// Crea la tabla y la conexion 
        /// </summary>
        /// <param name="dbPath">Ruta di¡onde se aloja la bdd</param>
        public Usuario_Repository(string dbPath)
        {
            // Inicializamos una nueva instancia de SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // Creamos la tabla Usuario
            // Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Usuario>().Wait();
        }

        /// <summary>
        /// Recorre la tabla Usuario y nos permite acceder a estos datos en forma de Coleccion
        /// </summary>
        /// <returns>Una Lista de Objetos de tipo Usuario</returns>
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            //Creamos la lista de personas
            List<Usuario> lst = new List<Usuario>();
            try
            {
                lst = await conn.Table<Usuario>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR: No pueden leerse los datos. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe un usuario, pasado por parametro.
        /// </summary>
        /// <param name="usuario">Usuario a buscar</param>
        /// <returns>El propio usuario si se encuentra en la base de datos, o null si no se encuentra.</returns>
        public static async Task<Usuario> ComprobarId(Usuario user)
        {
            Usuario usuario;

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.Usuario_Repository.GetAllUsuarios());
            usuario = usuarios.SingleOrDefault(p => p.Id == user.Id);

            return usuario;
        }

        /// <summary>
        /// Comprueba si existe un usuario que tenga el nombre pasado por parametro.
        /// </summary>
        /// <param name="nombre">El nombre a buscar.</param>
        /// <returns>El propio usuario si se encuentra en la base de datos, o null si no se encuentra.</returns>
        public static async Task<Usuario> ComprobarNombre(String nombre)
        {
            Usuario usuario = new Usuario();
            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.Usuario_Repository.GetAllUsuarios());
            usuario = usuarios.SingleOrDefault(p => p.Nombre == nombre);
            return usuario;
        }

        /// <summary>
        /// Devuelve la contraseña de un usuario pasado por parametro
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>Usuario o null</returns>
        public static async Task<String> GetContrasenia(Usuario user)
        {
            Usuario usuario;

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.Usuario_Repository.GetAllUsuarios());
            usuario = usuarios.SingleOrDefault(p => p.Contrasenia == user.Contrasenia);
            return usuario.Contrasenia;
        }

        /// <summary>
        /// Comprueba si la contraseña y el nombre de usuario son correctos.
        /// </summary>
        /// <param name="user">El nombre de usuario a comprobar. Este debe existir en la base de datos.</param>
        /// <param name="contrasenia">La contraseña introducida con la que vamos a comparar.</param>
        /// <returns>True si la contraseña es correcta. False si no es correcta.</returns>
        public static async Task<Boolean> ComprobarLogin(String user, String contrasenia)
        {
            Boolean valido = false;
            Usuario usuario = await ComprobarNombre(user);

            //Si usuario existe comprobamos que la contraseña es correcta
            if (!usuario.Equals(null))
            {

                if (contrasenia == await GetContrasenia(usuario))
                {
                    valido = true;
                }
            }

            return valido;
        }

        public static async Task<Boolean> EsCliente(String user)
        {
            Boolean valido = false;
            Usuario usuario = await ComprobarNombre(user);

            // Comprobamos si existe el usuario, si es un cliente o un vendedor
            if (!usuario.Nombre.Equals(null) || !usuario.Nombre.Equals(""))
            {
                if (usuario.Tipo == TIPO_CLIENTE)
                {
                    valido = true;
                }
            }
            return valido;
        }
    }
}
