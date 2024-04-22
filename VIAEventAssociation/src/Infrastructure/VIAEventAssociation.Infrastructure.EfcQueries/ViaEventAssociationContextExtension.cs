using Microsoft.EntityFrameworkCore;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public static class ViaEventAssociationContextExtension
{
    public static ViaEventAssociationContext Seed(this ViaEventAssociationContext context)
    {
        context.Guests.AddRange(GuestSeedFactory.CreateGuests());
        List<Event> veaEvents = EventSeedFactory.CreateEvents();
        context.Events.AddRange(veaEvents);
        context.SaveChanges();
        ParticipationSeedFactory.Seed(context);
        context.SaveChanges();
        InvitationSeedFactory.Seed(context);
        context.SaveChanges();
        return context;
    }
    public static ViaEventAssociationContext SetupReadContext()
    {
        DbContextOptionsBuilder<ViaEventAssociationContext> optionsBuilder = new();
        string testDbName = "Test"+Guid.NewGuid().ToString()+".db";
        optionsBuilder.UseSqlite(@"Data Source = " + testDbName);
        ViaEventAssociationContext context = new(optionsBuilder.Options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}