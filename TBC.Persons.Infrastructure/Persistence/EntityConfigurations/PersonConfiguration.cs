using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Infrastructure.Persistence.EntityConfigurations
{
    public class PersonConfiguration :
        IEntityTypeConfiguration<Person>,
        IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("getdate()");

            builder.Property(x => x.PersonalNumber).IsRequired();

            builder.HasOne(x => x.City);
            builder.HasMany(x => x.RelatedPersons).WithOne(x => x.Person)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(x => x.Firstname);
            builder.OwnsOne(x => x.Lastname);
            builder.OwnsMany(
                x => x.Phones,
                builder =>
                {
                    builder.ToTable("PersonPhones");
                });
        }

        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.ToTable("RelatedPersons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("getdate()");
        }
    }
}
