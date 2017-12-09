using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.Assets
{
    public class Pedido_Repository
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
        public Pedido_Repository(string dbPath)
        {
            // Inicializamos el SQLiteconnection.
            conn = new SQLiteAsyncConnection(dbPath);
            // Creamos la tabla Pedido.
            // Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Pedido>().Wait();
        }

        #endregion

        #region Delete

        #endregion

        #region Add

        /// <summary>
        /// Añade un nuevo pedido a la tabla.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="placa"></param>
        /// <param name="procesador"></param>
        /// <param name="tarjeta"></param>
        /// <param name="RAM"></param>
        /// <param name="chasis"></param>
        /// <returns></returns>
        public async Task AddNewPedidoAsync(Usuario user, PlacaBase placa, Procesador procesador, TarjetaGrafica tarjeta, RAM RAM, Chasis chasis)
        {
            int result = 0;
            try
            {
                // Validamos que exista el Usuario.
                if (Usuario_Repository.ComprobarId(user).Equals(null))
                    throw new Exception("Valid user required");
                // Validamos que exista la Placa Base.
                if (PlacaBase_Repository.ComprobarId(placa).Equals(null))
                    throw new Exception("Valid placa required");
                // Validamos que exista el Procesador.
                if (Procesador_Repository.ComprobarId(procesador).Equals(null))
                    throw new Exception("Valid procesador required");
                // Validamos que exista la Tarjeta Grafica.
                if (TarjetaGrafica_Repository.ComprobarId(tarjeta).Equals(null))
                    throw new Exception("Valid tarjeta grafica required");
                // Validamos que exista la Memoria RAM.
                if (RAM_Repository.ComprobarId(RAM).Equals(null))
                    throw new Exception("Valid RAM required");
                // Validamos que exista el Chasis.
                if (Chasis_Repository.ComprobarId(chasis).Equals(null))
                    throw new Exception("Valid chasis required");

                // Introducimos la nueva linea de pedido.
                result = await conn.InsertAsync(new Pedido { Usuario = user.Id, PlacaBase = placa.Id, Procesador = procesador.Id, TarjetaGrafica = tarjeta.Id, RAM = RAM.Id, Chasis = chasis.Id });

                StatusMessage = string.Format("{0} record(s) added [User: {1} [PlacaBase: {2} [Procesador: {3} [TarjetaGrafica: {4} [RAM: {5} [Chasis: {6})", result, user, placa, procesador, tarjeta, RAM, chasis);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", user, ex.Message);
            }
        }

        #endregion

        #region Select

        /// <summary>
        /// Devuelve todos los pedidos.
        /// </summary>
        /// <returns>Una colección de Pedidos.</returns>
        public async Task<List<Pedido>> GetAllPedidosAsync()
        {
            List<Pedido> lst = new List<Pedido>();
            try
            {
                lst = await conn.Table<Pedido>().ToListAsync();
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
        /// <param name="pedido">Id del pedido a comprobar.</param>
        /// <returns>El mismo pedido, o null si no existe.</returns>
        public static async Task<Pedido> ComprobarId(Pedido pedido)
        {
            Pedido pe;

            ObservableCollection<Pedido> pedidos = new ObservableCollection<Pedido>(await App.Pedido_Repository.GetAllPedidosAsync());
            pe = pedidos.SingleOrDefault(p => p.Id == pedido.Id);

            return pe;
        }

        #endregion

    }
}
