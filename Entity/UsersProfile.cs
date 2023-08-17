using System;
using System.Collections.Generic;

namespace Entity;

public partial class UsersProfile
{
    public int UserProfileId { get; set; }

    public int? ProfileId { get; set; }

    public int? UserId { get; set; }

    public virtual Profile? Profile { get; set; }

    public virtual User? User { get; set; }
}
