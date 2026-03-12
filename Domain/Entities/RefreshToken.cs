using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }

    public DateTime ExpireDate { get; set; }

    public bool IsRevoked { get; set; }

    public string UserId { get; set; }

    public AppUser User { get; set; }
}
