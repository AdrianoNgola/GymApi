using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.Infra.Data.EntitiesConfiguration
{
    public class PerfilCofiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Perfil> builder)
        {
            builder.HasKey(p => p.PerfilId);
            builder.Property(p => p.Nome).IsRequired();
        }
    }
}