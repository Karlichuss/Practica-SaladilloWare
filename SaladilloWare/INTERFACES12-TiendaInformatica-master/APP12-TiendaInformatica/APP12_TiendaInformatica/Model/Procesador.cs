using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP12_TiendaInformatica.Model
{
    [Table("Procesador")]
    public class Procesador
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(45), Column("Nombre")]
        public String Nombre { get; set; }
        [Column("Precio")]
        public float Precio { get; set; }
    }
}
