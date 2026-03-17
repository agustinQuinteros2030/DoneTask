using System;

namespace DoneTask.Models.Dto
{
    public class TableroResponseDto
    {
        
            public Guid Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaCreacion { get; set; }
            public Guid CreadorId { get; set; }
           
        }
    }

