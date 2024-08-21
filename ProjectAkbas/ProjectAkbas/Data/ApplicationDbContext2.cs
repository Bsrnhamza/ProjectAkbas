using Microsoft.EntityFrameworkCore;
using ProjectAkbas.Models;

namespace ProjectAkbas.Data
{
    public class ApplicationDbContext2 : DbContext
    {
        public ApplicationDbContext2(DbContextOptions<ApplicationDbContext2> options)
            : base(options)
        {
        }

        public DbSet<mProformaMaliyet> ProformaMaliyetler { get; set; }
        public DbSet<mUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mProformaMaliyet>()
              .ToTable("ProformaMaliyet") 
        .HasKey(p => p.IDD);
            modelBuilder.Entity<mUser>()
            .ToTable("Users")
      .HasKey(p => p.Id);
        }
    }
}
