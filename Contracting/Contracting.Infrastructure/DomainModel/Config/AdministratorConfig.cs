using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Administrators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracting.Infrastructure.DomainModel.Config;

internal class AdministratorConfig : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("administrators");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("administratorId");

        builder.Property(x => x.Name).HasColumnName("name");

        builder.Property(x => x.Phone).HasColumnName("phone");

        builder.Ignore("_domainEvents");
        builder.Ignore(x => x.DomainEvents);
    }
}
