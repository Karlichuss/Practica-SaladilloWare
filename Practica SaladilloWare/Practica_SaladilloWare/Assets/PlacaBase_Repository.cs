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
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public PlacaBase_Repository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // TODO: Create the Person table
            //Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<PlacaBase>().Wait();
        }

        public async Task<List<PlacaBase>> GetAllPlacasBaseAsync()
        {
            //Creamos la lista de personas
            List<PlacaBase> lst = new List<PlacaBase>();
            try
            {
                // TODO: return a list of people saved to the Person table in the database7
                lst = await conn.Table<PlacaBase>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe la placa base recibida por parametro
        /// </summary>
        /// <param name="producto">Placa base</param>
        /// <returns>Producto o null</returns>
        public static async Task<PlacaBase> ComprobarId(PlacaBase producto)
        {
            PlacaBase placa;

            ObservableCollection<PlacaBase> placas = new ObservableCollection<PlacaBase>(await App.PlacaBase_Repository.GetAllPlacasBaseAsync());
            placa = placas.SingleOrDefault(p => p.Id == producto.Id);

            return placa;
        }

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

        public static async Task<PlacaBase> ComprobarNombre(String nombre)
        {
            PlacaBase placa;

            ObservableCollection<PlacaBase> placas = new ObservableCollection<PlacaBase>(await App.PlacaBase_Repository.GetAllPlacasBaseAsync());
            placa = placas.SingleOrDefault(p => p.Nombre == nombre);

            return placa;
        }
    }
}
