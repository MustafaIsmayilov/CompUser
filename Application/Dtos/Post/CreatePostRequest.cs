using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Post;

public class CreatePostRequest
{
    public string Content { get; set; }
    public int ServiceId { get; set; }
}
