using System;
using System.Collections.Generic;

namespace Entity;

public partial class ProjectsTask
{
    public int ProjectTaskId { get; set; }

    public string? ProjectTask { get; set; }

    public DateOnly? ProjectTaskStartDate { get; set; }

    public DateOnly? ProjectTaskEndDate { get; set; }

    public int? TaskStatusId { get; set; }

    public int? EnvironmentId { get; set; }

    public string? ProjectTaskObservation { get; set; }

    public int? ProjectId { get; set; }

    public virtual Environment? Environment { get; set; }

    public virtual Project? Project { get; set; }

    public virtual TasksStatus? TaskStatus { get; set; }

    public virtual ICollection<TasksUser> TasksUsers { get; set; } = new List<TasksUser>();
}
