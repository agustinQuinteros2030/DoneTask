using DoneTask.Models.DoneTask.Models;
using DoneTask.Models.Helper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoneTask.Models
{
    public class Usuario : IdentityUser<Guid>
    {
        //public Guid Id { get; set; }
        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = ErrorMsg.RangoCaracteres)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = ErrorMsg.SoloLetrasNumeros)]
        public override string UserName
        {
            get => base.UserName;
            set => base.UserName = value;
        }
        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = ErrorMsg.RangoCaracteres)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = ErrorMsg.SoloLetras)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = ErrorMsg.RangoCaracteres)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = ErrorMsg.SoloLetras)]
        public string Apellido { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = ErrorMsg.CampoRequerido)]
        [EmailAddress(ErrorMessage = ErrorMsg.FormatoEmail)]
        public override string Email
        {
            get => base.Email;
            set => base.Email = value;
        }

        public List<UsuarioTablero> TablerosUsuario { get; set; } = new();

    }
}
