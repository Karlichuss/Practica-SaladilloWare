using System;

namespace Practica_SaladilloWare.Model
{
    public class LineaPedido
    {

        public int Id { get; set; }
        public String Usuario { get; set; }
        public String PlacaBase { get; set; }
        public String Procesador { get; set; }
        public String TarjetaGrafica { get; set; }
        public String Chasis { get; set; }
        public String RAM { get; set; }
        public float Total { get; set; }

        #region Constructores

        public LineaPedido(int id, string usuario, string placaBase, string procesador, string tarjetaGrafica, string chasis, string ram, float total)
        {
            Id = id;
            Usuario = usuario;
            PlacaBase = placaBase;
            Procesador = procesador;
            TarjetaGrafica = tarjetaGrafica;
            Chasis = chasis;
            RAM = ram;
            Total = total;
        }

        #endregion
    }
}
