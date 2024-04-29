﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public partial class ViaEventAssociationContext : DbContext
{
    public ViaEventAssociationContext()
    {
    }

    public ViaEventAssociationContext(DbContextOptions<ViaEventAssociationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Creator> Creators { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=D:\\VIA\\Semester6\\DCA1\\NBNP-DCA\\NBNP-DCA\\VIAEventAssociation\\ViaEventAssociation");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasIndex(e => e.LocationId, "IX_Events_locationId");

            entity.Property(e => e.LocationId).HasColumnName("locationId");

            entity.HasOne(d => d.Location).WithMany(p => p.Events).HasForeignKey(d => d.LocationId);

            entity.HasMany(d => d.Guests).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventGuest",
                    r => r.HasOne<Guest>().WithMany().HasForeignKey("GuestsId"),
                    l => l.HasOne<Event>().WithMany().HasForeignKey("EventsId"),
                    j =>
                    {
                        j.HasKey("EventsId", "GuestsId");
                        j.ToTable("EventGuests");
                        j.HasIndex(new[] { "GuestsId" }, "IX_EventGuests_GuestsId");
                    });
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.Property(e => e.ProfilePicUrl).HasColumnName("ProfilePicURL");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.ToTable("Invitation");

            entity.HasIndex(e => e.EventId, "IX_Invitation_EventId");

            entity.HasIndex(e => e.GuestId, "IX_Invitation_GuestId");

            entity.HasOne(d => d.Event).WithMany(p => p.Invitations).HasForeignKey(d => d.EventId);

            entity.HasOne(d => d.Guest).WithMany(p => p.Invitations).HasForeignKey(d => d.GuestId);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.HasIndex(e => e.EventId, "IX_Request_EventId");

            entity.HasIndex(e => e.GuestId, "IX_Request_GuestId");

            entity.HasOne(d => d.Event).WithMany(p => p.Requests).HasForeignKey(d => d.EventId);

            entity.HasOne(d => d.Guest).WithMany(p => p.Requests).HasForeignKey(d => d.GuestId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}