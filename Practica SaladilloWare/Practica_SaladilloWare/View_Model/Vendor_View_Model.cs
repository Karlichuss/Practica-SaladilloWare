using System.Threading.Tasks;
using Practica_SaladilloWare.Model;
using System.Collections.Generic;
using Practica_SaladilloWare.Assets;
using System;

namespace Practica_SaladilloWare.View_Model
{
    public class Vendor_View_Model
    {

        public Usuario usuario;
        public List<Pedido> pedidos;

        public Vendor_View_Model(Usuario usuario)
        {
            this.usuario = usuario;
        }

        public Task ActualizarPrecios()
        {
            return null;
        }

        public async Task<List<LineaPedido>> CargarPedidosAsync()
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

        private async Task<float> CalcularTotalAsync(Pedido pedido)
        {
            PlacaBase placabase = await PlacaBase_Repository.ComprobarId(pedido.PlacaBase);
            Procesador procesador = await Procesador_Repository.ComprobarId(pedido.Procesador);
            Chasis chasis = await Chasis_Repository.ComprobarId(pedido.Chasis);
            RAM ram = await RAM_Repository.ComprobarId(pedido.RAM);
            TarjetaGrafica tarjetaGrafica = await TarjetaGrafica_Repository.ComprobarId(pedido.TarjetaGrafica);

            return placabase.Precio + procesador.Precio + chasis.Precio + ram.Precio + tarjetaGrafica.Precio;
        }
    }
}
