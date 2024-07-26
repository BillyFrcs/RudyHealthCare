using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RudyHealthCare.Blueprints.Admin
{
    public class AdminAccountBlueprint
    {
        public string? Email { get; set; }

        public IList<string>? Role { get; set; }
    }
}