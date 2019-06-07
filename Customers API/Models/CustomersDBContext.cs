using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Customers_API.Models
{
    public partial class CustomersDBContext : DbContext
    {
        public CustomersDBContext()
        {
        }

        public CustomersDBContext(DbContextOptions<CustomersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<CustomerCustomerAddress> CustomerCustomerAddress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local);Database=CustomersDB;User Id=SA;Password=babuszka2400!;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

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

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.Property(e => e.CustomerAddressId).HasColumnName("CustomerAddressID");

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

            modelBuilder.Entity<CustomerCustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.Customer, e.CustomerAddress })
                    .HasName("PK__Customer__B00D1EA0E0989061");

                entity.ToTable("Customer_CustomerAddress");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.CustomerCustomerAddress)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCustomer_C290024");

                entity.HasOne(d => d.CustomerAddressNavigation)
                    .WithMany(p => p.CustomerCustomerAddress)
                    .HasForeignKey(d => d.CustomerAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCustomer_C565799");
            });
        }
    }
}
