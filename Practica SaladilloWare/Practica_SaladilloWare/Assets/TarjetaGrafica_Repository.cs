using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.Assets
{
    public class TarjetaGrafica_Repository
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
        public TarjetaGrafica_Repository(string dbPath)
        {
            // Inicializamos el SQLiteconnection.
            conn = new SQLiteAsyncConnection(dbPath);
            // Creamos la tabla TarjetaGrafica.
            // Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<TarjetaGrafica>().Wait();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Resetea la tabla y sus datos.
        /// </summary>
        public void Reset()
        {
            conn.DropTableAsync<TarjetaGrafica>().Wait();
            conn.CreateTableAsync<TarjetaGrafica>().Wait();
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
                result = await conn.InsertAsync(new TarjetaGrafica { Nombre = Nombre, Precio = float.Parse(Precio) });

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
        public async Task<List<TarjetaGrafica>> GetAllTarjetassync()
        {
            List<TarjetaGrafica> lst = new List<TarjetaGrafica>();
            try
            {
                lst = await conn.Table<TarjetaGrafica>().ToListAsync();
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
        public static async Task<TarjetaGrafica> ComprobarId(TarjetaGrafica producto)
        {
            TarjetaGrafica tarjeta;

            ObservableCollection<TarjetaGrafica> tarjetas = new ObservableCollection<TarjetaGrafica>(await App.TarjetaGrafica_Repository.GetAllTarjetassync());
            tarjeta = tarjetas.SingleOrDefault(p => p.Id == producto.Id);

            return tarjeta;
        }

        /// <summary>
        /// Comprueba si existe el producto recibido por parámetro.
        /// </summary>
        /// <param name="producto">Chasis a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<TarjetaGrafica> ComprobarId(int producto)
        {
            TarjetaGrafica tarjeta;

            ObservableCollection<TarjetaGrafica> tarjetas = new ObservableCollection<TarjetaGrafica>(await App.TarjetaGrafica_Repository.GetAllTarjetassync());
            tarjeta = tarjetas.SingleOrDefault(p => p.Id == producto);

            return tarjeta;
        }

        /// <summary>
        /// Obtiene de la tabla todos los nombres.
        /// </summary>
        /// <returns>Una colección de todos los nombres de los elementos que se encontraban en la tabla.</returns>
        public static async Task<List<String>> GetNombres()
        {
            List<TarjetaGrafica> TarjetasGraficas;
            List<String> Nombres = new List<String>();

            ObservableCollection<TarjetaGrafica> tarjetasGraficas = new ObservableCollection<TarjetaGrafica>(await App.TarjetaGrafica_Repository.GetAllTarjetassync());
            TarjetasGraficas = tarjetasGraficas.ToList();

            foreach (TarjetaGrafica p in TarjetasGraficas)
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
        public static async Task<TarjetaGrafica> ComprobarNombre(String nombre)
        {
            TarjetaGrafica tarjeta;

            ObservableCollection<TarjetaGrafica> tarjetas = new ObservableCollection<TarjetaGrafica>(await App.TarjetaGrafica_Repository.GetAllTarjetassync());
            tarjeta = tarjetas.SingleOrDefault(p => p.Nombre == nombre);

            return tarjeta;
        }

        #endregion

    }
}
