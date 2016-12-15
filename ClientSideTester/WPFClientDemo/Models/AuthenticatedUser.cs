using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace WPFClientDemo
{

    public sealed class  AuthenticatedUser : UserDTO
    {
        public Guid Authentication { get; set; }
        private static AuthenticatedUser instance = null;
        public static AuthenticatedUser Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AuthenticatedUser();
                    }
                    return instance;
                }
            }

        }
        private static readonly object padlock = new object();

        private AuthenticatedUser()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            Authentication = Guid.Empty;
        }
    }
}
