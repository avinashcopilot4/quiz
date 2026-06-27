using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class AuditLog
{
    public int LogId { get; set; }

    public string EntityType { get; set; } = null!;

    public int EntityId { get; set; }

    public string ActionType { get; set; } = null!;

    public int? PerformedBy { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public DateTime? ActionDate { get; set; }
}
