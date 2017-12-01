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
    public class RAMRepository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public RAMRepository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // TODO: Create the Person table
            //Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<RAM>().Wait();
        }

        public async Task<List<RAM>> GetAllRAMAsync()
        {
            //Creamos la lista de personas
            List<RAM> lst = new List<RAM>();
            try
            {
                // TODO: return a list of people saved to the Person table in the database7
                lst = await conn.Table<RAM>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe la RAM recibida por parametro
        /// </summary>
        /// <param name="producto">RAM</param>
        /// <returns>Producto o null</returns>
        public static async Task<RAM> ComprobarId(RAM producto)
        {
            RAM memoria;

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.ramRepository.GetAllRAMAsync());
            memoria = memorias.SingleOrDefault(p => p.Id == producto.Id);

            return memoria;
        }
    }
}
