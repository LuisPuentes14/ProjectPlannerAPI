using System;
using System.Collections.Generic;

namespace Entity;

public partial class TasksStatus
{
    public int TaskStatusId { get; set; }

    public string? TaskStatusName { get; set; }

    public virtual ICollection<ProjectsTask> ProjectsTasks { get; set; } = new List<ProjectsTask>();
}
