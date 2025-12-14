namespace LibraryManagementSytem.Data;

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class CategoryUpdateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
