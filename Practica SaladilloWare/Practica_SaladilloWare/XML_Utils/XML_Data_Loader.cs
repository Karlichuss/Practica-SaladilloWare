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
        public static async Task LoadData()
        {

            var assembly = typeof(PlacaBase).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Practica_SaladilloWare.Data.Precios.xml");

            StreamReader objReader = new StreamReader(stream);

            var doc = XDocument.Load(stream);

            App.PlacaBase_Repository.Reset();

            foreach (XElement element in doc.Root.Element("Placas").Elements())
            {
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                await App.PlacaBase_Repository.Add_Item(nombre, precio);
            }

            App.Procesador_Repository.Reset();

            foreach (XElement element in doc.Root.Element("Procesadores").Elements())
            {
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                await App.Procesador_Repository.Add_Item(nombre, precio);
            }

            App.Chasis_Repository.Reset();

            foreach (XElement element in doc.Root.Element("Torres").Elements())
            {
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                await App.Chasis_Repository.Add_Item(nombre, precio);
            }

            App.Ram_Repository.Reset();

            foreach (XElement element in doc.Root.Element("Memorias").Elements())
            {
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                await App.Ram_Repository.Add_Item(nombre, precio);
            }

            App.TarjetaGrafica_Repository.Reset();

            foreach (XElement element in doc.Root.Element("Graficas").Elements())
            {
                String nombre = element.Element("NOMBRE").Value;
                String precio = element.Element("PRECIO").Value;

                await App.TarjetaGrafica_Repository.Add_Item(nombre, precio);
            }
        }
    }
}
