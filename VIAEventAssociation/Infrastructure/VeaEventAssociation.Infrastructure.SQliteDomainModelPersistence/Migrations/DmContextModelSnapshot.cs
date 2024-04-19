﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

#nullable disable

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.Migrations
{
    [DbContext(typeof(DmContext))]
    partial class DmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4");

            modelBuilder.Entity("Domain.Aggregates.Creator.Creator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Creators");
                });

            modelBuilder.Entity("Domain.Aggregates.Events.Event", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("end_date_time")
                        .HasColumnType("TEXT");

                    b.Property<int>("locationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("max_guests")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("start_date_time")
                        .HasColumnType("TEXT");

                    b.Property<int>("status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("visibility")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("locationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Domain.Aggregates.Guests.Guest", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Eventid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("eventId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("Eventid");

                    b.HasIndex("eventId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Domain.Aggregates.Locations.Location", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("maxCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Domain.Common.Entities.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Guestid")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("eventId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Guestid");

                    b.HasIndex("eventId");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("Domain.Common.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Guestid")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("eventId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Guestid");

                    b.HasIndex("eventId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("Domain.Aggregates.Events.Event", b =>
                {
                    b.HasOne("Domain.Aggregates.Locations.Location", "location")
                        .WithMany()
                        .HasForeignKey("locationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("Domain.Aggregates.Guests.Guest", b =>
                {
                    b.HasOne("Domain.Aggregates.Events.Event", null)
                        .WithMany("Guests")
                        .HasForeignKey("Eventid");

                    b.HasOne("Domain.Aggregates.Events.Event", null)
                        .WithMany("guests")
                        .HasForeignKey("eventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Common.Entities.Invitation", b =>
                {
                    b.HasOne("Domain.Aggregates.Guests.Guest", null)
                        .WithMany("invitations")
                        .HasForeignKey("Guestid");

                    b.HasOne("Domain.Aggregates.Events.Event", null)
                        .WithMany("invitations")
                        .HasForeignKey("eventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Common.Entities.Request", b =>
                {
                    b.HasOne("Domain.Aggregates.Guests.Guest", null)
                        .WithMany("requests")
                        .HasForeignKey("Guestid");

                    b.HasOne("Domain.Aggregates.Events.Event", null)
                        .WithMany("requests")
                        .HasForeignKey("eventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Aggregates.Events.Event", b =>
                {
                    b.Navigation("Guests");

                    b.Navigation("guests");

                    b.Navigation("invitations");

                    b.Navigation("requests");
                });

            modelBuilder.Entity("Domain.Aggregates.Guests.Guest", b =>
                {
                    b.Navigation("invitations");

                    b.Navigation("requests");
                });
#pragma warning restore 612, 618
        }
    }
}
