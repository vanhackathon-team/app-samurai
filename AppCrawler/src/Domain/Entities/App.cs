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

        public string Icon { get; set; }

        public int RankingCategory { get; set; }

        public int PositionOverall { get; set; }

        public IList<string> Screenshots { get; set; }

        public string Link { get; set; }
                
        public string SubTitle { get; set; }

        public string Rating { get; set; }
        
        public string Price { get; set; }

        public string Package { get; set; }

        public string Category { get; set; }

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
