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
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public RAM_Repository(string dbPath)
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

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.Ram_Repository.GetAllRAMAsync());
            memoria = memorias.SingleOrDefault(p => p.Id == producto.Id);

            return memoria;
        }

        public static async Task<RAM> ComprobarId(int producto)
        {
            RAM memoria;

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.Ram_Repository.GetAllRAMAsync());
            memoria = memorias.SingleOrDefault(p => p.Id == producto);

            return memoria;
        }

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

        public static async Task<RAM> ComprobarNombre(String nombre)
        {
            RAM memoria;

            ObservableCollection<RAM> memorias = new ObservableCollection<RAM>(await App.Ram_Repository.GetAllRAMAsync());
            memoria = memorias.SingleOrDefault(p => p.Nombre == nombre);

            return memoria;
        }
    }
}
