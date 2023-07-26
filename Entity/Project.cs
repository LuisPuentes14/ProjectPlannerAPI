using System;
using System.Collections.Generic;

namespace Entity;

public partial class Project
{
    public int ProjectId { get; set; }

    public int? ProjectStatus { get; set; }

    public string? ProjectTitle { get; set; }

    public int? ProjectCustomer { get; set; }

    public int? ProjectDirectBoss { get; set; }

    public int? ProjectImmediateBoss { get; set; }

    public virtual Customer? ProjectCustomerNavigation { get; set; }

    public virtual User? ProjectDirectBossNavigation { get; set; }

    public virtual User? ProjectImmediateBossNavigation { get; set; }

    public virtual ProjectStatus? ProjectStatusNavigation { get; set; }

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    public virtual ICollection<User> ProjectUsers { get; set; } = new List<User>();
}
