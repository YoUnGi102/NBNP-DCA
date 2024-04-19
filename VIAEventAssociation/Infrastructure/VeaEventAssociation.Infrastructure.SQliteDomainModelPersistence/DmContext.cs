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
        eventEntity.Property(e => e.id).ValueGeneratedOnAdd();
        eventEntity.HasKey(e => e.id);
        eventEntity.Property(e => e.title).IsRequired();
        eventEntity.Property(e => e.description).IsRequired();
        eventEntity.Property(e => e.start_date_time).IsRequired();
        eventEntity.Property(e => e.end_date_time).IsRequired();
        eventEntity.Property(e => e.max_guests).IsRequired();
        eventEntity.Property(e => e.visibility).IsRequired();
        eventEntity.Property(e => e.status).IsRequired();
        eventEntity.HasOne<Location>("location").WithMany().HasForeignKey("locationId");
        eventEntity.HasMany<Invitation>("invitations").WithOne().HasForeignKey("eventId")
            .OnDelete(DeleteBehavior.Cascade);
        eventEntity.HasMany<Request>("requests").WithOne();
        eventEntity.HasMany(e => e.guests).WithMany(g => g.events).UsingEntity(j => j.ToTable("EventGuests"));
    }

    private static void ConfigureGuest(EntityTypeBuilder<Guest> guestEntity)
    {
        guestEntity.Property(e => e.id).ValueGeneratedOnAdd();
        guestEntity.HasKey(e => e.id);
        guestEntity.Property(e => e.email).IsRequired();
        guestEntity.HasMany(g => g.events).WithMany(e => e.guests).UsingEntity(j => j.ToTable("EventGuests"));;
    }

    private static void ConfigureLocation(EntityTypeBuilder<Location> locationEntity)
    {
        locationEntity.Property(e => e.id).ValueGeneratedOnAdd();
        locationEntity.HasKey(e => e.id);
        locationEntity.Property(e => e.name).IsRequired();
        locationEntity.Property(e => e.maxCapacity).IsRequired();
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
