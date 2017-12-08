using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.Assets
{
    public class Procesador_Repository
    {
        #region Declaracion de variables

        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Realiza el enlace de la base de datos con el modelo y crea la tabla. 
        /// </summary>
        /// <param name="dbPath">La ruta de la base de datos.</param>
        public Procesador_Repository(string dbPath)
        {
            // Inicializamos el SQLiteconnection.
            conn = new SQLiteAsyncConnection(dbPath);
            // Creamos la tabla Procesador.
            // Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Procesador>().Wait();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Resetea la tabla y sus datos.
        /// </summary>
        public void Reset()
        {
            conn.DropTableAsync<Procesador>().Wait();
            conn.CreateTableAsync<Procesador>().Wait();
        }

        #endregion

        #region Add

        /// <summary>
        /// Añade un nuevo elemento en la tabla.
        /// </summary>
        /// <param name="Nombre">El nombre del elemento a añadir</param>
        /// <param name="Precio">El precio del elemento a añadir</param>
        /// <returns></returns>
        public async Task Add_Item(String Nombre, String Precio)
        {
            int result = 0;
            try
            {
                //Comprobamos que el nombre y el precio sean validos.
                if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Precio))
                    throw new Exception("Valid values required");

                // Introducimos la nueva linea de pedido.
                result = await conn.InsertAsync(new Procesador { Nombre = Nombre, Precio = float.Parse(Precio) });

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", Nombre, ex.Message);
            }
        }

        #endregion

        #region Select

        /// <summary>
        /// Obtiene de la tabla todos los nombres de los componentes.
        /// </summary>
        /// <returns>Una coleccion de todos los nombres de los elementos que se encontraban en la tabla.</returns>
        public static async Task<List<String>> GetNombres()
        {
            List<Procesador> Procesadores;
            List<String> Nombres = new List<String>();

            ObservableCollection<Procesador> procesadores = new ObservableCollection<Procesador>(await App.Procesador_Repository.GetAllProcesadoresAsync());
            Procesadores = procesadores.ToList();

            foreach (Procesador p in Procesadores)
            {
                Nombres.Add(p.Nombre);
            }

            return Nombres.ToList();
        }

        /// <summary>
        /// Obtiene de la tabla todos los componentes.
        /// </summary>
        /// <returns>Una coleccion de todos los elementos que se encontraban en la tabla.</returns>
        public async Task<List<Procesador>> GetAllProcesadoresAsync()
        {
            List<Procesador> lst = new List<Procesador>();
            try
            {
                lst = await conn.Table<Procesador>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe el producto recibido por parámetro.
        /// </summary>
        /// <param name="producto">El producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<Procesador> ComprobarId(Procesador producto)
        {
            Procesador procesador;

            ObservableCollection<Procesador> procesaodres = new ObservableCollection<Procesador>(await App.Procesador_Repository.GetAllProcesadoresAsync());
            procesador = procesaodres.SingleOrDefault(p => p.Id == producto.Id);

            return procesador;
        }

        /// <summary>
        /// Comprueba si existe el producto recibido por parámetro.
        /// </summary>
        /// <param name="producto">El producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<Procesador> ComprobarId(int producto)
        {
            Procesador procesador;

            ObservableCollection<Procesador> procesaodres = new ObservableCollection<Procesador>(await App.Procesador_Repository.GetAllProcesadoresAsync());
            procesador = procesaodres.SingleOrDefault(p => p.Id == producto);

            return procesador;
        }

        /// <summary>
        /// Comprueba si existe el nombre recibido por parámetro.
        /// </summary>
        /// <param name="nombre">Nombre del producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<Procesador> ComprobarNombre(String nombre)
        {
            Procesador procesador;

            ObservableCollection<Procesador> procesadores = new ObservableCollection<Procesador>(await App.Procesador_Repository.GetAllProcesadoresAsync());
            procesador = procesadores.SingleOrDefault(p => p.Nombre == nombre);

            return procesador;
        }

        #endregion
    }
}
