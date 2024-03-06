using Domain.Common.Enums;

namespace Domain.Common.Entities;

public class Invitation
{
    internal InvitationStatus status;

    public Invitation(InvitationStatus status)
    {
        this.status = status;
    }
}