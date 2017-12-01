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
    public class ChasisRepository
    {

        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public ChasisRepository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // TODO: Create the Person table
            //Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Chasis>().Wait();
        }

        public async Task<List<Chasis>> GetAllChasisAsync()
        {
            //Creamos la lista de personas
            List<Chasis> lst = new List<Chasis>();
            try
            {
                // TODO: return a list of people saved to the Person table in the database7
                lst = await conn.Table<Chasis>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe la chasis recibida por parametro
        /// </summary>
        /// <param name="producto">Chasis</param>
        /// <returns>Producto o null</returns>
        public static async Task<Chasis> ComprobarId(Chasis producto)
        {
            Chasis chasi;

            ObservableCollection<Chasis> chasis = new ObservableCollection<Chasis>(await App.chasisRepository.GetAllChasisAsync());
            chasi = chasis.SingleOrDefault(p => p.Id == producto.Id);

            return chasi;
        }
    }
}
