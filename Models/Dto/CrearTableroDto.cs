using DoneTask.Models.Helper;
using System.ComponentModel.DataAnnotations;

namespace DoneTask.Models.Dto
{
    public class CrearTableroDto
    {
        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ErrorMsg.RangoCaracteres)]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = ErrorMsg.RangoCaracteres)]
        public string Descripcion { get; set; }
    }
}
