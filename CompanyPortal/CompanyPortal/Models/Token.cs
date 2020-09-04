using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPortal.Models
{
    public class Token
    {
        public string JWTSecret { get; set; }
        public int TokenExpiryInMinutes { get; set; }
    }
}
