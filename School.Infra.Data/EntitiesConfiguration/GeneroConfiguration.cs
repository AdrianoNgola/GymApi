using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.Infra.Data.EntitiesConfiguration
{
    public class GeneroConfiguration: IEntityTypeConfiguration<Genero>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Genero> builder)
        {
            builder.HasKey(g => g.GeneroId);
            builder.Property(g => g.Nome).IsRequired();
        }
    }
}