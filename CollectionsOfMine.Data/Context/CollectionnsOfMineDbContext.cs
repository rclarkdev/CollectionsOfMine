using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CollectionsOfMine.Data.Models;
using Attribute = CollectionsOfMine.Data.Models.Attribute;
using File = CollectionsOfMine.Data.Models.File;

namespace CollectionsOfMine.Data.Context
{
    public partial class CollectionnsOfMineDbContext : DbContext
    {
        public CollectionnsOfMineDbContext()
        {
        }

        public CollectionnsOfMineDbContext(DbContextOptions<CollectionnsOfMineDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration
                    .GetConnectionString("LocalSqlServer");

                optionsBuilder
                    .UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<Attribute>()
                .HasMany(p => p.Items)
                .WithMany(p => p.Attributes)
                .UsingEntity(j => j.ToTable("ItemAttributes"));

            modelBuilder
                .Entity<Category>()
                .HasMany(p => p.Items)
                .WithMany(p => p.Categories)
                .UsingEntity(j => j.ToTable("ItemCategories"));

            modelBuilder
                .Entity<File>()
                .HasMany(p => p.Items)
                .WithMany(p => p.Files)
                .UsingEntity(j => j.ToTable("ItemFiles"));

            modelBuilder
               .Entity<Collection>()
               .HasMany(p => p.ContentTypes)
               .WithMany(p => p.Collections)
               .UsingEntity(j => j.ToTable("CollectionContentTypes"));

        }
    }
}