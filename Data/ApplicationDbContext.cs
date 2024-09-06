using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestIdentityApp.Data.Models;
using TestIdentityApp.Data.Repository;
using Ders = TestIdentityApp.Data.Models.Ders;
using Ödev = TestIdentityApp.Data.Models.Ödev;
using Öğrenci = TestIdentityApp.Data.Models.Öğrenci;
using Öğretmen = TestIdentityApp.Data.Models.Öğretmen;
using YıldızÖğrenci = TestIdentityApp.Models.YıldızÖğrenci;

namespace TestIdentityApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Öğrenci> Öğrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Öğretmen> Öğretmenler { get; set; }
        public DbSet<YıldızÖğrenci> YıldızÖğrenciler { get; set; }
        public DbSet<Veli> Veliler { get; set; }

        public DbSet<Ödev> Ödevler { get; set; }
        public DbSet<HikayeÖzeti> HikayeÖzetleri { get; set; }
        public DbSet<DersIcerik> DersIcerikler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Öğretmen (Teacher)



            // Seed data for Ders (Lesson)
            modelBuilder.Entity<Ders>().HasData(
                new Ders
                {
                    DersId = 1, DersAdi = "Matematik", Günler = DaysOfWeek.Pazartesi | DaysOfWeek.Salı,
                    ÖğretmenId = 1
                },
                new Ders
                {
                    DersId = 2, DersAdi = "Türkçe", Günler = DaysOfWeek.Pazartesi | DaysOfWeek.Çarşamba,
                    ÖğretmenId = 1
                },
                new Ders
                {
                    DersId = 3, DersAdi = "Sosyal Bilgiler", Günler = DaysOfWeek.Salı | DaysOfWeek.Çarşamba,
                    ÖğretmenId = 1
                },
                new Ders
                {
                    DersId = 4, DersAdi = "Fen Bilimleri", Günler = DaysOfWeek.Perşembe | DaysOfWeek.Cuma,
                    ÖğretmenId = 1
                },
                new Ders
                {
                    DersId = 5, DersAdi = "Din Kültürü ve Ahlak Bilgisi",
                    Günler = DaysOfWeek.Pazartesi | DaysOfWeek.Salı, ÖğretmenId = 1
                },
                new Ders
                {
                    DersId = 6, DersAdi = "İngilizce", Günler = DaysOfWeek.Salı | DaysOfWeek.Cuma, ÖğretmenId = 1
                },
                new Ders { DersId = 7, DersAdi = "Müzik", Günler = DaysOfWeek.Pazartesi, ÖğretmenId = 1 },
                new Ders { DersId = 8, DersAdi = "Resim", Günler = DaysOfWeek.Salı, ÖğretmenId = 1 },
                new Ders { DersId = 9, DersAdi = "Beden Eğitimi", Günler = DaysOfWeek.Çarşamba, ÖğretmenId = 1 },
                new Ders
                {
                    DersId = 10, DersAdi = "Trafik Güvenliği", Günler = DaysOfWeek.Perşembe, ÖğretmenId = 1
                },
                new Ders
                {
                    DersId = 11, DersAdi = "Yurttaşlık ve Demokrasi", Günler = DaysOfWeek.Salı, ÖğretmenId = 1
                }
            );
            modelBuilder.Entity<Ödev>().HasData(

                new Ödev {Id = 1 ,Başlık = "Öykü", DersId = 1, İçerik = "oyku" }

            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
    

