using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using Tema2_MemoryGame.Model;

namespace Tema2_MemoryGame.Services
{
    public class GameServices
    {
        ObservableCollection<ObservableCollection<Card>> Cards;
        GameCategory category;
        byte BoardRows;
        byte BoardColumns;

        public GameServices(ObservableCollection<ObservableCollection<Card>> Cards, byte rows, byte columns, GameCategory category)
        {
            this.BoardRows = rows;
            this.BoardColumns = columns;
            this.Cards = Cards;
            this.category = category;
        }
        public void InitializeCards()
        {
            List<string> imagePaths = GetImages(category);
            for (byte i = 0; i < BoardRows; i++)
            {
                Cards.Add(new ObservableCollection<Card>());
                for (int j = 0; j < BoardColumns - 1; j+=2)
                {
                    string imagePath = GetRandomImagePath(imagePaths);
                    Cards[i].Add(new Card(imagePath));
                    Cards[i].Add(new Card(imagePath));
                    
                }
            }
            ShuffleCards();
        }
        private string GetRandomImagePath(List<string> imagePaths)
        {
            Random rand = new Random();
            int index = rand.Next(imagePaths.Count);
            return imagePaths[index];
        }
        private List<string> GetImages(GameCategory category)
        {
            List<string> imagePaths = new List<string>();
            string directory = "D:/Facultate_An2_Sem2/MAP/Tema2_MemoryGame/Tema2_MemoryGame/cards_images/";

            switch (category)
            {
                case GameCategory.Flowers:
                    imagePaths.AddRange(Directory.GetFiles(Path.Combine(directory, "flowers_category"), "*.jpg"));
                    break;
                case GameCategory.Animals:
                    imagePaths.AddRange(Directory.GetFiles(Path.Combine(directory, "animals_category"), "*.jpg"));
                    break;
                case GameCategory.Fruits:
                    imagePaths.AddRange(Directory.GetFiles(Path.Combine(directory, "fruits_category"), "*.jpg"));
                    break;
            }

            return imagePaths;
        }
        private void ShuffleCards()
        {
            Random rand = new Random();
            foreach (var row in Cards)
            {
                var shuffledRow = row.OrderBy(c => rand.Next()).ToList();
                row.Clear();
                foreach (var card in shuffledRow)
                    row.Add(card);
            }

            int numRows = Cards.Count;
            int numColumns = Cards[0].Count;
            for (int col = 0; col < numColumns; col++)
            {
                var columnCards = new List<Card>();
                for (int row = 0; row < numRows; row++)
                {
                    columnCards.Add(Cards[row][col]);
                }

                columnCards = columnCards.OrderBy(c => rand.Next()).ToList();
                for (int row = 0; row < numRows; row++)
                {
                    Cards[row][col] = columnCards[row];
                }
            }
        }
    }
 }

