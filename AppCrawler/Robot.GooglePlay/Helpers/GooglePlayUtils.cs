using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            double result = Convert.ToDouble(rating);
            result = Math.Round((result * 5) / 100, 1);

            return result.ToString();
        }

    }
}
