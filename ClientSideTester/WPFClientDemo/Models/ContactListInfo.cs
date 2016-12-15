using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace WPFClientDemo.Models
{
    public class ContactListInfo
    {
        public List<UserDTO> OfflineContacts { get; set; }
        public List<UserDTO> OnlineContacts { get; set; }
        private static ContactListInfo instance = null;
        public static ContactListInfo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ContactListInfo();
                    }
                    return instance;
                }
            }

        }
        private static readonly object padlock = new object();
        //ctor
        private ContactListInfo()
        {
            InitInstance();
        }

        private void InitInstance()
        {

            OfflineContacts = new List<UserDTO>();
            OnlineContacts = new List<UserDTO>();
        }
    }
}
