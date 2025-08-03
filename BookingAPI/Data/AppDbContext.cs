using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pelanggan> Pelanggan { get; set; }
        public DbSet<CabangUsaha> CabangUsaha { get; set; }
        public DbSet<JenisRuanganKaraoke> JenisRuanganKaraoke { get; set; }
        public DbSet<RuanganKaraoke> RuanganKaraoke { get; set; }
        public DbSet<LayananPijat> LayananPijat { get; set; }
        public DbSet<Terapis> Terapis { get; set; }
        public DbSet<PemanduKaraoke> PemanduKaraoke { get; set; }
        public DbSet<PemesananKaraoke> PemesananKaraoke { get; set; }
        public DbSet<PemesananPijat> PemesananPijat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Pemesanan Karaoke
            modelBuilder.Entity<PemesananKaraoke>()
                .HasOne(p => p.Pelanggan)
                .WithMany(pel => pel.PemesananKaraokes)
                .HasForeignKey(p => p.IdPelanggan);

            modelBuilder.Entity<PemesananKaraoke>()
                .HasOne(p => p.RuanganKaraoke)
                .WithMany(r => r.PemesananKaraokes)
                .HasForeignKey(p => p.RuanganKaraokeId);

            modelBuilder.Entity<PemesananKaraoke>()
                .HasOne(p => p.PemanduKaraoke)
                .WithMany(pm => pm.PemesananKaraokes)
                .HasForeignKey(p => p.PemanduKaraokeId)
                .IsRequired(false);

            // Pemesanan Pijat
            modelBuilder.Entity<PemesananPijat>()
                .HasOne(p => p.Pelanggan)
                .WithMany(pel => pel.PemesananPijats)
                .HasForeignKey(p => p.PelangganId);

            modelBuilder.Entity<PemesananPijat>()
                .HasOne(p => p.CabangUsaha)
                .WithMany(c => c.PemesananPijats)
                .HasForeignKey(p => p.CabangUsahaId);

            modelBuilder.Entity<PemesananPijat>()
                .HasOne(p => p.LayananPijat)
                .WithMany(l => l.PemesananPijats)
                .HasForeignKey(p => p.LayananPijatId);

            modelBuilder.Entity<PemesananPijat>()
                .HasOne(p => p.Terapis)
                .WithMany(t => t.PemesananPijats)
                .HasForeignKey(p => p.TerapisId)
                .IsRequired(false);

            // Ruangan Karaoke
            modelBuilder.Entity<RuanganKaraoke>()
                .HasOne(r => r.CabangUsaha)
                .WithMany(c => c.RuanganKaraokes)
                .HasForeignKey(r => r.CabangId);

            modelBuilder.Entity<RuanganKaraoke>()
                .HasOne(r => r.JenisRuanganKaraoke)
                .WithMany(j => j.RuanganKaraokes)
                .HasForeignKey(r => r.JenisRuanganId);
        }
    }
}
