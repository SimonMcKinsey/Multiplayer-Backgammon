using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using TalkBackWCF.Contract.Services;
using TalkBackWCF.Services;

namespace TalkBackWCF.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(EntryPoint), new Uri("http://localhost:888")))
            {
                WSDualHttpBinding bindingInstance = new WSDualHttpBinding();
                bindingInstance.OpenTimeout = new TimeSpan(0, 0, 5);
                bindingInstance.SendTimeout = new TimeSpan(0, 0, 5);
                host.AddServiceEndpoint(typeof(IUserService), bindingInstance, "");
                host.AddServiceEndpoint(typeof(IChatService), bindingInstance, "");
                host.AddServiceEndpoint(typeof(IGameService), bindingInstance, "");
                host.Open();
                Console.ReadLine();
            }
        }
    }
}
