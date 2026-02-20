using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DoneTask.Models.Helper;

namespace DoneTask.Models
{
    public class Tarea
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [StringLength(80, MinimumLength = 3, ErrorMessage = ErrorMsg.RangoCaracteres)]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = ErrorMsg.RangoCaracteres)]
        public string Descripcion { get; set; }

        public bool Completada { get; set; } = false;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // FK
        public Guid ListaTareaId { get; set; }
        public ListaTarea ListaTarea { get; set; }

        public List<Subtarea> Subtareas { get; set; } = new();
    }
}