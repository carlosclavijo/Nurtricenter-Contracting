using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Contracts;
using Contracting.Domain.Delivery;
using Contracting.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Contracting.Infrastructure.DomainModel.Config;

internal class ContractConfig : IEntityTypeConfiguration<Contract>, IEntityTypeConfiguration<DeliveryDay>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("contracts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("contractId");

        builder.Property(x => x.AdministratorId).HasColumnName("administratorId");

        builder.Property(x => x.PatientId).HasColumnName("patientId");

        var typeConverter = new ValueConverter<ContractType, string>(
            typeEnumValue => typeEnumValue.ToString(),
            type => (ContractType)Enum.Parse(typeof(ContractType), type)
        );

        builder.Property(x => x.Type).HasConversion(typeConverter).HasColumnName("transactionType");

        var statusConverter = new ValueConverter<ContractStatus, string>(
            statusEnumValue => statusEnumValue.ToString(),
            status => (ContractStatus)Enum.Parse(typeof(ContractStatus), status)
        );

        builder.Property(x => x.Status).HasConversion(statusConverter).HasColumnName("transactionStatus");

        builder.Property(x => x.CreationDate).HasColumnName("creationDate");

        builder.Property(x => x.StartDate).HasColumnName("startDate");

        builder.Property(x => x.CompletedDate).HasColumnName("completedDate").IsRequired(false);

        var costConverter = new ValueConverter<CostValue, decimal>(
            costValue => costValue.Value,
            cost => new CostValue(cost)
        );

        builder.Property(x => x.Cost).HasColumnName("totalCost").HasConversion(costConverter);

        builder.HasMany(c => c.DeliveryDays)
               .WithOne(c => c.Contract)
               .HasForeignKey(c => c.ContractId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore("_domainEvents");
        builder.Ignore(x => x.DomainEvents);
    }

    public void Configure(EntityTypeBuilder<DeliveryDay> builder)
    {
        builder.ToTable("deliverydays");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("deliveryDayId");
        builder.Property(x => x.ContractId).HasColumnName("contractId");

        builder.Property(x => x.Date).HasColumnName("date");

        builder.Property(x => x.Street).HasColumnName("street");
        builder.Property(x => x.Number).HasColumnName("number");

        builder.Property(x => x.Longitude).HasColumnName("longitude");

        builder.Property(x => x.Latitude).HasColumnName("latitude");

        builder.HasOne(c => c.Contract)  // Asegúrate de tener esta relación configurada en DeliveryDay
               .WithMany(c => c.DeliveryDays)
               .HasForeignKey(d => d.ContractId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}