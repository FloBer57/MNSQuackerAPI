using Microsoft.EntityFrameworkCore;
using QuackerMNS.Model;

namespace QuackerMNS.infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Définir les DbSet pour chaque type d'entité
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonStatut> PersonStatuts { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<PersonJobTitle> PersonJobTitles { get; set; }
        public DbSet<PersonXNotification> PersonXNotifications { get; set; }
        public DbSet<PersonXMessage> UsersXMessages { get; set; }
        public DbSet<PersonXLoggedIn> PersonXLoggedIns { get; set; }
        public DbSet<PersonXChannel> PersonXChannels { get; set; }
        public DbSet<ChannelRolePerson> ChannelPersonRoles { get; set; }
        public DbSet<ChannelType> ChannelTypes { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelRolePersonXPersonXChannel> ChannelRolePersonXPersonXChannels { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Logged> Loggeds { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<MessageXReactionXPerson> MessageXReactionXUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurations pour chaque table
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");
                entity.HasKey(e => e.Person_Id);
                entity.HasIndex(e => e.Person_Email).IsUnique();
                entity.Property(e => e.Person_Password).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<PersonStatut>(entity =>
            {
                entity.ToTable("personstatut");
                entity.HasKey(e => e.PersonStatut_Id);
            });

            modelBuilder.Entity<PersonRole>(entity =>
            {
                entity.ToTable("personrole");
                entity.HasKey(e => e.PersonRole_Id);
            });

            ModelBuilder.Ent

            // Continuez avec les autres entités...
        }
    }
}
