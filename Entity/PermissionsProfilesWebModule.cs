using System;
using System.Collections.Generic;

namespace Entity;

public partial class PermissionsProfilesWebModule
{
    public int PermissionProfileWebModuleId { get; set; }

    public int ProfileId { get; set; }

    public int WebModuleId { get; set; }

    public bool? PermissionProfileWebModuleAccess { get; set; }

    public bool? PermissionProfileWebModuleCreate { get; set; }

    public bool? PermissionProfileWebModuleUpdate { get; set; }

    public bool? PermissionProfileWebModuleDelete { get; set; }

    public bool? PermissionProfileWebModuleDownload { get; set; }
}
