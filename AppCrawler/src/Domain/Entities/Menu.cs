using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public static class Menu
    {
         public static readonly string[,] Itens = {
            { "Home,Index,Details", "/Home/Index" },
            { "Contact", "/Home/Contact" },
            { "About", "/Home/About" },
            { "GitHub", "https://github.com/vanhackathon-team/app-samurai"}
        };

        public static string GetMenuName(int index) {
            return Itens[index,0].Split(',')[0];
        }

        public static string GetStatus(string currentAction, int index) {
            
           foreach (string action in Itens[index, 0].Split(',')) 
           if (currentAction.ToLower().Contains(action.ToLower()))
                return "active";                                          

            return "";
        }

    }
}
