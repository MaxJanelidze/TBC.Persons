using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using TBC.Persons.Domain.Aggregates.Cities;
using TBC.Persons.Domain.Shared.ValueObjects;

namespace TBC.Persons.Infrastructure.Persistence.EntityConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("getdate()");

            builder.OwnsOne(x => x.Name);
        }
    }
}
