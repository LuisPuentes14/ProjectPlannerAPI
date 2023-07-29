using System;
using System.Collections.Generic;

namespace Entity;

public partial class UserState
{
    public int UserStateId { get; set; }

    public string? UserStateName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
