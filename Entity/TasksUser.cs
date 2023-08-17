using System;
using System.Collections.Generic;

namespace Entity;

public partial class TasksUser
{
    public int TaskUserId { get; set; }

    public int? TasksId { get; set; }

    public int? UserId { get; set; }

    public virtual ProjectsTask? Tasks { get; set; }

    public virtual User? User { get; set; }
}
