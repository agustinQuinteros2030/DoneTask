namespace DoneTask.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::DoneTask.Models.Helper;

    namespace DoneTask.Models
    {
        public class Tablero
        {
            public Guid Id { get; set; }

            [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
            [StringLength(50, MinimumLength = 3, ErrorMessage = ErrorMsg.RangoCaracteres)]
            public string Nombre { get; set; }

            [StringLength(250, ErrorMessage = ErrorMsg.RangoCaracteres)]
            public string Descripcion { get; set; }

            public DateTime FechaCreacion { get; set; } = DateTime.Now;

            // FK del creador
            [Required]
            public Guid CreadorId { get; set; }

            public Usuario Creador { get; set; }

            // Muchos a muchos
            public List<Usuario> Participantes { get; set; } = new();

            // Uno a muchos
            public List<ListaTarea> ListaTareas { get; set; } = new();
        }
    }
}