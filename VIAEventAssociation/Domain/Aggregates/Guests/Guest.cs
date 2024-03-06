using Domain.Aggregates.Events;
using Domain.Common.Entities;

namespace Domain.Aggregates.Guests;

public class Guest
{
    private string email;
    private List<Request> requests;
    private List<Invitation> invitations;

    public Guest(string email, List<Request> requests, Invitation invitations)
    {
        this.invitations = new List<Invitation>();
        this.email = email;
        this.requests = requests;
    }
    
    public void Participate(Event _event){}
    public void RemoveParticipation(Event _event){}
    
    public void RequestToJoin(Event _event){}
    
    public void AcceptInvitation(Invitation invitation){}
    
    public void DeclineInvitation(Invitation invitation){}
    
    public List<Request> GetRequests()
    {
        return this.requests;
    }
    public List<Invitation> GetInvitations()
    {
        return this.invitations;
    }
    public void SetInvitations(List<Invitation> invitations)
    {
        this.invitations = invitations;
    }
}