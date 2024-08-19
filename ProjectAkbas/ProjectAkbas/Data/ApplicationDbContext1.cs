using Microsoft.EntityFrameworkCore;
using projectakbas.models;
using ProjectAkbas.Models;

namespace ProjectAkbas.Data
{
    public class ApplicationDbContext1 : DbContext
    {
        public ApplicationDbContext1(DbContextOptions<ApplicationDbContext1> options)
            : base(options)
        {
        }

        // DbSet'leri burada tanımlıyoruz
        public DbSet<mGrupAdlari> GrupAdlari { get; set; }
        public DbSet<mSapIthalatVerileri> SapIthalatVerileri { get; set; }
        public DbSet<mSapGuncelStokVerileri> SapGuncelStokVerileri { get; set; }

        // Fluent API veya konfigürasyon için OnModelCreating metodunu kullanabiliriz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // mGrupAdlari konfigürasyonu
            modelBuilder.Entity<mGrupAdlari>()
                .ToTable("GrupAdları")
                .HasKey(g => new { g.KumasKodu });

            // SapIthalatVerileri konfigürasyonu
            modelBuilder.Entity<mSapIthalatVerileri>()
                .ToTable("SapIthalatVerileri")
                .HasKey(si => si.Kumas);

            // SapGuncelStokVerileri konfigürasyonu
            modelBuilder.Entity<mSapGuncelStokVerileri>()
                .ToTable("SapGuncelStokVerileri")
              .HasNoKey();
        }
    }
}