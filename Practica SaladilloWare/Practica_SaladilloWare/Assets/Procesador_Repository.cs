﻿using Practica_SaladilloWare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.Assets
{
    public class Procesador_Repository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        public Procesador_Repository(string dbPath)
        {
            // TODO: Initialize a new SQLiteConnection
            conn = new SQLiteAsyncConnection(dbPath);
            // TODO: Create the Person table
            //Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Procesador>().Wait();
        }

        public async Task<List<Procesador>> GetAllProcesadoresAsync()
        {
            //Creamos la lista de personas
            List<Procesador> lst = new List<Procesador>();
            try
            {
                // TODO: return a list of people saved to the Person table in the database7
                lst = await conn.Table<Procesador>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Comprueba si existe la procesador recibida por parametro
        /// </summary>
        /// <param name="producto">Procesador</param>
        /// <returns>Producto o null</returns>
        public static async Task<Procesador> ComprobarId(Procesador producto)
        {
            Procesador procesador;

            ObservableCollection<Procesador> procesaodres = new ObservableCollection<Procesador>(await App.Procesador_Repository.GetAllProcesadoresAsync());
            procesador = procesaodres.SingleOrDefault(p => p.Id == producto.Id);

            return procesador;
        }
    }
}