using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.CreatorPersistance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.EventPersitance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.GuestPersistance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.InvitationPersistance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.LocationPersistance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.RequestPersistance;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public class DmContext(DbContextOptions options) : DbContext(options)
{
    
    public DbSet<EventEntityConfiguration> Events => Set<EventEntityConfiguration>();
    public DbSet<GuestEntityConfiguration> Guests => Set<GuestEntityConfiguration>();
    public DbSet<LocationEntityConfiguration> Locations => Set<LocationEntityConfiguration>();
    public DbSet<CreatorEntityConfiguration> Creators => Set<CreatorEntityConfiguration>();
    // public DbSet<Invitation> Invitations => Set<Invitation>();
    // public DbSet<Request> Requests => Set<Request>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(DmContext).Assembly);
        ConfigureEvent(modelBuilder.Entity<EventEntityConfiguration>(), modelBuilder.Entity<LocationEntityConfiguration>(), modelBuilder.Entity<GuestEntityConfiguration>());
        ConfigureGuest(modelBuilder.Entity<GuestEntityConfiguration>(),
            modelBuilder.Entity<InvitationEntityConfiguration>(), modelBuilder.Entity<RequestEntityConfiguration>());
        ConfigureLocation(modelBuilder.Entity<LocationEntityConfiguration>());
        ConfigureCreator(modelBuilder.Entity<CreatorEntityConfiguration>());
    }

    private static void ConfigureEvent(EntityTypeBuilder<EventEntityConfiguration> eventEntity, EntityTypeBuilder<LocationEntityConfiguration> locationEntity
    , EntityTypeBuilder<GuestEntityConfiguration> guestEntity)
    {
        eventEntity.HasKey(e => e.id);
        locationEntity.HasKey(e => e.id);
        guestEntity.HasKey(e => e.id);
        eventEntity.Property<Guid>("LocationId");
        eventEntity.Property(e => e.title).IsRequired();
        eventEntity.Property(e => e.description).IsRequired();
        eventEntity.Property(e => e.start_date_time).IsRequired();
        eventEntity.Property(e => e.end_date_time).IsRequired();
        eventEntity.Property(e => e.max_guests).IsRequired();
        eventEntity.Property(e => e.visibility).IsRequired();
        eventEntity.Property(e => e.status).IsRequired();
        eventEntity.HasMany<GuestEntityConfiguration>("guests").WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
        eventEntity.HasOne<LocationEntityConfiguration>().WithMany().HasForeignKey("LocationId");
    }
    
    private static void ConfigureGuest(EntityTypeBuilder<GuestEntityConfiguration> guestEntity, EntityTypeBuilder<InvitationEntityConfiguration> invitationEntity, EntityTypeBuilder<RequestEntityConfiguration> requestEntity)
    {
        guestEntity.HasKey(e => e.id);
        invitationEntity.HasKey(e => e.id);
        requestEntity.HasKey(e => e.id);
        guestEntity.Property(e => e.email).IsRequired();
        guestEntity.HasMany<GuestEntityConfiguration>("guests").WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
        guestEntity.HasMany<InvitationEntityConfiguration>().WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
        guestEntity.HasMany<RequestEntityConfiguration>().WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
    }
    
    private static void ConfigureLocation(EntityTypeBuilder<LocationEntityConfiguration> locationEntity)
    {
        locationEntity.HasKey(e => e.id);
        locationEntity.Property(e => e.name).IsRequired();
        locationEntity.Property(e => e.maxCapacity).IsRequired();
        locationEntity.HasMany<LocationEntityConfiguration>("locations").WithOne().HasForeignKey("parentId").OnDelete(DeleteBehavior.Cascade);
    }
    
    private static void ConfigureCreator(EntityTypeBuilder<CreatorEntityConfiguration> creatorEntity)
    {
        creatorEntity.HasKey(e => e.id);
        creatorEntity.Property(e => e.username).IsRequired();
        creatorEntity.Property(e => e.password).IsRequired();
    }
    
}
