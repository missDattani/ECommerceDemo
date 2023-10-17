using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.JwtSetting
{
    public class JwtSetter
    {
        public string? JWT_Secret { get; set; }

        public string Duration { get; set; }
    }
}
