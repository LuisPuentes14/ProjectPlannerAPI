﻿using System;
using System.Collections.Generic;

namespace Entity;

public partial class ProjectsUser
{
    public int ProjectUser { get; set; }

    public int? ProjectId { get; set; }

    public int? UserId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
