using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.Assets
{
    public class Chasis_Repository
    {

        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public Chasis_Repository(string dbPath)
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

        public static async Task<List<String>> GetNombres()
        {
            List<Chasis> Chasis;
            List<String> Nombres = new List<String>();

            ObservableCollection<Chasis> chasis = new ObservableCollection<Chasis>(await App.Chasis_Repository.GetAllChasisAsync());
            Chasis = chasis.ToList();

            foreach (Chasis c in Chasis)
            {
                Nombres.Add(c.Nombre);
            }

            return Nombres.ToList();
        }

        /// <summary>
        /// Comprueba si existe el chasis recibida por parametro
        /// </summary>
        /// <param name="producto">Chasis</param>
        /// <returns>Producto o null</returns>
        public static async Task<Chasis> ComprobarId(Chasis producto)
        {
            Chasis chasi;

            ObservableCollection<Chasis> chasis = new ObservableCollection<Chasis>(await App.Chasis_Repository.GetAllChasisAsync());
            chasi = chasis.SingleOrDefault(p => p.Id == producto.Id);

            return chasi;
        }

        public static async Task<Chasis> ComprobarNombre(String nombre)
        {
            Chasis chasi;

            ObservableCollection<Chasis> chasis = new ObservableCollection<Chasis>(await App.Chasis_Repository.GetAllChasisAsync());
            chasi = chasis.SingleOrDefault(p => p.Nombre == nombre);

            return chasi;
        }

    }
}
