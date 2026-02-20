using DoneTask.Models.DoneTask.Models;
using System;
using System.Collections.Generic;

namespace DoneTask.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        public string Email { get; set; }

        public List<Tablero> Tableros { get; set; } = new List<Tablero>();
    }
}
