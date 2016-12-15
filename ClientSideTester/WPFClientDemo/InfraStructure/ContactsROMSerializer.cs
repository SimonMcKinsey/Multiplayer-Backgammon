using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract;

namespace WPFClientDemo.InfraStructure
{
    public class ContactsROMSerializer
    {

        private string path;
        #region Singleton Implementation
        private static ContactsROMSerializer instance = null;
        public static ContactsROMSerializer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ContactsROMSerializer();
                    }
                    return instance;
                }
            }

        }
        private static readonly object padlock = new object();
        #endregion

        private ContactsROMSerializer()
        {
            path = AppDomain.CurrentDomain.BaseDirectory + "/Contacts.txt";

        }
        public void SaveAllSignedContacts(List<UserDTO> AllOtherSignedUpUsers)  //serialize to Rom
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(path);
            formatter.Serialize(fileStream, AllOtherSignedUpUsers);
            fileStream.Close();
        }
        public void SaveNewSignedUser(UserDTO newSignedUpUser) //add to file the new SingedUp user
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(path,FileMode.Append,FileAccess.Write);
            formatter.Serialize(fileStream, newSignedUpUser);
            fileStream.Close();
        }
        public List<UserDTO> LoadAllSignedContacts()  //desirialize from Rom
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(path))
            {
                FileStream fileStream = File.Open(path, FileMode.Open);
                List<UserDTO> AllOtherSignedUpUsers = (List<UserDTO>)formatter.Deserialize(fileStream);
                fileStream.Close();
                return AllOtherSignedUpUsers;
            }
            else
            {
                List<UserDTO> emptyContactList = new List<UserDTO>();
                SaveAllSignedContacts(emptyContactList);
                return emptyContactList;
            }
        }
    }
}
