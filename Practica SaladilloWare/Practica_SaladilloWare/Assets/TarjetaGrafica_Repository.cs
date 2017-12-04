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
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public TarjetaGrafica_Repository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // TODO: Create the Person table
            //Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<TarjetaGrafica>().Wait();
        }

        public async Task<List<TarjetaGrafica>> GetAllTarjetassync()
        {
            //Creamos la lista de personas
            List<TarjetaGrafica> lst = new List<TarjetaGrafica>();
            try
            {
                // TODO: return a list of people saved to the Person table in the database7
                lst = await conn.Table<TarjetaGrafica>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe la grafica recibida por parametro
        /// </summary>
        /// <param name="producto">Tarjeta Grafica</param>
        /// <returns>Producto o null</returns>
        public static async Task<TarjetaGrafica> ComprobarId(TarjetaGrafica producto)
        {
            TarjetaGrafica tarjeta;

            ObservableCollection<TarjetaGrafica> tarjetas = new ObservableCollection<TarjetaGrafica>(await App.TarjetaGrafica_Repository.GetAllTarjetassync());
            tarjeta = tarjetas.SingleOrDefault(p => p.Id == producto.Id);

            return tarjeta;
        }

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

        public static async Task<TarjetaGrafica> ComprobarNombre(String nombre)
        {
            TarjetaGrafica tarjeta;

            ObservableCollection<TarjetaGrafica> tarjetas = new ObservableCollection<TarjetaGrafica>(await App.TarjetaGrafica_Repository.GetAllTarjetassync());
            tarjeta = tarjetas.SingleOrDefault(p => p.Nombre == nombre);

            return tarjeta;
        }
    }
}
