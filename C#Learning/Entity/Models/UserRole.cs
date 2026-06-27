using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public string RoleName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public virtual User User { get; set; } = null!;
}
