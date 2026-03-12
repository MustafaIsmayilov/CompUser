using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Follow;

public class FollowResponse
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
}
