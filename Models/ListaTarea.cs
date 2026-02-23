using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DoneTask.Models.DoneTask.Models;
using DoneTask.Models.Helper;

namespace DoneTask.Models
{
    public class ListaTarea
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ErrorMsg.RangoCaracteres)]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = ErrorMsg.RangoCaracteres)]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // FK
        public Guid TableroId { get; set; }
        public Tablero Tablero { get; set; }

        public List<Tarea> Tareas { get; set; } = new();
    }
}