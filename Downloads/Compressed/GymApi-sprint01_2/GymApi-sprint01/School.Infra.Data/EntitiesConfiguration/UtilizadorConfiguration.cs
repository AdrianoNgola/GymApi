using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.Infra.Data.EntitiesConfiguration
{
    public class UtilizadorConfiguration : IEntityTypeConfiguration<Utilizador>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Utilizador> builder)
        {
            builder.HasKey(u => u.UtilizadorId);
            builder.Property(u => u.Nome).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.PalavraPasseHash).IsRequired();
            builder.Property(u => u.PalavraPasseSalt).IsRequired();
            builder.Property(u => u.DataNascimento).IsRequired();
            builder.Property(u => u.GeneroId).IsRequired();
            builder.Property(u => u.PerfilId).IsRequired();
            builder.Property(u => u.Activo).IsRequired();
            builder.Property(u => u.DataInicio).IsRequired();
            builder.Property(u => u.DataFim).IsRequired(false);
            builder.Property(u => u.DataActualizacao).IsRequired();

            builder.HasOne(u => u.Perfil)
                .WithMany()
                .HasForeignKey(u => u.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Genero)
                .WithMany()
                .HasForeignKey(u => u.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}