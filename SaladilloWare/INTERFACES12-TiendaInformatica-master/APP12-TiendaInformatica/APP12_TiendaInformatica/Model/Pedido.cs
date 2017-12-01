using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace APP12_TiendaInformatica.Model
{
    [Table("Pedido")]
    public class Pedido
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull, Column("Usuario")]
        public int Usuario { get; set; }
        [NotNull, Column("PB")]
        public int PlacaBase { get; set; }
        [NotNull, Column("CPU")]
        public int Procesador { get; set; }
        [NotNull, Column("GPU")]
        public int TarjetaGrafica { get; set; }
        [NotNull, Column("Chasis")]
        public int Chasis { get; set; }
        [NotNull, Column("RAM")]
        public int RAM { get; set; }
    }
}
