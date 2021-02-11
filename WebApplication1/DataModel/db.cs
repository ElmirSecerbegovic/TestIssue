using Microsoft.EntityFrameworkCore;
using System;

#nullable disable

namespace WebApplication1.DataModel
{
    public partial class db : DbContext
    {

        static string ConnectionString
        {
            get;
            set;
        }

        public db()
        {
        }

        protected db(DbContextOptions options)
         : base(options)
        {
        }

        public db(string conn_string)
        {
            if (String.IsNullOrEmpty(conn_string))
            {
                throw new Exception("Connection string is empty or null.");
            }

            ConnectionString = conn_string;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConnectionString);
            }

        }

        public virtual DbSet<activity> activity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<activity>(entity =>
            {
                entity.ToTable("activity", "crm");

                entity.Property(e => e.name).IsRequired();

                entity.Property(e => e.uid).HasDefaultValueSql("gen_random_uuid()");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
