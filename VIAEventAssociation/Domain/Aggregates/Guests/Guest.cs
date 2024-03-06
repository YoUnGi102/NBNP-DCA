namespace Domain.Aggregates.Guests;

public class Guest
{
    private string email;

    public Guest(string email)
    {
        this.email = email;
    }
    
    public void Participate(){}
    public void RemoveParticipation(){}
    
    public void RequestToJoin(){}
    
    public void AcceptInvitation(){}
    
    public void DeclineInvitation(){}
}