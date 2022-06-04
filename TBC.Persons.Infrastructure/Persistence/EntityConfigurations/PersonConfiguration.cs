using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Infrastructure.Persistence.EntityConfigurations
{
    public class PersonConfiguration :
        IEntityTypeConfiguration<Person>,
        IEntityTypeConfiguration<PersonRelationship>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("getdate()");

            builder
                .HasIndex(x => x.PersonalNumber)
                .IsUnique();
            builder
                .Property(x => x.PersonalNumber)
                .HasMaxLength(11)
                .IsRequired();

            builder.OwnsOne(x => x.Firstname);
            builder.OwnsOne(x => x.Lastname);
            builder.OwnsMany(
                x => x.Phones,
                builder =>
                {
                    builder.ToTable("PersonPhones");
                });

            builder.HasOne(x => x.City);
            builder
                .HasMany(x => x.RelatedPersons)
                .WithOne(x => x.MasterPerson)
                .HasForeignKey(x => x.MasterPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.RelatedPersonOf)
                .WithOne(x => x.RelatedPerson)
                .HasForeignKey(x => x.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public void Configure(EntityTypeBuilder<PersonRelationship> builder)
        {
            builder.ToTable("PersonRelationships");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("getdate()");
        }
    }
}
