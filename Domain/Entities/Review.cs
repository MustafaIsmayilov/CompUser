using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}
