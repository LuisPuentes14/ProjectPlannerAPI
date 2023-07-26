using System;
using System.Collections.Generic;

namespace Entity;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string UserEmail { get; set; } = null!;

    public virtual ICollection<Project> ProjectProjectDirectBossNavigations { get; set; } = new List<Project>();

    public virtual ICollection<Project> ProjectProjectImmediateBossNavigations { get; set; } = new List<Project>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}
