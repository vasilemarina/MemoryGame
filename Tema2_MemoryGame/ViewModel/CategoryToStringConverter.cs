using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema2_MemoryGame.Model;

namespace Tema2_MemoryGame.ViewModel
{

    public class CategoryToStringConverter
    {
        public string Convert(GameCategory Category)
        {
            return Category.ToString();
        }
        public GameCategory ConvertBack(string Category)
        {
            switch (Category)
            {
                case "Flowers":
                    return GameCategory.Flowers;
                case "Animals":
                    return GameCategory.Animals;
                case "Fruits":
                    return GameCategory.Fruits;
            }
            return GameCategory.Flowers;
        }

    }
}
