using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.AppStore.iTunes.Helpers
{
    public static class AppUrl
    {
        public static string GetUrlWithCountry(string url, string country)
        {
            try
            {
                var appleUrl = "https://itunes.apple.com/";
                var appUrl = url.Substring(appleUrl.Length, url.Length - appleUrl.Length);
                var appUrlArray = appUrl.Split('/').ToList();

                if (appUrlArray[0]?.ToLower() != "app")
                    appUrlArray.RemoveAt(0);

                appUrl = string.Concat(appleUrl, "/", country, "/", string.Join("/", appUrlArray.ToArray()));

                return appUrl;
            }
            catch
            {
                return null;
            }
        }
    }
}
