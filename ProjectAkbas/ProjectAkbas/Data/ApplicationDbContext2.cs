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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mProformaMaliyet>()
              .ToTable("ProformaMaliyet") // Tablo adının doğru olduğundan emin olun
        .HasKey(p => p.IDD); // IDD'yi birincil anahtar olarak belirleyin
        }
    }
}
