using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.InfraData.EntitiesConfiguration
{
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Nome).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Descricao).IsRequired().HasMaxLength(200);
            builder.Property(t => t.CursoId).IsRequired();
            
            builder.HasOne(t => t.Curso)
                   .WithMany(c => c.Turma)
                   .HasForeignKey(t => t.CursoId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}