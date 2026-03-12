using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public int ExpirationMinutes { get; set; }
}
