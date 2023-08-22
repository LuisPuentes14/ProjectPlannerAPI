using System;
using System.Collections.Generic;

namespace Entity;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int UserStateId { get; set; }

    public DateTime? UserDateExpPassword { get; set; }

    public DateTime? UserDateCreate { get; set; }

    public DateTime? UserDateUpdate { get; set; }

    public int? UserAttempts { get; set; }

    public bool? UserFirstLogin { get; set; }

    public virtual ICollection<Project> ProjectProjectDirectBossUsers { get; set; } = new List<Project>();

    public virtual ICollection<Project> ProjectProjectImmediateBossUsers { get; set; } = new List<Project>();

    public virtual ICollection<ResponsiblesUsersProject> ResponsiblesUsersProjects { get; set; } = new List<ResponsiblesUsersProject>();

    public virtual ICollection<TasksUser> TasksUsers { get; set; } = new List<TasksUser>();

    public virtual UsersState UserState { get; set; } = null!;

    public virtual ICollection<UsersProfile> UsersProfiles { get; set; } = new List<UsersProfile>();
}
