using System;
using System.Collections.Generic;

namespace Entity;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
