﻿using System;
using System.Collections.Generic;

namespace Entity;

public partial class Environment
{
    public int EnvironmentId { get; set; }

    public string? EnvironmentName { get; set; }

    public virtual ICollection<ProjectsTask> ProjectsTasks { get; set; } = new List<ProjectsTask>();
}
