using DoneTask.Models;
using DoneTask.Models.DoneTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace DoneTask.Data
{
    public class ApplicationDbContext
       : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Tablero> Tableros { get; set; }
        public DbSet<ListaTarea> ListasTareas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Subtarea> Subtareas { get; set; }
        public DbSet<RolTablero> RolesTablero { get; set; }
        public DbSet<UsuarioTablero> usuarioTableros { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

         



            // UsuarioTablero (tabla puente)


            builder.Entity<UsuarioTablero>()
                    .HasKey(ut => new { ut.UsuarioId, ut.TableroId });

            // Usuario -> UsuarioTablero
            builder.Entity<UsuarioTablero>()
                .HasOne(ut => ut.Usuario)
                .WithMany(u => u.TablerosUsuario)
                .HasForeignKey(ut => ut.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tablero -> UsuarioTablero
            builder.Entity<UsuarioTablero>()
                .HasOne(ut => ut.Tablero)
                .WithMany(t => t.UsuariosTablero)
                .HasForeignKey(ut => ut.TableroId)
                .OnDelete(DeleteBehavior.Cascade);

            // RolTablero -> UsuarioTablero
            builder.Entity<UsuarioTablero>()
                .HasOne(ut => ut.RolTablero)
                .WithMany(r => r.UsuariosTablero)
                .HasForeignKey(ut => ut.RolTableroId)
                .OnDelete(DeleteBehavior.Restrict);


            // Tablero -> Creador


            builder.Entity<Tablero>()
                .HasOne(t => t.Creador)
                .WithMany()
                .HasForeignKey(t => t.CreadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }


        private void ForzarCambioDeHorario(IMutableEntityType entityType)
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(
                        new ValueConverter<DateTime, DateTime>(
                            v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                        )
                    );
                }

                if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(
                        new ValueConverter<DateTime?, DateTime?>(
                            v => v.HasValue
                                ? (v.Value.Kind == DateTimeKind.Utc
                                    ? v.Value
                                    : v.Value.ToUniversalTime())
                                : v,
                            v => v.HasValue
                                ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
                                : v
                        )
                    );
                }
            }
        }


    }
}

