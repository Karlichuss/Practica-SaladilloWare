using System;
using System.Collections;
using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.Assets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.View_Model
{
    public class Cliente_View_Model
    {
        public Usuario usuario;

        public Cliente_View_Model(Usuario usuario)
        {
            this.usuario = usuario;
        }

        public async Task<List<String>> GetPlacasBase()
        {
            return await PlacaBase_Repository.GetNombres();
        }

        public async Task<List<String>> GetProcesadores()
        {
            return await Procesador_Repository.GetNombres();
        }

        public async Task<List<String>> GetChasis()
        {
            return await Chasis_Repository.GetNombres();
        }

        public async Task<List<String>> GetRAMs()
        {
            return await RAM_Repository.GetNombres();
        }

        public async Task<List<String>> GetTarjetasGraficas()
        {
            return await TarjetaGrafica_Repository.GetNombres();
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

        internal async Task GenerarPedido(String placabaseSeleccionado, String procesadorSeleccionado, String chasisSeleccionado, String ramSeleccionada, String tarjetagraficaSeleccionada)
        {
            PlacaBase placabase = await PlacaBase_Repository.ComprobarNombre(placabaseSeleccionado);
            Procesador procesador = await Procesador_Repository.ComprobarNombre(procesadorSeleccionado);
            Chasis chasis = await Chasis_Repository.ComprobarNombre(chasisSeleccionado);
            RAM ram = await RAM_Repository.ComprobarNombre(ramSeleccionada);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarNombre(tarjetagraficaSeleccionada);

            await App.Pedido_Repository.AddNewPedidoAsync(usuario, placabase, procesador, tarjetaGrafica, ram, chasis);
        }
    }
}
