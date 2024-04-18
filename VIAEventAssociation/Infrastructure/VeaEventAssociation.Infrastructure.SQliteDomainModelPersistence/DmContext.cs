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
        ConfigureEvent(modelBuilder.Entity<Event>(), modelBuilder.Entity<Location>(), modelBuilder.Entity<Guest>());
        ConfigureGuest(modelBuilder.Entity<Guest>(),
            modelBuilder.Entity<Invitation>(), modelBuilder.Entity<Request>());
        ConfigureLocation(modelBuilder.Entity<Location>());
        ConfigureCreator(modelBuilder.Entity<Creator>());
    }

    private static void ConfigureEvent(EntityTypeBuilder<Event> eventEntity, EntityTypeBuilder<Location> locationEntity
    , EntityTypeBuilder<Guest> guestEntity)
    {
        eventEntity.HasKey(e => e.id);
        locationEntity.HasKey(e => e.id);
        guestEntity.HasKey(e => e.id);
        eventEntity.Property<int>("LocationId");
        eventEntity.Property(e => e.title).IsRequired();
        eventEntity.Property(e => e.description).IsRequired();
        eventEntity.Property(e => e.start_date_time).IsRequired();
        eventEntity.Property(e => e.end_date_time).IsRequired();
        eventEntity.Property(e => e.max_guests).IsRequired();
        eventEntity.Property(e => e.visibility).IsRequired();
        eventEntity.Property(e => e.status).IsRequired();
        eventEntity.HasMany<Guest>("guests").WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
        eventEntity.HasOne<Location>().WithMany().HasForeignKey("LocationId");
    }
    
    private static void ConfigureGuest(EntityTypeBuilder<Guest> guestEntity, EntityTypeBuilder<Invitation> invitationEntity, EntityTypeBuilder<Request> requestEntity)
    {
        guestEntity.HasKey(e => e.id);
        invitationEntity.HasKey(e => e.Id);
        requestEntity.HasKey(e => e.Id);
        guestEntity.Property(e => e.email).IsRequired();
        guestEntity.HasMany<Invitation>().WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
        guestEntity.HasMany<Request>().WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
    }
    
    private static void ConfigureLocation(EntityTypeBuilder<Location> locationEntity)
    {
        locationEntity.HasKey(e => e.id);
        locationEntity.Property(e => e.name).IsRequired();
        locationEntity.Property(e => e.maxCapacity).IsRequired();
    }
    
    private static void ConfigureCreator(EntityTypeBuilder<Creator> creatorEntity)
    {
        creatorEntity.HasKey(e => e.Id);
        creatorEntity.Property(e => e.Username).IsRequired();
        creatorEntity.Property(e => e.Password).IsRequired();
    }
    
}
