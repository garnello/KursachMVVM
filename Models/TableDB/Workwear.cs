using System;
using System.Collections.Generic;

namespace KursachWPF.Models;

public partial class Workwear
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int Size { get; set; }

    public int Quantity { get; set; }

    public int DegreeOfWear { get; set; }

    public virtual ICollection<HistoryOfIssuance> HistoryOfIssuances { get; set; } = new List<HistoryOfIssuance>();

    public virtual ICollection<HistoryOfReturn> HistoryOfReturns { get; set; } = new List<HistoryOfReturn>();

    public virtual ICollection<ReplacementHistory> ReplacementHistories { get; set; } = new List<ReplacementHistory>();
}
