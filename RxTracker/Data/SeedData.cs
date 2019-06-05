using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RxTracker.Models;
using System;

namespace RxTracker.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder builder)
        {
            var userId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<MyUser>();
            builder.Entity<MyUser>().HasData(new MyUser
            {
                Id = userId,
                UserName = "jane@example.com",
                NormalizedUserName = "JANE@EXAMPLE.COM",
                Email = "jane@example.com",
                NormalizedEmail = "JANE@EXAMPLE.COM",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "jane"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "734-555-1234",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                DOB = new DateTime(1972, 6, 24),
                FirstName = "Jane",
                LastName = "Doe"
            });

            builder.Entity<Doctor>().HasData(
                    new Doctor
                    {
                        DoctorId = 1,
                        Name = "Bob Pharma",
                        Hospital = "University Hospital",
                        Address = "East Ann Arbor",
                        UserId = userId
                    },
                    new Doctor
                    {
                        DoctorId = 2,
                        Name = "Mary Cutz",
                        Hospital = "VA Hospital",
                        Address = "Traverse City, MI",
                        UserId = userId
                    },
                    new Doctor
                    {
                        DoctorId = 3,
                        Name = "Dr. Feelgood",
                        Address = "Hell, MI",
                        UserId = userId
                    },
                    new Doctor
                    {
                        DoctorId = 4,
                        Name = "Douglas Adams",
                        Address = "Sirius Beta",
                        UserId = userId
                    });

            builder.Entity<Drug>().HasData(
                    new Drug
                    {
                        DrugId = 1,
                        Name = "Atorvastatin",
                        TradeName = "Lipitor",
                        Manufacturer = "Pfizer",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 2,
                        Name = "Atorvastatin",
                        GenericForId = 1,
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 3,
                        Name = "Buprenorphine/naloxone",
                        TradeName = "Suboxone",
                        Manufacturer = "Reckitt Benckiser",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 4,
                        Name = "Pregabalin",
                        TradeName = "Lyrica",
                        Manufacturer = "Pfizer",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 5,
                        Name = "Buprenorphine/naloxone",
                        GenericForId = 3,
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 6,
                        Name = "Methylphenidate",
                        TradeName ="Ritalin",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 7,
                        Name = "Methylphenidate",
                        GenericForId = 6,
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 8,
                        Name = "Beclomethasone",
                        TradeName = "QVAR",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 9,
                        Name = "Estadiol",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 10,
                        Name = "Fluoxetine",
                        TradeName = "Prozac",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 11,
                        Name = "Fluoxetine",
                        GenericForId = 10,
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 12,
                        Name = "Lisonopril HCL",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 13,
                        Name = "Trazodone",
                        UserId = userId
                    },
                    new Drug
                    {
                        DrugId = 14,
                        Name = "Oxybutynin",
                        UserId = userId
                    });

            builder.Entity<Pharmacy>().HasData(
                new Pharmacy
                {
                    PharmacyId = 1,
                    Name = "Meijer",
                    Address = "Carpenter Rd, Ypsilanti",
                    UserId = userId
                },
                new Pharmacy
                {
                    PharmacyId = 2,
                    Name = "CVS Caremark",
                    Address = "Mail Order",
                    UserId = userId
                },
                new Pharmacy
                {
                    PharmacyId = 3,
                    Name = "CVS",
                    Address = "Whittaker Rd, Ypsilanti, MI",
                    UserId = userId
                },
                new Pharmacy
                {
                    PharmacyId = 4,
                    Name = "Kroger",
                    Address = "Whittaker Rd, Ypsilanti, MI",
                    UserId = userId
                },
                new Pharmacy
                {
                    PharmacyId = 5,
                    Name = "Costco",
                    Address = "Ellwworth Rd, Pittsfield, MI",
                    UserId = userId
                });

            builder.Entity<Prescription>().HasData(
                new Prescription
                {
                    PrescriptionId = 1,
                    DoctorId = 1,
                    DrugId = 2,
                    Active = true,
                    Form = "Sublingual Strip",
                    Dosage = "10mg/12.5mg",
                    Regimen = "Half strip, twice daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 2,
                    DoctorId = 3,
                    DrugId = 4,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "60mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 3,
                    DoctorId = 3,
                    DrugId = 5,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "15mg/20mg",
                    Regimen = "Two tablets daily, morning and evening",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 4,
                    DoctorId = 2,
                    DrugId = 2,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "100mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 5,
                    DoctorId = 1,
                    DrugId = 1,
                    Active = true,
                    Form = "Capsule",
                    Dosage = "100mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 6,
                    DoctorId = 1,
                    DrugId = 8,
                    Active = false,
                    Form = "Inhaler",
                    Dosage = "60mcg",
                    Regimen = "Two puffs, twice daily or as needed",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 7,
                    DoctorId = 1,
                    DrugId = 9,
                    Active = true,
                    Form = "Patch",
                    Dosage = "0.1mg",
                    Regimen = "Once weekly",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 8,
                    DoctorId = 3,
                    DrugId = 11,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "50mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 9,
                    DoctorId = 1,
                    DrugId = 12,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "10-12.5mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 10,
                    DoctorId = 1,
                    DrugId = 13,
                    Active = true,
                    Form = "Capsule",
                    Dosage = "100mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 11,
                    DoctorId = 3,
                    DrugId = 6,
                    Active = true,
                    Form = "Capsule",
                    Dosage = "100mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 12,
                    DoctorId = 3,
                    DrugId = 7,
                    Active = true,
                    Form = "Capsule",
                    Dosage = "100mg",
                    Regimen = "Once daily",
                    UserId = userId
                },
                new Prescription
                {
                    PrescriptionId = 13,
                    DoctorId = 3,
                    DrugId = 10,
                    Active = true,
                    Form = "Capsule",
                    Dosage = "100mg",
                    Regimen = "Once daily",
                    UserId = userId
                });

            builder.Entity<Transaction>().HasData(
                new Transaction
                {
                    TransactionId = 1,
                    PrescriptionId = 1,
                    PharmacyId = 2,
                    DateFilled = new DateTime(2019, 1, 5),
                    Cost = 20,
                    InsuranceUsed = "BCBS"
                },
                new Transaction
                {
                    TransactionId = 2,
                    PrescriptionId = 1,
                    PharmacyId = 3,
                    DateFilled = new DateTime(2019, 2, 15),
                    Cost = 25,
                    InsuranceUsed = "BCBS"
                },
                new Transaction
                {
                    TransactionId = 3,
                    PrescriptionId = 1,
                    PharmacyId = 2,
                    DateFilled = new DateTime(2019, 3, 9),
                    Cost = 20,
                    InsuranceUsed = "BCBS"
                },
                new Transaction
                {
                    TransactionId = 4,
                    PrescriptionId = 5,
                    PharmacyId = 1,
                    DateFilled = new DateTime(2018, 10, 21),
                    Cost = 50,
                    InsuranceUsed = "Aetna",
                    DiscountUsed = "GoodRx"
                },
                new Transaction
                {
                    TransactionId = 5,
                    PrescriptionId = 2,
                    PharmacyId = 1,
                    DateFilled = new DateTime(2019, 3, 15),
                    Cost = 32.99M,
                    InsuranceUsed = "Molina",
                    DiscountUsed = "Manufacturer's Coupon"
                },
                new Transaction
                {
                    TransactionId = 6,
                    PrescriptionId = 2,
                    PharmacyId = 1,
                    DateFilled = new DateTime(2019, 4, 10),
                    Cost = 30.65M,
                    InsuranceUsed = "Molina",
                    DiscountUsed = "Manufacturer's Coupon"
                },
                new Transaction
                {
                    TransactionId = 7,
                    PrescriptionId = 3,
                    PharmacyId = 3,
                    DateFilled = new DateTime(2019, 4, 23),
                    Cost = 41.82M,
                    InsuranceUsed = "Molina"
                },
                new Transaction
                {
                    TransactionId = 8,
                    PrescriptionId = 3,
                    PharmacyId = 2,
                    DateFilled = new DateTime(2019, 5, 31),
                    Cost = 26.62M,
                    InsuranceUsed = "Molina"
                },
                new Transaction
                {
                    TransactionId = 9,
                    PrescriptionId = 4,
                    PharmacyId = 2,
                    DateFilled = new DateTime(2019, 3, 15),
                    Cost = 12.55M,
                    DiscountUsed = "GoodRx Gold"
                },
                new Transaction
                {
                    TransactionId = 10,
                    PrescriptionId = 4,
                    PharmacyId = 1,
                    DateFilled = new DateTime(2019, 3, 15),
                    Cost = 9.82M,
                    InsuranceUsed = "Molina"
                });
        }
    }
}
