using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.InfraData.EntitiesConfiguration
{
    public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Matricula> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.DataMatricula).IsRequired();
            builder.Property(m => m.UserId).IsRequired();
            builder.Property(m => m.TurmaId).IsRequired();
            builder.Property(m => m.DataExp).IsRequired();

            builder.HasOne(m => m.User)
                .WithMany(u => u.Matriculas)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Turma)
                .WithMany(t => t.Matriculas)
                .HasForeignKey(m => m.TurmaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}