using System;
using System.Collections.Generic;

namespace Entity;

public partial class WebModule
{
    public int WebModuleId { get; set; }

    public int? WebModuleFather { get; set; }

    public string? WebModuleTitle { get; set; }

    public string? WebModuleDescription { get; set; }

    public string? WebModuleUrl { get; set; }

    public string? WebModuleIcon { get; set; }

    public int? WebModuleIndex { get; set; }

    public int? TypeModuleId { get; set; }

    public DateTime? WebModuleCreateDate { get; set; }

    public virtual ICollection<PermissionsProfilesWebModule> PermissionsProfilesWebModules { get; set; } = new List<PermissionsProfilesWebModule>();

    public virtual TypesModule? TypeModule { get; set; }
}
