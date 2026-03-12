using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Feed;

public class FeedResponse
{
    public int PostId { get; set; }
    public string Content { get; set; }

    public int CompanyId { get; set; }
    public string CompanyName { get; set; }

    public DateTime CreatedAt { get; set; }
}
