using System;

namespace Practica_SaladilloWare.Model
{
    public class LineaPedido
    {
        public LineaPedido(int id, string usuario, string placaBase, string procesador, string tarjetaGrafica, string chasis, string rAM, float total)
        {
            Id = id;
            Usuario = usuario;
            PlacaBase = placaBase;
            Procesador = procesador;
            TarjetaGrafica = tarjetaGrafica;
            Chasis = chasis;
            RAM = rAM;
            Total = total;
        }

        public int Id { get; set; }
        public String Usuario { get; set; }
        public String PlacaBase { get; set; }
        public String Procesador { get; set; }
        public String TarjetaGrafica { get; set; }
        public String Chasis { get; set; }
        public String RAM { get; set; }
        public float Total { get; set; }
    }
}
