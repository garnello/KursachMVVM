using System;
using System.Collections.Generic;

namespace KursachWPF.Models;

public partial class Employee
{
    public int Id { get; set; }
    public string FullName => $"{Surname} {Name}";

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Post { get; set; } = null!;

    public DateOnly DateOfEmployment { get; set; }

    public DateOnly? DateOfBirthday { get; set; }

    public virtual ICollection<HistoryOfIssuance> HistoryOfIssuances { get; set; } = new List<HistoryOfIssuance>();

    public virtual ICollection<HistoryOfReturn> HistoryOfReturns { get; set; } = new List<HistoryOfReturn>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
