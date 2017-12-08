using Practica_SaladilloWare.Model;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practica_SaladilloWare.XML_Utils
{
    class XML_Data_Loader
    {
        /// <summary>
        /// Elimina los datos de los componentes, y los sobrescribe con los nuevos precios que figuran en el XML Precios.
        /// </summary>
        /// <returns></returns>
        public static async Task LoadData()
        {
            // Primero cargamos el XML.
            var assembly = typeof(PlacaBase).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Practica_SaladilloWare.Data.Precios.xml");

            StreamReader objReader = new StreamReader(stream);

            var doc = XDocument.Load(stream);

            // Reseteamos los datos de la tabla PlacaBase.
            App.PlacaBase_Repository.Reset();

            // Recorremos la rama Placas del XML.
            foreach (XElement element in doc.Root.Element("Placas").Elements())
            {
                // Extraemos el nombre y el precio.
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                // Y añadimos el nuevo producto.
                await App.PlacaBase_Repository.Add_Item(nombre, precio);
            }

            // Reseteamos los datos de la tabla Procesador.
            App.Procesador_Repository.Reset();

            // Recorremos la rama Procesadores del XML.
            foreach (XElement element in doc.Root.Element("Procesadores").Elements())
            {
                // Extraemos el nombre y el precio.
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                // Y añadimos el nuevo producto.
                await App.Procesador_Repository.Add_Item(nombre, precio);
            }

            // Reseteamos los datos de la tabla Chasis.
            App.Chasis_Repository.Reset();

            // Recorremos la rama Torres del XML.
            foreach (XElement element in doc.Root.Element("Torres").Elements())
            {
                // Extraemos el nombre y el precio.
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                // Y añadimos el nuevo producto.
                await App.Chasis_Repository.Add_Item(nombre, precio);
            }

            // Reseteamos los datos de la tabla RAM.
            App.Ram_Repository.Reset();

            // Recorremos la rama Memorias del XML.
            foreach (XElement element in doc.Root.Element("Memorias").Elements())
            {
                // Extraemos el nombre y el precio.
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                // Y añadimos el nuevo producto.
                await App.Ram_Repository.Add_Item(nombre, precio);
            }

            // Reseteamos los datos de la tabla TarjetaGrafica.
            App.TarjetaGrafica_Repository.Reset();

            // Recorremos la rama Graficas del XML.
            foreach (XElement element in doc.Root.Element("Graficas").Elements())
            {
                // Extraemos el nombre y el precio.
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                // Y añadimos el nuevo producto.
                await App.TarjetaGrafica_Repository.Add_Item(nombre, precio);
            }
        }
    }
}
