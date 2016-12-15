using Backgammon;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFClientDemo.Converters
{
    public class ImageSourceCubeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            string changedPath = path.Remove(path.Length - 9) + "Images/";
            string replacedPathByRegex = Regex.Replace(changedPath, @"\\", "/");

            switch ((int)value)
            {
                case 1:
                    return replacedPathByRegex + "1.png";
                case 2:
                    return replacedPathByRegex + "2.png";
                case 3:
                    return replacedPathByRegex + "3.png";
                case 4:
                    return replacedPathByRegex + "4.png";
                case 5:
                    return replacedPathByRegex + "5.png";
                case 6:
                    return replacedPathByRegex + "6.png";
                default:
                    return new Exception("something is wrong with the steps values");
                    //case 1:
                    //    return @"C:\Users\msvs\Desktop\Desktop_2\LastTalkBack\ClientSideTester\WPFClientDemo\Images\1.png";
                    //case 2:
                    //    return @"C:\Users\msvs\Desktop\Desktop_2\LastTalkBack\ClientSideTester\WPFClientDemo\Images\2.png";
                    //case 3:
                    // return @"C:\Users\msvs\Desktop\Desktop_2\LastTalkBack\ClientSideTester\WPFClientDemo\Images\3.png";
                    //case 4:
                    //    return @"C:\Users\msvs\Desktop\Desktop_2\LastTalkBack\ClientSideTester\WPFClientDemo\Images\4.png";
                    //case 5:
                    //    return @"C:\Users\msvs\Desktop\Desktop_2\LastTalkBack\ClientSideTester\WPFClientDemo\Images\5.png";
                    //case 6:
                    //    return @"C:\Users\msvs\Desktop\Desktop_2\LastTalkBack\ClientSideTester\WPFClientDemo\Images\6.png";
                    //default:
                    //    return new Exception("something is wrong with the steps values");
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
