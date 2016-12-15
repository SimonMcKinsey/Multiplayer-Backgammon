using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFClientDemo.ViewModels;

namespace WPFClientDemo.Models
{
    public class ConsoleLog : ModelBase
    {
        private string document;

        public string Document
        {
            get { return document; }
            set {
                document = value;
                OnPropertyChanged();
            }
        }

    }
}
