using System;
using System.Collections.Generic;

namespace Entity;

public partial class Atmosphere
{
    public int AtmosphereId { get; set; }

    public string? AtmosphereName { get; set; }

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
