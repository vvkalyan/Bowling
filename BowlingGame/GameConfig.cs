using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class GameConfig
    {
        public int   ThrowsPerFrame {get;set;}
        public int FramesPerGame { get; set; }
        public string GameName { get; set; }
        public int NoOfPinsforStrike { get; set; }
        public int NoOfPinsforSpare {get;set;}
        public int NoOfPins { get; set; }
    }
}
