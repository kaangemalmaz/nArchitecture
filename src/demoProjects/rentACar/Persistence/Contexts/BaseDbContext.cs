using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(a =>
            {
                a.ToTable("Brands").HasKey(b => b.Id);
                a.Property(b => b.Id).UseIdentityColumn();
                a.Property(b => b.Id).HasColumnName("Id");
                a.Property(b => b.Name).HasColumnName("Name");
                a.HasMany(b => b.Models);
            });


            modelBuilder.Entity<Model>(a =>
            {
                a.ToTable("Models").HasKey(b => b.Id);
                a.Property(m => m.Id).UseIdentityColumn();
                a.Property(m => m.Id).HasColumnName("Id");
                a.Property(m => m.BrandId).HasColumnName("BrandId");
                a.Property(m => m.Name).HasColumnName("Name");
                a.Property(m => m.DailyPrice).HasColumnName("DailyPrice");
                a.Property(m => m.ImageUrl).HasColumnName("ImageUrl");
                a.HasOne(m => m.Brand);
            });


            Brand[] brandSeedData = { new(1, "BMW"), new(2, "Mercedes") };
            modelBuilder.Entity<Brand>().HasData(brandSeedData);


            Model[] modelEntitySeeds = { new(1, 1, "Series 4", 1500, ""), new(2, 1, "Series 3", 1200, ""), new(3, 2, "A180", 1000, "") };
            modelBuilder.Entity<Model>().HasData(modelEntitySeeds);


        }
    }
}
