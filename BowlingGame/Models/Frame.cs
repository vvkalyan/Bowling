using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class Frame : IFrame
    {
        private List<int> throws;
        private int throwsPerFrame;
        private int NoOfPinsforStrike;
        private int NoOfPinsforSpare;
        
        private IOptions<GameConfig> gameconfig;

        public Frame()
        {
            throwsPerFrame = 2;
            NoOfPinsforStrike = 10;
            NoOfPinsforSpare = 10;
            throws = new List<int>();
        }
       
        public Frame(int number, IOptions<GameConfig> config) : this()
        {
            if (config != null)
            {
                this.gameconfig = config;
                int.TryParse(gameconfig.Value.ThrowsPerFrame.ToString(), out throwsPerFrame);
                int.TryParse(gameconfig.Value.NoOfPinsforStrike.ToString(), out NoOfPinsforStrike);
                int.TryParse(gameconfig.Value.NoOfPinsforSpare.ToString(), out NoOfPinsforSpare);
            }
            this.Number = number;
            throws = new List<int>();
        }

        public IFrame Add(int number)
        {
            return new Frame(number, gameconfig); 
        }
        public int Number { get; set; }

        public int TotalScore { get; set; }

        public bool IsSpare()
        {
            return (throws.Count == throwsPerFrame && throws.Sum() == NoOfPinsforSpare);
        }
        
        public bool IsStrike()
        {
            return (throws.Count == 1 && throws.Sum() == NoOfPinsforStrike);
        }

        public int ThrowCount()
        {
            return throws.Count; 
        }
        
        public int Score()
        {
            int score = throws.Sum();
            if (score < 0 || score >30)
            {
                throw new Exception("Hmmm something is not working right!!"); 
            }
            
            return throws.Sum(); 
        }
        public int ThrowsPerFrame()
        {
            return throwsPerFrame; 
        }

        public int GetFirstThrow()
        {
            return throws.FirstOrDefault() ; 
        }

        public int GetFirstTwoThrows()
        {
            return throws.Take(2).Sum();
        }
        public void Load(int pinCount)
        {
            throws.Add(pinCount);
        }

        public void IncreaseThrowCount()
        {
            throwsPerFrame++; 
        }
    }
}
