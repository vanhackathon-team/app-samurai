using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class App
    {

        private const int LIMIT_SHORT_DESCRIPTION = 40, LIMIT_TITLE = 20;

        public string Name { get; set; }

        public string ReducedName { get {
                return GetReducedContent(Name, LIMIT_TITLE);
            }
        }

        public string Description { get; set; }

        public string ReducedDescription { get {
                return GetReducedContent(Description, LIMIT_SHORT_DESCRIPTION);
            }
        }

        private string icon;

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

        public string Rating { get; set; }
        
        public string Price { get; set; }

        public string Package { get; set; }

        public string Category { get; set; }
        
        public string CategoryCapitalized
        {
            get { return Utils.CapitalizeText(Category); }            
        }


        private string GetReducedContent(string data, int limit) {

            if (data.Length > limit) { 
                data = data.Substring(0, limit - 4) + " ...";

                if (data.Length < LIMIT_SHORT_DESCRIPTION)
                    data = data.PadRight(LIMIT_SHORT_DESCRIPTION - data.Length);

                return data;
                }
            else
                return data;
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
