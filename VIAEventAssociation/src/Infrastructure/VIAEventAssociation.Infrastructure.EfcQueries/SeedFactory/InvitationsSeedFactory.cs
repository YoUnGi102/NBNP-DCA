using Newtonsoft.Json;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactory;

public class InvitationsSeedFactory
{
    public static List<Invitation>? CreateInvitations()
    {
        string jsonContent = File.ReadAllText("../JSONS/Invitations.json");
        List<Invitation>? invitations = JsonConvert.DeserializeObject<List<Invitation>>(jsonContent);
        return invitations;
    }
}