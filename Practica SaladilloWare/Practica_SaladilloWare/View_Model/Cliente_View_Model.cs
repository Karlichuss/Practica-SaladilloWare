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
        public Usuario Usuario;
        INavigation Navigation;
        Page Page;
        Picker picPlacaBase, picProcesador, picChasis, picMemoria, picTarjetaGrafica;
        ListView lstResumen;
        Label lblTotal;
        Button btnAceptar, btnConfirmar;

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

        public void ComprobarSeleccion()
        {
            btnAceptar.IsEnabled = true;
            btnConfirmar.IsEnabled = true;

            if (picPlacaBase.SelectedIndex == -1 || picProcesador.SelectedIndex == -1 || picChasis.SelectedIndex == -1 || picMemoria.SelectedIndex == -1 || picTarjetaGrafica.SelectedIndex == -1)
            {
                btnAceptar.IsEnabled = false;
                btnConfirmar.IsEnabled = false;
            }
        }

        public static async Task<List<Object>> RellenarLista(String placabaseSeleccionado, String procesadorSeleccionado, String chasisSeleccionado, String ramSeleccionada, String tarjetagraficaSeleccionada)
        {

            PlacaBase placabase = await PlacaBase_Repository.ComprobarNombre(placabaseSeleccionado);
            Procesador procesador = await Procesador_Repository.ComprobarNombre(procesadorSeleccionado);
            Chasis chasis = await Chasis_Repository.ComprobarNombre(chasisSeleccionado);
            RAM ram = await RAM_Repository.ComprobarNombre(ramSeleccionada);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarNombre(tarjetagraficaSeleccionada);

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

        public static async Task<String> ObtenerTotal(String placabaseSeleccionado, String procesadorSeleccionado, String chasisSeleccionado, String ramSeleccionada, String tarjetagraficaSeleccionada)
        {
            PlacaBase placabase = await PlacaBase_Repository.ComprobarNombre(placabaseSeleccionado);
            Procesador procesador = await Procesador_Repository.ComprobarNombre(procesadorSeleccionado);
            Chasis chasis = await Chasis_Repository.ComprobarNombre(chasisSeleccionado);
            RAM ram = await RAM_Repository.ComprobarNombre(ramSeleccionada);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarNombre(tarjetagraficaSeleccionada);

            return "Total: " + (placabase.Precio + procesador.Precio + chasis.Precio + ram.Precio + tarjetaGrafica.Precio) + "€";
        }

        public async Task GenerarPedido(String placabaseSeleccionado, String procesadorSeleccionado, String chasisSeleccionado, String ramSeleccionada, String tarjetagraficaSeleccionada)
        {
            PlacaBase placabase = await PlacaBase_Repository.ComprobarNombre(placabaseSeleccionado);
            Procesador procesador = await Procesador_Repository.ComprobarNombre(procesadorSeleccionado);
            Chasis chasis = await Chasis_Repository.ComprobarNombre(chasisSeleccionado);
            RAM ram = await RAM_Repository.ComprobarNombre(ramSeleccionada);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarNombre(tarjetagraficaSeleccionada);

            await App.Pedido_Repository.AddNewPedidoAsync(Usuario, placabase, procesador, tarjetaGrafica, ram, chasis);
        }

        public async Task RellenarPickers()
        {
            // Rellenas cada Picker con los productos que hay en la base de datos.
            picPlacaBase.ItemsSource = await PlacaBase_Repository.GetNombres();
            picProcesador.ItemsSource = await Procesador_Repository.GetNombres();
            picChasis.ItemsSource = await Chasis_Repository.GetNombres();
            picMemoria.ItemsSource = await RAM_Repository.GetNombres();
            picTarjetaGrafica.ItemsSource = await TarjetaGrafica_Repository.GetNombres();
        }

        public void LimpiarFormulario()
        {
            // Vuelves a vaciar el contenido de todos los Picker y la lista de Productos.
            picPlacaBase.SelectedItem = -1;
            picProcesador.SelectedItem = -1;
            picChasis.SelectedItem = -1;
            picMemoria.SelectedItem = -1;
            picTarjetaGrafica.SelectedItem = -1;

            lstResumen.ItemsSource = new List<String>();
            lblTotal.Text = "";
        }

        public async Task RealizarPedido()
        {
            // Introduces en la base de datos un nuevo pedido con los IDs de los productos seleccionados y el ID del usuario. Luego limpiamos el formulario.
            await GenerarPedido(picPlacaBase.Items[picPlacaBase.SelectedIndex], picProcesador.Items[picProcesador.SelectedIndex], picChasis.Items[picChasis.SelectedIndex], picMemoria.Items[picMemoria.SelectedIndex], picTarjetaGrafica.Items[picTarjetaGrafica.SelectedIndex]);

            await Page.DisplayAlert("¡Gracias por su confianza!",
                            "Pedido realizado con éxito.",
                            "Limpiar formulario y realizar otro pedido");

            LimpiarFormulario();
        }
    }
}
