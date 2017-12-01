﻿using SQLite;
using System;

namespace Practica_SaladilloWare.Model
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
