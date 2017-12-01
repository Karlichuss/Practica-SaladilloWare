using SQLite;

namespace Practica_SaladilloWare.Model
{
    [Table("Pedido")]
    public class Pedido
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull, Column("Usuario")]
        public int Usuario { get; set; }
        [NotNull, Column("PlacaBase")]
        public int PlacaBase { get; set; }
        [NotNull, Column("Procesador")]
        public int Procesador { get; set; }
        [NotNull, Column("TarjetaGrafica")]
        public int TarjetaGrafica { get; set; }
        [NotNull, Column("Chasis")]
        public int Chasis { get; set; }
        [NotNull, Column("RAM")]
        public int RAM { get; set; }
    }
}
