using Microsoft.EntityFrameworkCore;
using RxTracker.Models;
using System;

namespace RxTracker.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Doctor>().HasData(
                    new Doctor
                    {
                        DoctorId = 1,
                        Name = "Bob Pharma",
                        Hospital = "University Hospital",
                        Address = "East Ann Arbor"
                    },
                    new Doctor
                    {

                        DoctorId = 2,
                        Name = "Jane Cuts",
                        Hospital = "VA Hospital",
                        Address = "Traverse City, MI"
                    },
                    new Doctor
                    {

                        DoctorId = 3,
                        Name = "Dr. Strangelove",
                        Address = "Hell, MI"
                    });

            builder.Entity<Drug>().HasData(
                    new Drug
                    {
                        DrugId = 1,
                        Name = "Atorvastatin",
                        TradeName = "Lipitor",
                        Manufacturer = "Pfizer"
                    },
                    new Drug
                    {
                        DrugId = 2,
                        Name = "Atorvastatin",
                        GenericForId = 1
                    },
                    new Drug
                    {
                        DrugId = 3,
                        Name = "Buprenorphine/naloxone",
                        TradeName = "Suboxone",
                        Manufacturer = "Reckitt Benckiser"
                    },
                    new Drug
                    {
                        DrugId = 4,
                        Name = "Pregabalin",
                        TradeName = "Lyrica",
                        Manufacturer = "Pfizer"
                    },
                    new Drug
                    {
                        DrugId = 5,
                        Name = "Buprenorphine/naloxone",
                        GenericForId = 3
                    });

            

            builder.Entity<Pharmacy>().HasData(
                new Pharmacy
                {
                    PharmacyId = 1,
                    Name = "Meijer",
                    Address = "Carpenter Rd, Ypsilanti"
                },
                new Pharmacy
                {
                    PharmacyId = 2,
                    Name = "CVS Caremark",
                    Address = "Mail Order"
                },
                new Pharmacy
                {
                    PharmacyId = 3,
                    Name = "CVS",
                    Address = "Whittaker Rd, Ypsilanti, MI"
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
                    Regimen = "Half strip, twice daily"
                },
                new Prescription
                {
                    PrescriptionId = 2,
                    DoctorId = 3,
                    DrugId = 4,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "60mg",
                    Regimen = "Once daily"
                },
                new Prescription
                {
                    PrescriptionId = 3,
                    DoctorId = 3,
                    DrugId = 5,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "15mg/20mg",
                    Regimen = "Two tablets daily, morning and evening"
                },
                new Prescription
                {
                    PrescriptionId = 4,
                    DoctorId = 2,
                    DrugId = 2,
                    Active = true,
                    Form = "Tablet",
                    Dosage = "100mg",
                    Regimen = "Once daily"
                },
                new Prescription
                {
                    PrescriptionId = 5,
                    DoctorId = 1,
                    DrugId = 1,
                    Active = false,
                    Form = "Capsule",
                    Dosage = "100mg",
                    Regimen = "Once daily"
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
