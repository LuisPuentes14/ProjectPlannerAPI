﻿using System;
using System.Collections.Generic;

namespace Entity;

public partial class UserState
{
    public int UserStateId { get; set; }

    public string? UserStateName { get; set; }

    public virtual ICollection<Usersa> Usersas { get; set; } = new List<Usersa>();
}
