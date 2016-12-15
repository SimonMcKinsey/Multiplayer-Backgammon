using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;
using WPFClientDemo.InfraStructure;

namespace WPFClientDemo.Models
{
    public class ContactStateManager
    {

        public static void AssignAllCurrentContacts(List<UserDTO> allSignedUpUsers)
        {
            var contactList = ContactListInfo.Instance;
            contactList.OfflineContacts = allSignedUpUsers;
            //serialize to Rom - SavingContacts:               
            ContactsROMSerializer.Instance.SaveAllSignedContacts(allSignedUpUsers);
        }
        public static void AddNewSignedContact(UserDTO newSignedUpUser)
        {
            ContactListInfo.Instance.OfflineContacts.Add(newSignedUpUser);
            //TODO : Save to file
            ContactsROMSerializer.Instance.SaveNewSignedUser(newSignedUpUser);
        }
        public static void AssignAllCurrentOnlineContacts(List<UserDTO> allOnlineUsers)
        {
            //ContactListInfo.Instance.OnlineContacts = allOnlineUsers;
            ////remove from OfflineContacts this users:
            //foreach (var onlineUser in allOnlineUsers)
            //{
            //    ContactListInfo.Instance.OfflineContacts.RemoveAll(u => u.UserName == onlineUser.UserName);
            //}
            foreach (var user in allOnlineUsers)
            {
                UpdateContactToOnline(user);
            }
        }

        public static List<UserDTO> GetOfflineContacts()
        {
            if (ContactListInfo.Instance.OfflineContacts.Count == 0)
            {
                LoadAllContacts();
            }
            //it includes all users so we want all users except the user that use this ContactList:
            ContactListInfo.Instance.OfflineContacts.RemoveAll(u => u.UserName == AuthenticatedUser.Instance.UserName);
            return ContactListInfo.Instance.OfflineContacts;
        }

        internal static void LoadAllContacts()
        {
            //loadin Contacts From ROM:
            ContactListInfo.Instance.OfflineContacts = ContactsROMSerializer.Instance.LoadAllSignedContacts();

        }
        public static List<UserDTO> GetOnlineContacts()
        {
            return ContactListInfo.Instance.OnlineContacts;
        }

        public static void UpdateContactToOnline(UserDTO newOnlineUser)
        {
            ContactListInfo.Instance.OnlineContacts.Add(newOnlineUser);
            //remove the user from OfflineContacts:
            ContactListInfo.Instance.OfflineContacts.RemoveAll(u => u.UserName == newOnlineUser.UserName);
        }
        public static void UpdateContactToOffline(UserDTO newOfflineUser)
        {
            ContactListInfo.Instance.OfflineContacts.Add(newOfflineUser);
            //remove the user from OnlineContacts:
            ContactListInfo.Instance.OnlineContacts.RemoveAll(u => u.UserName == newOfflineUser.UserName);
        }

    }
}
