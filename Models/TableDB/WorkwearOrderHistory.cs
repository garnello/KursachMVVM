using System;
using System.Collections.Generic;

namespace KursachWPF.Models;

public class WorkwearOrderHistory
{
    public int Id { get; set; }

    public int IdEmployee { get; set; }

    public string TypeWorkwear { get; set; } = null!;

    public int Quantity { get; set; }

    public DateOnly DateOfOrder { get; set; }

    public int Cost { get; set; }

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;
}
