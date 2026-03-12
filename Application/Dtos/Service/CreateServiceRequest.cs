using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Service;

public class CreateServiceRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CompanyId { get; set; }
}