using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingGame.Services
{
    public class BowlingGameService
    {
        private IGame game;
        private readonly IFrame frame;
        private readonly IOptions<GameConfig> config;
        

        public BowlingGameService(IOptions<GameConfig> config, IFrame frame)
        {
            this.config = config;
            this.frame = frame;
            game = new Game(config, frame);
            
        }
        public void SetScoreString(string scores)
        {
            game.ScoreString = scores;
        }
        public int TotalScore()
        {
            game.Start();
            return game.TotalScore;

        }
        public List<string> ScoresByFrame(int? frameNo)
        {
            List<string> results = new List<string>();

            if (frameNo != null)
            {
                results.Add("Score of Frame #" + frameNo.ToString() + " is " + game.ScoreByFrame(frameNo.GetValueOrDefault()));
            }
            else
            {
                for (int i = 1; i <= game.GetFrames().Count; i++)
                {
                    results.Add("Score of Frame #" + i.ToString() + " is " + game.ScoreByFrame(i));
                    if (game.ScoreByFrame(i) == game.TotalScore) break; 
                }
            }
            return results;
        }
    }
}
