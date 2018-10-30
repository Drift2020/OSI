namespace Chat.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ChatDB1")
        {
        }

        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Correspondence_Contacts> Correspondence_Contacts { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Сorrespondence> Сorrespondence { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacts>()
                .HasMany(e => e.Correspondence_Contacts)
                .WithRequired(e => e.Contacts)
                .HasForeignKey(e => e.id_contact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<List>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.List)
                .HasForeignKey(e => e.Letters_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Сorrespondence)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.Id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Сorrespondence>()
                .HasMany(e => e.Correspondence_Contacts)
                .WithRequired(e => e.Сorrespondence)
                .HasForeignKey(e => e.id_correspondence)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Сorrespondence>()
                .HasMany(e => e.List)
                .WithRequired(e => e.Сorrespondence)
                .HasForeignKey(e => e.messegeID)
                .WillCascadeOnDelete(false);
        }
    }
}
