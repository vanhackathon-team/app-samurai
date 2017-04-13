using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class App
    {
        public string Name { get; set; }

        public string Description { get; set; }

        private string icon;
        private string rating;

        public string Icon
        {
            get {
                if (icon == null)
                    return string.Empty;

                if (icon.Contains("https"))
                    return icon;
                else
                    return $"https:{icon}";
            }
            set { icon = value; }
        }


        public int RankingCategory { get; set; }

        public int PositionOverall { get; set; }

        public IList<string> Screenshots { get; set; }

        public string Link { get; set; }
                
        public string SubTitle { get; set; }

        //public string Rating
        //{
        //    get { return FixRating(rating); }
        //    set { rating = value; }
        //}
        public string Rating { get; set; }

        public string Price { get; set; }

        public string Package { get; set; }

        public string Category { get; set; }

        private static string FixRating(string rating)
        {
            double result = Convert.ToDouble(rating);
            result = Math.Round((result * 5) / 100, 1);

            return result.ToString();
        }

        public override string ToString()
        {

            return "\n Title: " + Name + "\n "
                + "SubTitle: " + SubTitle + "\n "
                + "Description: " + Description + "\n "
                + "Rating: " + Rating + "\n "
                + "Price: " + Price + "\n "
                + "Package: " + Package + "\n "
                + "RankingCategory: " + RankingCategory + "\n "
                + "PositionOverall: " + PositionOverall + "\n "
                + "Icon: " + Icon + "\n "
                + "Link: " + Link + "\n "
                + "Category: " + Category + "\n ";
        }
    }
}
