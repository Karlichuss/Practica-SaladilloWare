using APP12_TiendaInformatica.Model;
using APP12_TiendaInformatica.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP12_TiendaInformatica.Asset
{
    public class PedidoRepository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        /// <summary>
        /// Crea la tabla pedido y la conexion
        /// </summary>
        /// <param name="dbPath">ruta de donde se aloja la bdd</param>
        public PedidoRepository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // TODO: Create the Person table
            //Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Pedido>().Wait();
        }

        /// <summary>
        /// Se añade un nuevo pedido:
        /// 
        /// comprobamos que todos los productos pasados existen
        /// si existen se hace ek Insert son Async
        /// Se muestra un mensaje por consola en caso de exito y fallo
        /// 
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
                //basic validation to user
                if (UsuarioRepository.ComprobarId(user).Equals(null))
                    throw new Exception("Valid user required");
                //basic validation to placaBase
                if (PlacaBaseRepository.ComprobarId(placa).Equals(null))
                    throw new Exception("Valid placa required");
                //basic validation to Procesador
                if (ProcesadorRepository.ComprobarId(procesador).Equals(null))
                    throw new Exception("Valid procesador required");
                //basic validation to tarjeta
                if (TarjetaGraficaRepository.ComprobarId(tarjeta).Equals(null))
                    throw new Exception("Valid tarjeta grafica required");
                //basic validation to RAM
                if (RAMRepository.ComprobarId(RAM).Equals(null))
                    throw new Exception("Valid RAM required");
                //basic validation to chasis
                if (ChasisRepository.ComprobarId(chasis).Equals(null))
                    throw new Exception("Valid chasis required");

                // TODO: insert a new person into the Person table
                result = await conn.InsertAsync(new Pedido { Usuario = user.Id, PlacaBase = placa.Id, Procesador = procesador.Id, TarjetaGrafica = tarjeta.Id, RAM = RAM.Id, Chasis = chasis.Id});

                StatusMessage = string.Format("{0} record(s) added [User: {1} [PlacaBase: {2} [Procesador: {3} [TarjetaGrafica: {4} [RAM: {5} [Chasis: {6})", result, user, placa, procesador, tarjeta, RAM, chasis);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", user, ex.Message);
            }
        }

        /// <summary>
        /// Devuelve todos los pedidos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Pedido>> GetAllPedidosAsync()
        {
            //Creamos la lista de personas
            List<Pedido> lst = new List<Pedido>();
            try
            {
                // TODO: return a list of people saved to the Person table in the database7
                lst = await conn.Table<Pedido>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Devuelve el calculo de de la suma del precio de todos los productos de una tabla
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<float> totalPedidoAsync(Pedido pedido)
        {
            float suma = 0.0f;
            try
            {
                //basic validation to pedido
                if (ComprobarId(pedido).Equals(null))
                    throw new Exception("Valid user required");

                // TODO: insert a new person into the Person table
                suma = await conn.ExecuteAsync("select sum(pb.precio, cpu.precio, gpu.precio, ram.precio, chasis.precio)" +
                    "from Pedido as p " +
                    "left join PlacaBase as pb on p.placabase = pb.id " +
                    "left join Procesador as cpu on p.procesador = cpu.id " +
                    "left join TarjetaGrafica as gpu on p.tarjetagrafica = gpu.id " +
                    "left join RAM as ram on p.memoria = ram.id " +
                    "left join Chasis as chasis on p.cajaPC = chasis.id " +
                    "where p.numPedido = "+pedido.Id+ " and p.usuario = " + pedido.Id + ";");

                StatusMessage = string.Format("{0} record(s) sum Precio Total : {0}", suma);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to sum {0}. Error: {1}", pedido, ex.Message);
            }

            return suma;
        }

        /// <summary>
        /// Comprueba si existe un usuario por parametro
        /// </summary>
        /// <param name="pedido">Pedido</param>
        /// <returns>Producto o null</returns>
        public static async Task<Pedido> ComprobarId(Pedido pedido)
        {
            Pedido pe;

            ObservableCollection<Pedido> pedidos = new ObservableCollection<Pedido>(await App.pedidoRepository.GetAllPedidosAsync());
            pe = pedidos.SingleOrDefault(p => p.Id == pedido.Id);

            return pe;
        }
    }
}
