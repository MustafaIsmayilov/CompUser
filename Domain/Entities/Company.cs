using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; }

    public string? OwnerId { get; set; }
    public AppUser Owner { get; set; }

    public ICollection<Service> Services { get; set; }
}
