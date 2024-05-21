using System;
using System.Collections.Generic;

namespace KursachWPF.Models;

public partial class Report
{
    public int Id { get; set; }

    public int IdEmployee { get; set; }

    public DateOnly DateOfReport { get; set; }

    public char StatusOfReport { get; set; }

    public string Description { get; set; } = null!;

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;
}
