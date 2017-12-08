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
        #region Declaracion de variables

        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public const string TIPO_CLIENTE = "C";

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Realiza el enlace de la base de datos con el modelo y crea la tabla. 
        /// </summary>
        /// <param name="dbPath">La ruta de la base de datos.</param>
        public Usuario_Repository(string dbPath)
        {
            // Inicializamos el SQLiteconnection.
            conn = new SQLiteAsyncConnection(dbPath);
            // Creamos la tabla Usuario.
            // Para que la ejecucion no siga y se espere a que esté creada la tabla ponemos el wait
            conn.CreateTableAsync<Usuario>().Wait();
        }

        #endregion

        #region Delete

        #endregion

        #region Select

        /// <summary>
        /// Obtiene de la tabla todos los componentes.
        /// </summary>
        /// <returns>Una colección de todos los elementos que se encuentren en la tabla.</returns>
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            List<Usuario> lst = new List<Usuario>();
            try
            {
                lst = await conn.Table<Usuario>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe el id recibido por parámetro.
        /// </summary>
        /// <param name="user">Id del elemento a comprobar.</param>
        /// <returns>El mismo usuario, o null si no existe.</returns>
        public static async Task<Usuario> ComprobarId(Usuario user)
        {
            Usuario usuario;

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.Usuario_Repository.GetAllUsuarios());
            usuario = usuarios.SingleOrDefault(p => p.Id == user.Id);

            return usuario;
        }

        /// <summary>
        /// Comprueba si existe el elemento recibido por parametro.
        /// </summary>
        /// <param name="user">Usuario a comprobar.</param>
        /// <returns>El mismo usuario, o null si no existe.</returns>
        public static async Task<Usuario> ComprobarId(int user)
        {
            Usuario usuario;

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.Usuario_Repository.GetAllUsuarios());
            usuario = usuarios.SingleOrDefault(p => p.Id == user);

            return usuario;
        }

        /// <summary>
        /// Comprueba si existe un usuario que tenga el nombre pasado por parámetro.
        /// </summary>
        /// <param name="nombre">El nombre a buscar.</param>
        /// <returns>El propio usuario si se encuentra en la base de datos, o null si no se encuentra.</returns>
        public static async Task<Boolean> ComprobarNombre(String nombre)
        {
            List<Usuario> usuario = new List<Usuario>();
            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.Usuario_Repository.GetAllUsuarios());
            usuario = usuarios.Where(p => p.Nombre == nombre).ToList();

            return usuario.Count > 0;
        }

        /// <summary>
        /// Comprueba si existe el nombre recibido por parámetro.
        /// </summary>
        /// <param name="nombre">Nombre del usuario a comprobar.</param>
        /// <returns>El mismo usuario, o null si no existe.</returns>
        public static async Task<Usuario> GetUsuario(String nombre)
        {
            List<Usuario> listUsuarios = new List<Usuario>();
            Usuario usuario;
            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>(await App.Usuario_Repository.GetAllUsuarios());
            listUsuarios = usuarios.Where(p => p.Nombre == nombre).ToList();

            if (listUsuarios.Count > 0)
            {
                usuario = listUsuarios[0];
            }
            else
            {
                usuario = null;
            }

            return usuario;
        }

        /// <summary>
        /// Devuelve la contraseña de un usuario pasado por parámetro
        /// </summary>
        /// <param name="user">Usuario</param>
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

        /// <summary>
        /// Comprueba si el tipo de usuario pasado por parametro es Cliente o no.
        /// </summary>
        /// <param name="user">El nombre del usuario a comprobar.</param>
        /// <returns>True si es Cliente, False si es Vendor.</returns>
        public static async Task<Boolean> EsCliente(String user)
        {
            Boolean valido = false;
            Usuario usuario = await ComprobarNombre(user);

            // Comprobamos si existe el usuario, si es un cliente o un vendedor.
            if (!usuario.Nombre.Equals(null) || !usuario.Nombre.Equals(""))
            {
                if (usuario.Tipo == TIPO_CLIENTE)
                {
                    valido = true;
                }
            }
            return valido;
        }

        #endregion

    }
}
