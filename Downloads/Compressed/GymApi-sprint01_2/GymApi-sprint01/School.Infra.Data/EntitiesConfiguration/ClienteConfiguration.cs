using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Infra.Data.EntitiesConfiguration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.ClienteId);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Telefone).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Endereco).IsRequired().HasMaxLength(100);
            builder.Property(c => c.DataNascimento).IsRequired();
            builder.Property(c => c.DataInicio).IsRequired();
            builder.Property(c => c.DataFim).IsRequired(false);
            builder.Property(c => c.DataActualizacao).IsRequired();
            builder.Property(c => c.Activo).IsRequired();
            builder.Property(c => c.GeneroId).IsRequired();
            builder.Property(c => c.CriadoUtilizadorId).IsRequired();
            builder.Property(c => c.ActualUtilizadorId).IsRequired(false);

            builder.HasOne(c => c.genero)
                .WithMany()
                .HasForeignKey(c => c.GeneroId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.utilizador)
                .WithMany()
                .HasForeignKey(c => c.CriadoUtilizadorId)
                .HasForeignKey(c => c.ActualUtilizadorId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}