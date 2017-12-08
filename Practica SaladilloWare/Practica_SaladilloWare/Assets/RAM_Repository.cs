using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.Assets
{
    public class RAM_Repository
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
        public RAM_Repository(string dbPath)
        {
            // Inicializamos el SQLiteconnection.
            conn = new SQLiteAsyncConnection(dbPath);
            // Creamos la tabla RAM.
            // Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<RAM>().Wait();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Resetea la tabla y sus datos.
        /// </summary>
        public void Reset()
        {
            conn.DropTableAsync<RAM>().Wait();
            conn.CreateTableAsync<RAM>().Wait();
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

                // Introducimos el nuevo producto.
                result = await conn.InsertAsync(new RAM { Nombre = Nombre, Precio = float.Parse(Precio) });

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", Nombre, ex.Message);
            }
        }

        #endregion

        #region Select

        /// <summary>
        /// Obtiene de la tabla todos los componentes.
        /// </summary>
        /// <returns>Una colección de todos los elementos que se encontraban en la tabla.</returns>
        public async Task<List<RAM>> GetAllRAMAsync()
        {
            List<RAM> lst = new List<RAM>();
            try
            {
                lst = await conn.Table<RAM>().ToListAsync();
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
        /// <param name="producto">Id del producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<RAM> ComprobarId(RAM producto)
        {
            RAM memoria;

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.Ram_Repository.GetAllRAMAsync());
            memoria = memorias.SingleOrDefault(p => p.Id == producto.Id);

            return memoria;
        }

        /// <summary>
        /// Comprueba si existe el producto recibido por parámetro.
        /// </summary>
        /// <param name="producto">Producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<RAM> ComprobarId(int producto)
        {
            RAM memoria;

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.Ram_Repository.GetAllRAMAsync());
            memoria = memorias.SingleOrDefault(p => p.Id == producto);

            return memoria;
        }

        /// <summary>
        /// Obtiene de la tabla todos los nombres de los componentes.
        /// </summary>
        /// <returns>Una colección de todos los nombres de los elementos que se encontraban en la tabla.</returns>
        public static async Task<List<String>> GetNombres()
        {
            List<RAM> Memorias;
            List<String> Nombres = new List<String>();

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.Ram_Repository.GetAllRAMAsync());
            Memorias = memorias.ToList();

            foreach (RAM p in Memorias)
            {
                Nombres.Add(p.Nombre);
            }

            return Nombres.ToList();
        }

        /// <summary>
        /// Comprueba si existe el nombre recibido por parámetro.
        /// </summary>
        /// <param name="producto">Nombre del producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<RAM> ComprobarNombre(String nombre)
        {
            RAM memoria;

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.Ram_Repository.GetAllRAMAsync());
            memoria = memorias.SingleOrDefault(p => p.Nombre == nombre);

            return memoria;
        }

        #endregion
    }
}
