using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Company;

public class UpdateCompanyRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}
