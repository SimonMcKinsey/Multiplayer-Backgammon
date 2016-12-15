using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackWCF.BL
{
    class RequestHeaderProvider
    {
        public static WebHeaderCollection GetHeaders()
        {
        IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
        return request.Headers;
        }
        public static string GetHeaderByName(string headerName)
        {
            IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
            WebHeaderCollection headerCollection = request.Headers;
            return headerCollection[headerName];
        }

    }
}
