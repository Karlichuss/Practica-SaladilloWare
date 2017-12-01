using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace APP12_TiendaInformatica.Model
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(20), Column("Nombre")]
        public String Nombre { get; set; }
        [MaxLength(20), Column("Contrasenia")]
        public String Contrasenia { get; set; }
        [MaxLength(1), Column("Tipo")]
        public String Tipo { get; set; }
    }
}
