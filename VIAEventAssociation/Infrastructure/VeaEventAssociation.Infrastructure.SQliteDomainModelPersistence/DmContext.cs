using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public class DmContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Creator> Creators => Set<Creator>();
    
    // public DbSet<Invitation> Invitations => Set<Invitation>();
    // public DbSet<Request> Requests => Set<Request>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(DmContext).Assembly);
        ConfigureEvent(modelBuilder.Entity<Event>());
        ConfigureGuest(modelBuilder.Entity<Guest>());
        ConfigureLocation(modelBuilder.Entity<Location>());
        ConfigureCreator(modelBuilder.Entity<Creator>());
        ConfigureInvitation(modelBuilder.Entity<Invitation>());
        ConfigureRequest(modelBuilder.Entity<Request>());
    }

    private static void ConfigureEvent(EntityTypeBuilder<Event> eventEntity)
    {
        eventEntity.Property(e => e.Id).ValueGeneratedOnAdd();
        eventEntity.HasKey(e => e.Id);
        eventEntity.Property(e => e.Title).IsRequired();
        eventEntity.Property(e => e.Description).IsRequired();
        eventEntity.Property(e => e.StartDateTime).IsRequired();
        eventEntity.Property(e => e.EndDateTime).IsRequired();
        eventEntity.Property(e => e.MaxGuests).IsRequired();
        eventEntity.Property(e => e.Visibility).IsRequired();
        eventEntity.Property(e => e.Status).IsRequired();
        eventEntity.HasOne<Location>("Location").WithMany().HasForeignKey("locationId");
        eventEntity.HasMany<Invitation>("Invitations").WithOne().HasForeignKey("eventId")
            .OnDelete(DeleteBehavior.Cascade);
        eventEntity.HasMany<Request>("Requests").WithOne();
        eventEntity.HasMany(e => e.Guests).WithMany(g => g.Events).UsingEntity(j => j.ToTable("EventGuests"));
    }

    private static void ConfigureGuest(EntityTypeBuilder<Guest> guestEntity)
    {
        guestEntity.Property(e => e.Id).ValueGeneratedOnAdd();
        guestEntity.HasKey(e => e.Id);
        guestEntity.Property(e => e.Email).IsRequired();
        guestEntity.Property(e => e.FirstName).IsRequired();
        guestEntity.Property(e => e.LastName).IsRequired();
        guestEntity.Property(e => e.ProfilePicURL);
        guestEntity.HasMany(g => g.Events).WithMany(e => e.Guests).UsingEntity(j => j.ToTable("EventGuests"));;
    }

    private static void ConfigureLocation(EntityTypeBuilder<Location> locationEntity)
    {
        locationEntity.Property(e => e.Id).ValueGeneratedOnAdd();
        locationEntity.HasKey(e => e.Id);
        locationEntity.Property(e => e.Name).IsRequired();
        locationEntity.Property(e => e.MaxCapacity).IsRequired();
        locationEntity.HasMany(e => e.Availability).WithOne().HasForeignKey("locationId");
    }
    
    private static void ConfigureCreator(EntityTypeBuilder<Creator> creatorEntity)
    {
        creatorEntity.Property(e => e.Id).ValueGeneratedOnAdd();
        creatorEntity.HasKey(e => e.Id);
        creatorEntity.Property(e => e.Username).IsRequired();
        creatorEntity.Property(e => e.Password).IsRequired();
    }

    private static void ConfigureInvitation(EntityTypeBuilder<Invitation> invitationEntity)
    {
        invitationEntity.Property(e => e.Id).ValueGeneratedOnAdd();
        invitationEntity.HasKey(e => e.Id);
    }
    
    private static void ConfigureRequest(EntityTypeBuilder<Request> requestEntity)
    {
        requestEntity.Property(e => e.Id).ValueGeneratedOnAdd();
        requestEntity.HasKey(e => e.Id);
    }
}
