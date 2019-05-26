using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RxTracker.Models;

namespace RxTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Pharmacy> Pharmacy { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.Hospital)
                    .HasMaxLength(80);
                entity.Property(e => e.Address)
                    .HasMaxLength(256);
            });

            builder.Entity<Drug>(entity =>
            {
                entity.HasKey(d => d.DrugId);

                entity.HasOne(d => d.GenericFor)
                    .WithMany()
                    .HasForeignKey(e => e.GenericForId);
                    
                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsRequired();
                entity.Property(e => e.TradeName)
                    .HasMaxLength(80);
                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(80);
            });

            builder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.PatientId);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(80)
                    .IsRequired();
                entity.Property(e => e.LastName)
                    .HasMaxLength(80)
                    .IsRequired();
                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(80);
                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsRequired();

            });

            builder.Entity<Pharmacy>(entity =>
            {
                entity.HasKey(p => p.PharmacyId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.Address)
                    .HasMaxLength(256);

            });

            builder.Entity<Prescription>(entity =>
            {
                entity.HasKey(p => p.PrescriptionId);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.DoctorId);

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.DrugId);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.PatientId);

                entity.Property(e => e.Active)
                    .HasColumnType("tinyint");

                entity.Property(e => e.Form)
                    .HasMaxLength(80);
                entity.Property(e => e.Dosage)
                    .HasMaxLength(80);
                entity.Property(e => e.Regimen)
                    .HasMaxLength(80);
            });

            builder.Entity<Transaction>(entity =>
            {
                entity.HasKey(p => p.TransactionId);

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PrescriptionId);

                entity.HasOne(d => d.Pharmacy)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PharmacyId);

                entity.Property(e => e.DateFilled)
                    .HasColumnType("datetime");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(10,2)");
                entity.Property(e => e.InsuranceUsed)
                    .HasMaxLength(80);
                entity.Property(e => e.DiscountUsed)
                    .HasMaxLength(80);
            });

            builder.Seed();
        }
    }
}
