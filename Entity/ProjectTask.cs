using System;
using System.Collections.Generic;

namespace Entity;

public partial class ProjectTask
{
    public int ProjectTasksId { get; set; }

    public string? RpojectTasks { get; set; }

    public DateOnly? ProjectTasksStartDate { get; set; }

    public DateOnly? ProjectTasksEndDate { get; set; }

    public string? ProjectTasksDelivered { get; set; }

    public int? ProjectTasksAtmosphere { get; set; }

    public string? ProjectTasksObservations { get; set; }

    public int? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Atmosphere? ProjectTasksAtmosphereNavigation { get; set; }

    public virtual ICollection<User> TasksUsers { get; set; } = new List<User>();
}
