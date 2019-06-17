using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ordering_API.Models
{
    public partial class OrderingDBContext : DbContext
    {
        public OrderingDBContext()
        {
        }

        public OrderingDBContext(DbContextOptions<OrderingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<DocumentTypes> DocumentTypes { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SalesDocuments> SalesDocuments { get; set; }
        public virtual DbSet<SalesDocumentsProduct> SalesDocumentsProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.ApartmentNumber).HasMaxLength(5);

                entity.Property(e => e.BuildingNumber)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Province).HasMaxLength(20);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasColumnName("ZIPCode")
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64B864DF3C47");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Email).HasMaxLength(70);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DocumentTypes>(entity =>
            {
                entity.HasKey(e => e.DocumentTypeId)
                    .HasName("PK__Document__DBA390C14EDB1540");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<SalesDocuments>(entity =>
            {
                entity.HasKey(e => e.SalesDocumentId)
                    .HasName("PK__SalesDoc__EC345F70632D674A");

                entity.Property(e => e.SalesDocumentId).HasColumnName("SalesDocumentID");

                entity.Property(e => e.BasketId).HasColumnName("BasketID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.SalesDocuments)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("FKSalesDocum547221");

                entity.HasOne(d => d.CustomerAddressNavigation)
                    .WithMany(p => p.SalesDocuments)
                    .HasForeignKey(d => d.CustomerAddress)
                    .HasConstraintName("FKSalesDocum258221");

                entity.HasOne(d => d.DocumentTypesNavigation)
                    .WithMany(p => p.SalesDocuments)
                    .HasForeignKey(d => d.DocumentTypes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSalesDocum525243");
            });

            modelBuilder.Entity<SalesDocumentsProduct>(entity =>
            {
                entity.HasKey(e => new { e.SalesDocuments, e.Product })
                    .HasName("PK__SalesDoc__8E78FB4220FC77A1");

                entity.ToTable("SalesDocuments_Product");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.SalesDocumentsProduct)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSalesDocum858755");

                entity.HasOne(d => d.SalesDocumentsNavigation)
                    .WithMany(p => p.SalesDocumentsProduct)
                    .HasForeignKey(d => d.SalesDocuments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSalesDocum547789");
            });
        }
    }
}
