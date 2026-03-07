using System.ComponentModel.DataAnnotations;

namespace DoneTask.Models.viewModels
{
    public class InicioSesion
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool Recordarme { get; set; }
    }
}
