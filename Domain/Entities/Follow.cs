namespace Domain.Entities;

public class Follow : BaseEntity
{
    public string UserId { get; set; }
    public int CompanyId { get; set; }
    public AppUser User { get; set; }
    public Company Company { get; set; }
}
