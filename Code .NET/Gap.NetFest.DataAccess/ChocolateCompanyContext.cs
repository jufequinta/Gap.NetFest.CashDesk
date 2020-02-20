using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gap.NetFest.DataAccess.DTO;

namespace Gap.NetFest.DataAccess
{
    public class ChocolateCompanyContext : DbContext
    {
        public virtual DbSet<Chocolates_Brands> Chocolates_Brands { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Invoice_Details> Invoice_Details { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Pay_Methods> Pay_Methods { get; set; }
        public virtual DbSet<Providers> Providers { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Type_Notifications> Type_Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=tcp:testquintanadatabase.database.windows.net,1433;Initial Catalog=chocolatecompany;Persist Security Info=False;User ID=jufequinta;Password=Byacuya2012.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chocolates_Brands>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Price);

                entity.HasOne(d => d.Provider)
                     .WithMany(p => p.Chocolates)
                     .HasForeignKey(d => d.IdProvider)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_Chocolates_Brands_Providers");

            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PosMachineAmount);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pay_Methods>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date);

                entity.Property(e => e.IdCustomer);

                entity.Property(e => e.NameMachinePos)
                    .HasMaxLength(150)
                    .IsUnicode(false);


                entity.HasOne(d => d.Customer)
                     .WithMany(p => p.Invoices)
                     .HasForeignKey(d => d.IdCustomer)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_Invoices_Customers");


            });

            modelBuilder.Entity<Invoice_Details>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdInvoice);

                entity.Property(e => e.IdChocolate_brand);

                entity.Property(e => e.IdPymethod);

                entity.Property(e => e.Quantity);

                entity.HasOne(d => d.Invoice)
                     .WithMany(p => p.InvoiceDetails)
                     .HasForeignKey(d => d.IdInvoice)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_Invoice_Details_Invoices");

                entity.HasOne(d => d.PayMethod)
                     .WithMany(p => p.InvoiceDetails)
                     .HasForeignKey(d => d.IdPymethod)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_Invoice_Details_Pay_Methods");

                entity.HasOne(d => d.Chocolate)
                     .WithMany(p => p.InvoiceDetails)
                     .HasForeignKey(d => d.IdChocolate_brand)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_Invoice_Details_Chocolates_Brands");
            });
        }
    }
}
