using System;
using System.Collections.Generic;

namespace Entity;

public partial class TypesModule
{
    public int TypeModuleId { get; set; }

    public string? TypeModuleName { get; set; }

    public virtual ICollection<WebModule> WebModules { get; set; } = new List<WebModule>();
}
