using System;
using System.Collections.Generic;

namespace KursachWPF.Models;

public partial class WorkwearReceiptHistory
{
    public int Id { get; set; }

    public string TypeWorkwear { get; set; } = null!;

    public int Quantity { get; set; }

    public DateOnly DateOfOrder { get; set; }
}
