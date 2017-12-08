using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace Practica_SaladilloWare.Assets
{
    public class PlacaBase_Repository
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
        public PlacaBase_Repository(string dbPath)
        {
            // Inicializamos el SQLiteconnection.
            conn = new SQLiteAsyncConnection(dbPath);
            // Creamos la tabla PlacaBase.
            // Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<PlacaBase>().Wait();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Resetea la tabla y sus datos.
        /// </summary>
        public void Reset()
        {
            conn.DropTableAsync<PlacaBase>().Wait();
            conn.CreateTableAsync<PlacaBase>().Wait();
        }

        #endregion

        #region Add

        /// <summary>
        /// Añade un nuevo elemento en la tabla.
        /// </summary>
        /// <param name="Nombre">El nombre del elemento a añadir</param>
        /// <param name="Precio">El precio del elemento a añador</param>
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
                result = await conn.InsertAsync(new PlacaBase { Nombre = Nombre, Precio = float.Parse(Precio) });

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
        /// <returns>Una coleccion de todos los elementos que se encontraban en la tabla.</returns>
        public async Task<List<PlacaBase>> GetAllPlacasBaseAsync()
        {
            List<PlacaBase> lst = new List<PlacaBase>();
            try
            {
                lst = await conn.Table<PlacaBase>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe el id recibido por parametro.
        /// </summary>
        /// <param name="producto">Id del chasis a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<PlacaBase> ComprobarId(PlacaBase producto)
        {
            PlacaBase placa;

            ObservableCollection<PlacaBase> placas = new ObservableCollection<PlacaBase>(await App.PlacaBase_Repository.GetAllPlacasBaseAsync());
            placa = placas.SingleOrDefault(p => p.Id == producto.Id);

            return placa;
        }

        /// <summary>
        /// Comprueba si existe el chasis recibido por parametro.
        /// </summary>
        /// <param name="producto">Chasis a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<PlacaBase> ComprobarId(int producto)
        {
            PlacaBase placa;

            ObservableCollection<PlacaBase> placas = new ObservableCollection<PlacaBase>(await App.PlacaBase_Repository.GetAllPlacasBaseAsync());
            placa = placas.SingleOrDefault(p => p.Id == producto);

            return placa;
        }

        /// <summary>
        /// Obtiene de la tabla todos los nombres de los componentes.
        /// </summary>
        /// <returns>Una coleccion de todos los nombres de los elementos que se encontraban en la tabla.</returns>
        public static async Task<List<String>> GetNombres()
        {
            List<PlacaBase> PlacasBase;
            List<String> Nombres = new List<String>();

            ObservableCollection<PlacaBase> placas = new ObservableCollection<PlacaBase>(await App.PlacaBase_Repository.GetAllPlacasBaseAsync());
            PlacasBase = placas.ToList();

            foreach (PlacaBase p in PlacasBase)
            {
                Nombres.Add(p.Nombre);
            }

            return Nombres.ToList();
        }

        /// <summary>
        /// Comprueba si existe el nombre recibido por parametro.
        /// </summary>
        /// <param name="producto">Nombre del chasis a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<PlacaBase> ComprobarNombre(String nombre)
        {
            PlacaBase placa;

            ObservableCollection<PlacaBase> placas = new ObservableCollection<PlacaBase>(await App.PlacaBase_Repository.GetAllPlacasBaseAsync());
            placa = placas.SingleOrDefault(p => p.Nombre == nombre);

            return placa;
        }

        #endregion
    }
}
