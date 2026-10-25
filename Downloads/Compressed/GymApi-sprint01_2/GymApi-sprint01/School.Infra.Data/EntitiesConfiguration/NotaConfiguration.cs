using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.InfraData.EntitiesConfiguration
{
    public class NotaConfiguration : IEntityTypeConfiguration<Nota>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Nota> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.ValorNota).IsRequired();
            builder.Property(n => n.Aprovado).IsRequired();
            builder.Property(n => n.MatriculaId).IsRequired();

            builder.HasOne(n => n.Matricula)
                   .WithMany(m => m.ValorNota)
                   .HasForeignKey(n => n.MatriculaId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}