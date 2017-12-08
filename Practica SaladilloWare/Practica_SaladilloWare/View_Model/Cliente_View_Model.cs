using System;
using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.Assets;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Practica_SaladilloWare.View_Model
{
    public class Cliente_View_Model
    {

        #region Declaracion de variables

        public Usuario Usuario; // El usuario que ha iniciado sesión.
        INavigation Navigation; // Necesario para poder navegar entre las distintas vistas.
        Page Page; // Necesario para poder realizar diálogos.

        // Elementos del layout
        Picker picPlacaBase, picProcesador, picChasis, picMemoria, picTarjetaGrafica; 
        ListView lstResumen;
        Label lblTotal;
        Button btnAceptar, btnConfirmar;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor. Necesita muchos datos de la vista asociada, ya que esta es la parte lógica.
        /// </summary>
        /// <param name="page">El code behind de la vista asociada.</param>
        /// <param name="navigation">Necesario para poder navegar entre vistas.</param>
        /// <param name="usuario">El usuario que ha iniciado la sesion.</param>
        /// <param name="picPlacaBase"></param>
        /// <param name="picProcesador"></param>
        /// <param name="picChasis"></param>
        /// <param name="picMemoria"></param>
        /// <param name="picTarjetaGrafica"></param>
        /// <param name="lstResumen"></param>
        /// <param name="lblTotal"></param>
        /// <param name="btnAceptar"></param>
        /// <param name="btnConfirmar"></param>
        public Cliente_View_Model(Page page, INavigation navigation, Usuario usuario, Picker picPlacaBase, Picker picProcesador, Picker picChasis, Picker picMemoria, Picker picTarjetaGrafica, ListView lstResumen, Label lblTotal, Button btnAceptar, Button btnConfirmar)
        {
            Page = page;
            Navigation = navigation;
            Usuario = usuario;
            this.picPlacaBase = picPlacaBase;
            this.picProcesador = picProcesador;
            this.picChasis = picChasis;
            this.picMemoria = picMemoria;
            this.picTarjetaGrafica = picTarjetaGrafica;
            this.lstResumen = lstResumen;
            this.lblTotal = lblTotal;
            this.btnAceptar = btnAceptar;
            this.btnConfirmar = btnConfirmar;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Comprueba que algun Picker aún no tiene algo seleccionado, y si es así, deja deshabilitados los botones Aceptar y Confirmar.
        /// </summary>
        public void ComprobarSeleccion()
        {
            btnAceptar.IsEnabled = true;
            btnConfirmar.IsEnabled = true;

            if (picPlacaBase.SelectedIndex == -1 || 
                picProcesador.SelectedIndex == -1 || 
                picChasis.SelectedIndex == -1 || 
                picMemoria.SelectedIndex == -1 || 
                picTarjetaGrafica.SelectedIndex == -1)
            {
                btnAceptar.IsEnabled = false;
                btnConfirmar.IsEnabled = false;
            }
        }

        /// <summary>
        /// Rellena la lista Resumen con los componentes seleccionados.
        /// </summary>
        /// <param name="placabaseSeleccionado"></param>
        /// <param name="procesadorSeleccionado"></param>
        /// <param name="chasisSeleccionado"></param>
        /// <param name="ramSeleccionada"></param>
        /// <param name="tarjetagraficaSeleccionada"></param>
        /// <returns>La colección de los componentes seleccionados.</returns>
        public static async Task<List<Object>> RellenarLista(String placabaseSeleccionado, String procesadorSeleccionado, String chasisSeleccionado, String ramSeleccionada, String tarjetagraficaSeleccionada)
        {
            // A partir de los nombres de los componentes seleccionados, los buscamos en la base de datos, y sacamos todos los datos de cada componente.
            PlacaBase placabase = await PlacaBase_Repository.ComprobarNombre(placabaseSeleccionado);
            Procesador procesador = await Procesador_Repository.ComprobarNombre(procesadorSeleccionado);
            Chasis chasis = await Chasis_Repository.ComprobarNombre(chasisSeleccionado);
            RAM ram = await RAM_Repository.ComprobarNombre(ramSeleccionada);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarNombre(tarjetagraficaSeleccionada);

            // Metemos en una colección los componentes. Ya con DataBinding la propia lista Resumen sabrá qué propiedades debe mostrar.
            List<Object> LineasResumen = new List<Object>
            {
                placabase,
                procesador,
                chasis,
                ram,
                tarjetaGrafica
            };

            return LineasResumen;
        }

        /// <summary>
        /// Recoje los componentes seleccionados, los asigna a la lista Resumen, y calcula el precio total y lo muestra.
        /// </summary>
        /// <returns></returns>
        public async Task RellenarResumenAsync()
        {
            lstResumen.ItemsSource = await RellenarLista(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);

            lblTotal.Text = await ObtenerTotal(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);
        }

        /// <summary>
        /// Calcula el total de todos los componentes seleccionados.
        /// </summary>
        /// <param name="placabaseSeleccionado"></param>
        /// <param name="procesadorSeleccionado"></param>
        /// <param name="chasisSeleccionado"></param>
        /// <param name="ramSeleccionada"></param>
        /// <param name="tarjetagraficaSeleccionada"></param>
        /// <returns>La suma de todos los componentes seleccionados.</returns>
        public static async Task<String> ObtenerTotal(String placabaseSeleccionado, String procesadorSeleccionado, String chasisSeleccionado, String ramSeleccionada, String tarjetagraficaSeleccionada)
        {
            // A partir de los nombres de los componentes seleccionados, los buscamos en la base de datos, y sacamos todos los datos de cada componente.
            PlacaBase placabase = await PlacaBase_Repository.ComprobarNombre(placabaseSeleccionado);
            Procesador procesador = await Procesador_Repository.ComprobarNombre(procesadorSeleccionado);
            Chasis chasis = await Chasis_Repository.ComprobarNombre(chasisSeleccionado);
            RAM ram = await RAM_Repository.ComprobarNombre(ramSeleccionada);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarNombre(tarjetagraficaSeleccionada);

            // Devolvemos la suma de los 5 componentes seleccionados.
            return "Total: " + (placabase.Precio + procesador.Precio + chasis.Precio + ram.Precio + tarjetaGrafica.Precio) + "€";
        }

        /// <summary>
        /// Recoje los elementos seleccionados e introduce en la base de datos un nuevo pedido.
        /// </summary>
        /// <param name="placabaseSeleccionado"></param>
        /// <param name="procesadorSeleccionado"></param>
        /// <param name="chasisSeleccionado"></param>
        /// <param name="ramSeleccionada"></param>
        /// <param name="tarjetagraficaSeleccionada"></param>
        /// <returns></returns>
        public async Task GenerarPedido(String placabaseSeleccionado, String procesadorSeleccionado, String chasisSeleccionado, String ramSeleccionada, String tarjetagraficaSeleccionada)
        {
            // A partir de los nombres de los componentes seleccionados, los buscamos en la base de datos, y sacamos todos los datos de cada componente.
            PlacaBase placabase = await PlacaBase_Repository.ComprobarNombre(placabaseSeleccionado);
            Procesador procesador = await Procesador_Repository.ComprobarNombre(procesadorSeleccionado);
            Chasis chasis = await Chasis_Repository.ComprobarNombre(chasisSeleccionado);
            RAM ram = await RAM_Repository.ComprobarNombre(ramSeleccionada);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarNombre(tarjetagraficaSeleccionada);

            // Con estos componentes, los pasamos a la base de datos y queda el pedido realizado.
            await App.Pedido_Repository.AddNewPedidoAsync(Usuario, placabase, procesador, tarjetaGrafica, ram, chasis);
        }

        /// <summary>
        /// Rellena cada menu desplegable con los productos que se encuentren en el catalogo.
        /// </summary>
        /// <returns></returns>
        public async Task RellenarPickers()
        {
            // Rellenamos cada Picker con los productos que hay en la base de datos.
            picPlacaBase.ItemsSource = await PlacaBase_Repository.GetNombres();
            picProcesador.ItemsSource = await Procesador_Repository.GetNombres();
            picChasis.ItemsSource = await Chasis_Repository.GetNombres();
            picMemoria.ItemsSource = await RAM_Repository.GetNombres();
            picTarjetaGrafica.ItemsSource = await TarjetaGrafica_Repository.GetNombres();
        }

        /// <summary>
        /// Resetea la vista. Borra la selección de cada Picker y los datos de la lista Resumen.
        /// </summary>
        public void LimpiarFormulario()
        {
            // Volvemos a vaciar el contenido de todos los Picker y la lista de Productos.
            picPlacaBase.SelectedItem = -1;
            picProcesador.SelectedItem = -1;
            picChasis.SelectedItem = -1;
            picMemoria.SelectedItem = -1;
            picTarjetaGrafica.SelectedItem = -1;

            lstResumen.ItemsSource = new List<String>();
            lblTotal.Text = "";
        }

        /// <summary>
        /// Realiza las operaciones que actualizan la base de datos, notifican al usuario y resetea la vista.
        /// </summary>
        /// <returns></returns>
        public async Task RealizarPedido()
        {
            // Introducimos en la base de datos un nuevo pedido con los IDs de los productos seleccionados y el ID del usuario.
            await GenerarPedido(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);

            // Notificamos al usuario de que el pedido ha sido realizado.
            await Page.DisplayAlert("¡Gracias por su confianza!",
                            "Pedido realizado con éxito.",
                            "Limpiar formulario y realizar otro pedido");

            // Y reseteamos la vista.
            LimpiarFormulario();
        }

        #endregion

    }
}
