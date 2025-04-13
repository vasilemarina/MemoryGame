using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_MemoryGame.Model
{
    public class GameInfoEventArgs : EventArgs
    {
        public GameInfo GameInfo { get; set; }

        public GameInfoEventArgs(GameInfo gameInfo)
        {
            GameInfo = gameInfo;
        }
    }
}
