using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Infrastructure.EfcQueries.SeedFactory;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public static class ViaEventAssociationContextExtension
{
    public static ViaEventAssociationContext Seed(this ViaEventAssociationContext context)
    {
        context.Guests.AddRange(GuestSeedFactory.CreateGuests());
        context.Events.AddRange(EventsSeedFactory.CreateEvents());
        context.Locations.AddRange(LocationsSeedFactory.CreateLocations());
        context.Invitations.AddRange(InvitationsSeedFactory.CreateInvitations());
        context.SaveChanges();
        return context;
    }
    public static ViaEventAssociationContext SetupReadContext()
    {
        DbContextOptionsBuilder<ViaEventAssociationContext> optionsBuilder = new();
        string testDbName = "Test"+Guid.NewGuid()+".db";
        optionsBuilder.UseSqlite(@"Data Source = " + testDbName);
        ViaEventAssociationContext context = new(optionsBuilder.Options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}