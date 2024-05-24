namespace KursachWPF.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public byte[]? Avatar { get; set; }
}
