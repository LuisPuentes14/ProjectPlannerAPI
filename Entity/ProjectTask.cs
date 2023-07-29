using System;
using System.Collections.Generic;

namespace Entity;

public partial class ProjectTask
{
    public int ProjectTasksId { get; set; }

    public string? ProjectTasks { get; set; }

    public DateOnly? ProjectTasksStartDate { get; set; }

    public DateOnly? ProjectTasksEndDate { get; set; }

    public int? TaskStatusId { get; set; }

    public int? EnvironmentId { get; set; }

    public string? ProjectTasksObservations { get; set; }

    public int? ProjectId { get; set; }

    public virtual Environment? Environment { get; set; }

    public virtual Project? Project { get; set; }

    public virtual TasksStatus? TaskStatus { get; set; }

    public virtual ICollection<TasksUser> TasksUsers { get; set; } = new List<TasksUser>();
}
