using SQLite;
using System;

namespace Practica_SaladilloWare.Model
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(20), Unique, Column("Nombre")]
        public String Nombre { get; set; }
        [MaxLength(20), Column("Contrasenia")]
        public String Contrasenia { get; set; }
        [MaxLength(1), Column("Tipo")]
        public String Tipo { get; set; }
    }
}
