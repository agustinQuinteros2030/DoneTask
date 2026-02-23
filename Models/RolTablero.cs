using DoneTask.Models.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace DoneTask.Models
{
    public class RolTablero
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = ErrorMsg.RangoCaracteres)]
        public string Nombre { get; set; }

        // Navegación
        public List<UsuarioTablero> UsuariosTablero { get; set; } = new();
    }
}
