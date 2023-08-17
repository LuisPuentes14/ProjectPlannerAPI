using System;
using System.Collections.Generic;

namespace Entity;

public partial class Profile
{
    public int ProfileId { get; set; }

    public string? ProfileName { get; set; }

    public virtual ICollection<PermissionsProfilesWebModule> PermissionsProfilesWebModules { get; set; } = new List<PermissionsProfilesWebModule>();

    public virtual ICollection<UsersProfile> UsersProfiles { get; set; } = new List<UsersProfile>();
}
