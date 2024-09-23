﻿// <auto-generated />
using System;
using ConferenceRoomsWebAPI.ApplicationDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConferenceRoomsWebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240921161135_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.Booking", b =>
                {
                    b.Property<int>("IdBooking")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_booking");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdBooking"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("booking_date");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("interval")
                        .HasColumnName("end_time");

                    b.Property<int>("IdConferenceRoom")
                        .HasColumnType("integer")
                        .HasColumnName("id_conference_room");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("interval")
                        .HasColumnName("start_time");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total_price");

                    b.HasKey("IdBooking")
                        .HasName("pk_bookings");

                    b.HasIndex("IdConferenceRoom")
                        .HasDatabaseName("ix_bookings_id_conference_room");

                    b.ToTable("bookings", (string)null);
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.BookingCompanyService", b =>
                {
                    b.Property<int>("IdCompanyService")
                        .HasColumnType("integer")
                        .HasColumnName("id_company_service");

                    b.Property<int>("IdBooking")
                        .HasColumnType("integer")
                        .HasColumnName("id_booking");

                    b.HasKey("IdCompanyService", "IdBooking")
                        .HasName("pk_booking_company_service");

                    b.HasIndex("IdBooking")
                        .HasDatabaseName("ix_booking_company_service_id_booking");

                    b.ToTable("booking_company_service", (string)null);
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.CompanyServices", b =>
                {
                    b.Property<int>("IdService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_service");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdService"));

                    b.Property<int?>("ConferenceRoomsIdRoom")
                        .HasColumnType("integer")
                        .HasColumnName("conference_rooms_id_room");

                    b.Property<double>("PriceService")
                        .HasColumnType("double precision")
                        .HasColumnName("price_service");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("service_name");

                    b.HasKey("IdService")
                        .HasName("pk_company_services");

                    b.HasIndex("ConferenceRoomsIdRoom")
                        .HasDatabaseName("ix_company_services_conference_rooms_id_room");

                    b.ToTable("company_services", (string)null);
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.ConferenceRooms", b =>
                {
                    b.Property<int>("IdRoom")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_room");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdRoom"));

                    b.Property<decimal>("BasePricePerHour")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("base_price_per_hour");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer")
                        .HasColumnName("capacity");

                    b.Property<string>("NameRoom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name_room");

                    b.HasKey("IdRoom")
                        .HasName("pk_conference_rooms");

                    b.HasIndex("NameRoom")
                        .IsUnique()
                        .HasDatabaseName("ix_conference_rooms_name_room");

                    b.ToTable("conference_rooms", (string)null);
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.Booking", b =>
                {
                    b.HasOne("ConferenceRoomsWebAPI.Entity.ConferenceRooms", "ConferenceRooms")
                        .WithMany("Bookings")
                        .HasForeignKey("IdConferenceRoom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_bookings_conference_rooms_id_conference_room");

                    b.Navigation("ConferenceRooms");
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.BookingCompanyService", b =>
                {
                    b.HasOne("ConferenceRoomsWebAPI.Entity.Booking", "Booking")
                        .WithMany("BookingCompanyServices")
                        .HasForeignKey("IdBooking")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_booking_company_service_bookings_id_booking");

                    b.HasOne("ConferenceRoomsWebAPI.Entity.CompanyServices", "CompanyServices")
                        .WithMany("BookingCompanyServices")
                        .HasForeignKey("IdCompanyService")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_booking_company_service_company_services_id_company_service");

                    b.Navigation("Booking");

                    b.Navigation("CompanyServices");
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.CompanyServices", b =>
                {
                    b.HasOne("ConferenceRoomsWebAPI.Entity.ConferenceRooms", null)
                        .WithMany("CompanyServices")
                        .HasForeignKey("ConferenceRoomsIdRoom")
                        .HasConstraintName("fk_company_services_conference_rooms_conference_rooms_id_room");
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.Booking", b =>
                {
                    b.Navigation("BookingCompanyServices");
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.CompanyServices", b =>
                {
                    b.Navigation("BookingCompanyServices");
                });

            modelBuilder.Entity("ConferenceRoomsWebAPI.Entity.ConferenceRooms", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("CompanyServices");
                });
#pragma warning restore 612, 618
        }
    }
}
