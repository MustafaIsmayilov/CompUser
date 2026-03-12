namespace Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public ICollection<Post> Posts { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
