using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientDemo.ViewModels;

namespace WPFClientDemo.InfraStructure
{
    public static class ViewsFactory
    {
        //dictionary dont fit because of early going through ctor in contactListView
        //private static Lazy<Dictionary<string, ViewModelBase>> viewModelByName = null;
        //public static Dictionary<string, ViewModelBase> ViewModelByName
        //{
        //    get
        //    {
        //        return viewModelByName.Value;
        //    }
        //}
        //static ViewsFactory()
        //{
        //    viewModelByName = new Lazy<Dictionary<string, ViewModelBase>>(() => LoadDictionary());
        //}
        //public static ViewModelBase CreateView(string viewName)
        //{
        //    if (ViewModelByName.ContainsKey(viewName))
        //    {
        //        return ViewModelByName[viewName];
        //    }
        //    return new LoginViewModel();
        //}
        public static ViewModelBase CreateView(string viewName)
        {
            switch (viewName)
            {
                case "LoginView":
                    return new LoginViewModel();
                case "ContactListView":
                    return new ContactListViewModel();
                case "SignUpView":
                    return new SignUpViewModel();
                default:
                    return new LoginViewModel();
            }
        }

        //private static Dictionary<string, ViewModelBase> LoadDictionary()
        //{
        //    Dictionary<string, ViewModelBase> tempDictionary = new Dictionary<string, ViewModelBase>();

        //    tempDictionary.Add("SignUpView", new SignUpViewModel());
        //    tempDictionary.Add("ContactListView", new ContactListViewModel());
        //    tempDictionary.Add("LoginVIew", new LoginViewModel());
        //    return tempDictionary;
        //}
    }

    /*
    public static class ViewsFactory
    {
        private static Dictionary<string, ViewModelBase> viewModelByName;
        static ViewsFactory()
        {
   

        }
        public static ViewModelBase CreateView(string viewName)
        {
            if(viewModelByName == null)
            {
                InitDicitonary();
            }

            if (viewModelByName.ContainsKey(viewName))
            {
                return viewModelByName[viewName];
            }
            return new LoginViewModel();
        }

        private static void InitDicitonary()
        {
            viewModelByName = new Dictionary<string, ViewModelBase>();
            
            viewModelByName.Add("SignUpView", new SignUpViewModel());
            viewModelByName.Add("ContactListView", new ContactListViewModel());
            viewModelByName.Add("LoginVIew", new LoginViewModel());
        }
    }
}
*/ // Simpler Factory + Lazy loading implementation.
}
 