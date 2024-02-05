﻿// <auto-generated />
using System;
using Assign1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assign1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240205041702_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Assign1.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Assign1.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AirlineName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ArrivalPort")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("AvailSeats")
                        .HasColumnType("int");

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("DeparturePort")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FlightDuration")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TicketCost")
                        .HasColumnType("int");

                    b.HasKey("FlightId");

                    b.HasIndex("BookingId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Assign1.Models.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("CancellationPolicy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CheckInTime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CheckOutTime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CostPerNight")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MaxOccupancy")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StarRating")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("HotelId");

                    b.HasIndex("BookingId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Assign1.Models.Rental", b =>
                {
                    b.Property<int>("RentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AdditionalDriverOption")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Availability")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<bool>("CarInsurance")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("CostPerNight")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DriverAge")
                        .HasColumnType("int");

                    b.Property<string>("DropoffLocation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("GPSIncluded")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("longtext");

                    b.Property<string>("MakeModel")
                        .HasColumnType("longtext");

                    b.Property<double>("Mileage")
                        .HasColumnType("double");

                    b.Property<string>("PickupLocation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RentalCost")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("RentalId");

                    b.HasIndex("BookingID");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Assign1.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Assign1.Models.Booking", b =>
                {
                    b.HasOne("Assign1.Models.User", null)
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assign1.Models.Flight", b =>
                {
                    b.HasOne("Assign1.Models.Booking", null)
                        .WithMany("Flights")
                        .HasForeignKey("BookingId");
                });

            modelBuilder.Entity("Assign1.Models.Hotel", b =>
                {
                    b.HasOne("Assign1.Models.Booking", null)
                        .WithMany("Hotels")
                        .HasForeignKey("BookingId");
                });

            modelBuilder.Entity("Assign1.Models.Rental", b =>
                {
                    b.HasOne("Assign1.Models.Booking", null)
                        .WithMany("Rentals")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assign1.Models.Booking", b =>
                {
                    b.Navigation("Flights");

                    b.Navigation("Hotels");

                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("Assign1.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
