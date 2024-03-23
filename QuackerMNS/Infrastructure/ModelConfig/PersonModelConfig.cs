namespace QuackerMNS.Infrastructure.ModelConfig
{
    using global::QuackerMNS.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace QuackerMNS.Infrastructure.Database.EntityConfig
    {
        public class PersonModelConfig : IEntityTypeConfiguration<Person>
        {
            public void Configure(EntityTypeBuilder<Person> builder)
            {
                builder.ToTable("person");
                builder.HasKey(e => e.Person_Id);
                builder.Property(e => e.Person_Password).IsRequired().HasMaxLength(255);
                builder.HasIndex(e => e.Person_Email).IsUnique();
                builder.HasIndex(e => e.Person_PhoneNumber);
                builder.Property(e => e.Person_FirstName).IsRequired().HasMaxLength(50);
                builder.Property(e => e.Person_LastName).IsRequired().HasMaxLength(50);
                builder.Property(e => e.Person_CreatedTimeUser);
                builder.Property(e => e.Person_ProfilPicturePath).HasMaxLength(255);
                builder.Property(e => e.Person_Description).HasMaxLength(500);
                builder.Property(e => e.Person_IsTemporaryPassword);
                builder.Property(e => e.Person_TokenResetPassword).HasMaxLength(255);
                builder.HasIndex(e => e.PersonJobTitle_Id);
                builder.HasIndex(e => e.PersonStatut_Id);
                builder.HasIndex(e => e.PersonRole_Id);
            }
        }
    }
}
