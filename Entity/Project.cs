using System;
using System.Collections.Generic;

namespace Entity;

public partial class Project
{
    public int ProjectId { get; set; }

    public int? ProjectStatusId { get; set; }

    public string? ProjectTitle { get; set; }

    public int? CustomerId { get; set; }

    public int? ProjectDirectBossUserId { get; set; }

    public int? ProjectImmediateBossUserId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual User? ProjectDirectBossUser { get; set; }

    public virtual User? ProjectImmediateBossUser { get; set; }

    public virtual ProjectsStatus? ProjectStatus { get; set; }

    public virtual ICollection<ProjectsTask> ProjectsTasks { get; set; } = new List<ProjectsTask>();

    public virtual ICollection<ResponsiblesUsersProject> ResponsiblesUsersProjects { get; set; } = new List<ResponsiblesUsersProject>();
}
