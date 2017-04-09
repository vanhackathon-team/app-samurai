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
                return rating.Split(':')[1].Replace("%", "").Replace(";", "");
        }
    }
}
