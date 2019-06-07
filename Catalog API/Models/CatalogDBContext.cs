using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Catalog_API.Models
{
    public partial class CatalogDBContext : DbContext
    {
        public CatalogDBContext()
        {
        }

        public CatalogDBContext(DbContextOptions<CatalogDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producer> Producer { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsWarehouses> ProductsWarehouses { get; set; }
        public virtual DbSet<Warehouses> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=lwdev.database.windows.net;Database=CatalogDB;User Id=Ygg_;Password=babuszka2400!;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.ProducerId).HasColumnName("ProducerID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ProductTypes>(entity =>
            {
                entity.HasKey(e => e.ProductTypeId)
                    .HasName("PK__ProductT__A1312F4EF22396B4");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6ED1AC6F669");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DescriptionImage)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Ean)
                    .IsRequired()
                    .HasColumnName("EAN")
                    .HasMaxLength(13);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.ProducerNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Producer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProducts692437");

                entity.HasOne(d => d.ProductTypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProducts450562");
            });

            modelBuilder.Entity<ProductsWarehouses>(entity =>
            {
                entity.HasKey(e => new { e.Warehouses, e.Products })
                    .HasName("PK__Products__59F50C68B296C171");

                entity.ToTable("Products_Warehouses");

                entity.HasOne(d => d.ProductsNavigation)
                    .WithMany(p => p.ProductsWarehouses)
                    .HasForeignKey(d => d.Products)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProducts_W819913");

                entity.HasOne(d => d.WarehousesNavigation)
                    .WithMany(p => p.ProductsWarehouses)
                    .HasForeignKey(d => d.Warehouses)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProducts_W716494");
            });

            modelBuilder.Entity<Warehouses>(entity =>
            {
                entity.HasKey(e => e.WarehouseId)
                    .HasName("PK__Warehous__2608AFD912A3277E");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(4);
            });
        }
    }
}
