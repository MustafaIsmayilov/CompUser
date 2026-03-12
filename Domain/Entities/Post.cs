namespace Domain.Entities;

public class Post : BaseEntity
{
    public string Content { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }
}
