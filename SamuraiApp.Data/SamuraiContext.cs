using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using System;

namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Horse> Horses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=SamuraiAppData");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles)
                .WithMany(s => s.Samurais)
                .UsingEntity<SamuraiBattle>(bs => bs.HasOne<Battle>().WithMany(), bs => bs.HasOne<Samurai>().WithMany())
                .Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getDate()");
        }


    }
}
