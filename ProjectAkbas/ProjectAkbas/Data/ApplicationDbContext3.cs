using Microsoft.EntityFrameworkCore;
using ProjectAkbas.Models;

namespace ProjectAkbas.Data
{
    public class ApplicationDbContext3 : DbContext
    {
        public ApplicationDbContext3(DbContextOptions<ApplicationDbContext3> options)
            : base(options)
        {
        }

        public DbSet<mMara> Maras { get; set; }
        public DbSet<mMard> mMards { get; set; }
        public DbSet<ZmmGrupT> ZmmGrupTs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<mMara>(entity =>
            {
                entity.HasNoKey(); // Keyless (anahtarsız) olarak işaretleyin
                entity.ToTable("MARA", "aep");
            });

            modelBuilder.Entity<mMard>(entity =>
            {
                entity.HasNoKey() // Keyless (anahtarsız) olarak işaretleyin
                    .HasKey(md => md.mardMATNR);
            });

            modelBuilder.Entity<ZmmGrupT>(entity =>
            {
                entity.HasKey(zm => zm.zmGrup); // Keyless (anahtarsız) olarak işaretleyin
                entity.ToTable("ZMM_GRUP_T", "aep"); // Tablo adı ve şema belirtildi
            });
        }
    }
}
