using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackWCF.BL
{
    class HeadersValueConverter
    {
        public static Guid ConvertToGuid(string headerValue)
        {
            Guid authentication;
            Guid.TryParse(headerValue, out authentication);
            return authentication;
        }
    }
}
