using System;
using System.Collections.Generic;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public partial class Creator
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
