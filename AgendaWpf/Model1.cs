namespace AgendaWpf
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=agendaWpf")
        {
        }

        public virtual DbSet<appointments> appointments { get; set; }
        public virtual DbSet<brokers> brokers { get; set; }
        public virtual DbSet<customers> customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<appointments>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<brokers>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<brokers>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<brokers>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<brokers>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<brokers>()
                .HasMany(e => e.appointments)
                .WithRequired(e => e.brokers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<customers>()
                .HasMany(e => e.appointments)
                .WithRequired(e => e.customers)
                .WillCascadeOnDelete(false);
        }
    }
}
