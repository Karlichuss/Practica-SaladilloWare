﻿using Practica_SaladilloWare.Model;
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
        #region Declaracion de variables

        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Realiza el enlace de la base de datos con el modelo y crea la tabla. 
        /// </summary>
        /// <param name="dbPath">La ruta de la base de datos.</param>
        public Chasis_Repository(string dbPath)
        {
            // Inicializamos el SQLiteconnection.
            conn = new SQLiteAsyncConnection(dbPath);
            
            // Creamos la tabla Chasis.
            // Para que la ejecucion no siga y se espere a que este creada la tabla ponemos el wait
            conn.CreateTableAsync<Chasis>().Wait();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Resetea la tabla y sus datos.
        /// </summary>
        public void Reset()
        {
            conn.DropTableAsync<Chasis>().Wait();
            conn.CreateTableAsync<Chasis>().Wait();
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

                // Introducimos la nueva linea de pedido.
                result = await conn.InsertAsync(new Chasis { Nombre = Nombre, Precio = float.Parse(Precio) });

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
        public async Task<List<Chasis>> GetAllChasisAsync()
        {
            List<Chasis> lst = new List<Chasis>();
            try
            {
                lst = await conn.Table<Chasis>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return lst;
        }

        /// <summary>
        /// Obtiene de la tabla todos los nombres de los componentes.
        /// </summary>
        /// <returns>Una colección de todos los nombres de los elementos que se encontraban en la tabla.</returns>
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
        /// Comprueba si existe el producto recibido por parámetro.
        /// </summary>
        /// <param name="producto">Producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<Chasis> ComprobarId(Chasis producto)
        {
            Chasis chasi;

            ObservableCollection<Chasis> chasis = new ObservableCollection<Chasis>(await App.Chasis_Repository.GetAllChasisAsync());
            chasi = chasis.SingleOrDefault(p => p.Id == producto.Id);

            return chasi;
        }

        /// <summary>
        /// Comprueba si existe el id recibido por parámetro.
        /// </summary>
        /// <param name="producto">Id del producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<Chasis> ComprobarId(int producto)
        {
            Chasis chasi;

            ObservableCollection<Chasis> chasis = new ObservableCollection<Chasis>(await App.Chasis_Repository.GetAllChasisAsync());
            chasi = chasis.SingleOrDefault(p => p.Id == producto);

            return chasi;
        }

        /// <summary>
        /// Comprueba si existe el nombre recibido por parámetro.
        /// </summary>
        /// <param name="nombre">Nombre del producto a comprobar.</param>
        /// <returns>El mismo producto, o null si no existe.</returns>
        public static async Task<Chasis> ComprobarNombre(String nombre)
        {
            Chasis chasi;

            ObservableCollection<Chasis> chasis = new ObservableCollection<Chasis>(await App.Chasis_Repository.GetAllChasisAsync());
            chasi = chasis.SingleOrDefault(p => p.Nombre == nombre);

            return chasi;
        }

        #endregion

    }
}
