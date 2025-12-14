namespace LibraryManagementSytem.Data.DTO;

public class AuthorCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public string? Biography { get; set; }
    public string? Nationality { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
}

public class AuthorUpdateDto : AuthorCreateDto
{
    public bool IsActive { get; set; }
}
