using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Robot.GooglePlay.Helpers
{
    class GooglePlayUtils
    {
        public static string GetRating(string rating) {

            if (rating == string.Empty)
                return string.Empty;
            else
                return FixRating(rating.Split(':')[1].Replace("%", "").Replace(";", ""));
        }

        private static string FixRating(string rating) {

            if (rating == string.Empty)
                return string.Empty;

            rating = rating.Trim();
            if (rating.Length > 5)
                rating = rating.Substring(0, 5);

            rating = rating.Replace(",", ".");
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            double result = Convert.ToDouble(rating);
            result = Math.Round((result * 5) / 100, 1);

            rating = result.ToString();

            if (!rating.Contains('.'))
                rating += ".0";
                
            return rating;
        }

    }
}
