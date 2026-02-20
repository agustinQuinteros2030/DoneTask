using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DoneTask.Models
{
    public class Rol : IdentityRole<Guid>
    {
        public Rol() : base()
        {

        }

        public Rol(string name) : base(name)
        {

        }
        public Guid Id { get; set; }
        [Display(Name = "rol")]
        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
    }
}

