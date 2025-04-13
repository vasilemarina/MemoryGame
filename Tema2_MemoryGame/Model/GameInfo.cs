using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tema2_MemoryGame.Model
{
    [Serializable]
    public class GameInfo
    {
        public GameCategory Category { get; set; }
        public ObservableCollection<ObservableCollection<Card>> Cards { get; set; }
        public int RemainingTime { get; set; }
        public int ElapsedTime { get; set; }
        public string Username { get; set; }
        public byte MatchedCards { get; set; }

        public GameInfo() { }
        public GameInfo(GameCategory category, ObservableCollection<ObservableCollection<Card>> cards,  int remainingTime,
             int elapsedTime, string username, byte matchedCards) {
            Category = category;
            Cards = cards;
            RemainingTime = remainingTime;
            ElapsedTime = elapsedTime;
            Username = username;
            MatchedCards = matchedCards;
        }
    }
}
