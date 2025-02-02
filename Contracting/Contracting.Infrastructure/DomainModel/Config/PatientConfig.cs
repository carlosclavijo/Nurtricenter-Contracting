using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contracting.Infrastructure.DomainModel.Config;

internal class PatientConfig : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("patientId");

        builder.Property(x => x.Name).HasColumnName("name");

        builder.Property(x => x.Phone).HasColumnName("phone");

        builder.Ignore("_domainEvents");
        builder.Ignore(x => x.DomainEvents);
    }
}
