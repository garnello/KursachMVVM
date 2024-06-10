using System;
using System.Collections.Generic;

namespace KursachWPF.Models;

public partial class WriteOffHistory
{
    public int Id { get; set; }

    public int IdWorkwear { get; set; }

    public string? ReasonForWirteOff { get; set; }

    public DateOnly? DateOfWriteOff { get; set; }

    public virtual Workwear IdWorkwearNavigation { get; set; } = null!;
}
