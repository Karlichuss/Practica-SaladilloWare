using Practica_SaladilloWare.Assets;
using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.XML_Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Practica_SaladilloWare.View_Model
{
    public class Vendor_View_Model
    {

        #region Declaracion de variables

        public Usuario Usuario; // El usuario que ha iniciado sesión.
        INavigation Navigation; // Necesario para poder navegar entre las distintas vistas.
        Page Page; // Necesario para poder realizar diálogos.

        // Elementos del layout
        ListView lstResumen;

        // Coleccion de pedidos para la lista.
        List<Pedido> pedidos;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Necesita muchos datos de la vista asociada, ya que esta es la parte lógica.
        /// </summary>
        /// <param name="page">El code behind de la vista asociada.</param>
        /// <param name="navigation">Necesario para poder navegar entre vistas.</param>
        /// <param name="usuario">El usuario que ha iniciado la sesión.</param>
        /// <param name="lstResumen"></param>
        public Vendor_View_Model(Page page, INavigation navigation, Usuario usuario, ListView lstResumen)
        {
            Page = page;
            Navigation = navigation;
            Usuario = usuario;
            this.lstResumen = lstResumen;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Actualiza la base de datos con el nuevo catálogo que tenemos en un XML.
        /// </summary>
        /// <returns></returns>
        public async Task ActualizarPreciosAsync()
        {
            // Actualizamos la base de datos con los datos del XML.
            await XML_Data_Loader.LoadData();

            // Notificamos al usuario que la base de datos se ha actualizado.
            await Page.DisplayAlert("Datos actualizados con éxito.", "Los nuevos precios estan ya disponibles.", "OK");

            // Refrescamos la lista de los pedidos aplicando los nuevos precios.
            lstResumen.ItemsSource = await CargarPedidos();

        }

        /// <summary>
        /// Recupera de cada pedido realizado el nombre del usuario que lo ha realizado, el nombre de cada componente y el precio total.
        /// </summary>
        /// <returns>Una lista de los pedidos realizados.</returns>
        public async Task<List<LineaPedido>> CargarPedidos()
        {
            pedidos = await App.Pedido_Repository.GetAllPedidosAsync();

            List<LineaPedido> lineasPedido = new List<LineaPedido>();
            PlacaBase placabase;
            Procesador procesador;
            Chasis chasis;
            RAM ram;
            TarjetaGrafica tarjetaGrafica;
            Usuario usuarioPedido;

            for (int i = 0; i < pedidos.Count; i++)
            {
                placabase = await PlacaBase_Repository.ComprobarId(pedidos[i].PlacaBase);
                procesador = await Procesador_Repository.ComprobarId(pedidos[i].Procesador);
                chasis = await Chasis_Repository.ComprobarId(pedidos[i].Chasis);
                ram = await RAM_Repository.ComprobarId(pedidos[i].RAM);
                tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarId(pedidos[i].TarjetaGrafica);
                usuarioPedido = await Usuario_Repository.ComprobarId(pedidos[i].Usuario);

                lineasPedido.Add(new LineaPedido(
                    pedidos[i].Id,
                    usuarioPedido.Nombre,
                    placabase.Nombre,
                    procesador.Nombre,
                    tarjetaGrafica.Nombre,
                    chasis.Nombre,
                    ram.Nombre,
                    placabase.Precio + procesador.Precio + chasis.Precio + ram.Precio + tarjetaGrafica.Precio
                    ));
            }

            return lineasPedido;
        }

        /// <summary>
        /// Calcula el precio total de un pedido que pasamos por parámetro.
        /// </summary>
        /// <param name="pedido">El pedido al que le queremos sacar el total.</param>
        /// <returns>El precio total de todos los componentes del pedido.</returns>
        private async Task<float> CalcularTotalAsync(Pedido pedido)
        {
            PlacaBase placabase = await PlacaBase_Repository.ComprobarId(pedido.PlacaBase);
            Procesador procesador = await Procesador_Repository.ComprobarId(pedido.Procesador);
            Chasis chasis = await Chasis_Repository.ComprobarId(pedido.Chasis);
            RAM ram = await RAM_Repository.ComprobarId(pedido.RAM);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarId(pedido.TarjetaGrafica);

            return placabase.Precio + procesador.Precio + chasis.Precio + ram.Precio + tarjetaGrafica.Precio;
        }

        #endregion

    }
}
