using System;
using System.Collections.Generic;

namespace Entity;

public partial class Usersa
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int UserStateId { get; set; }

    public DateTime? UserDateExpPassword { get; set; }

    public DateTime? UserDateCreate { get; set; }

    public DateTime? UserDateUpdate { get; set; }

    public int? UserAttempts { get; set; }

    public bool? UserFirstLogin { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();

    public virtual UserState UserState { get; set; } = null!;
}
