using DoneTask.Models.DoneTask.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace DoneTask.Models
{
    public class UsuarioTablero
    {
      
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

      
        public Guid TableroId { get; set; }
        public Tablero Tablero { get; set; }

        
        public Guid RolTableroId { get; set; }
        public RolTablero RolTablero { get; set; }
    }
}
