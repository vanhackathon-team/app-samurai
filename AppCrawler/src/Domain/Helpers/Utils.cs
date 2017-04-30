using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class Utils
    {
        public static string CapitalizeText(string data)
        {

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(data.ToLower())
                .Replace("-", " ")
                .Replace("_", " ");
        }

        public static string FixSeachLink(string searchLink)
        {
            return searchLink.Replace(" ", "%20");
        }
    }
}
