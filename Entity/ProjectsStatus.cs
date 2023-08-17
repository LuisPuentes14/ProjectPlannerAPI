using System;
using System.Collections.Generic;

namespace Entity;

public partial class ProjectsStatus
{
    public int ProjectStatusId { get; set; }

    public string? ProjectStatusDescripcion { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
