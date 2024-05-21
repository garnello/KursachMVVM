using System;
using System.Collections.Generic;

namespace KursachWPF.Models;

public partial class HistoryOfIssuance
{
    public int Id { get; set; }

    public int IdEmployee { get; set; }

    public int IdWorkwear { get; set; }

    public DateOnly DateOfIssuance { get; set; }

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual Workwear IdWorkwearNavigation { get; set; } = null!;
}
