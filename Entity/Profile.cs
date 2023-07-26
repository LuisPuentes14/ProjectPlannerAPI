using System;
using System.Collections.Generic;

namespace Entity;

public partial class Profile
{
    public int ProfileId { get; set; }

    public string? ProfileName { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
