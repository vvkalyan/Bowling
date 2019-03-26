using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class Game : IGame
    {
        int maxFrameCount;
        int throwsPerFrame;
        int NoOfPinsforStrike;
        private IFrame frame;
        private IOptions<GameConfig> gameconfig;
        List<IFrame> frames;
        int currentFramecounter=1;
        public string ScoreString { get; set; }

        //constructor only for Unit Tests. Once DI is configured for unit tests we can refactor this
        public Game(IFrame frame)
        {
            this.frame = frame;
            maxFrameCount = 10;
            throwsPerFrame = 2;
            NoOfPinsforStrike = 10;
            frames = new List<IFrame>();
        }
            
        public Game(IOptions<GameConfig> config, IFrame frame)
        {
            this.frame = frame;
            this.gameconfig = config;
            int.TryParse(gameconfig.Value.FramesPerGame.ToString(), out maxFrameCount);
            int.TryParse(gameconfig.Value.ThrowsPerFrame.ToString(), out throwsPerFrame);
            int.TryParse(gameconfig.Value.NoOfPinsforStrike.ToString(), out NoOfPinsforStrike);
            frames = new List<IFrame>();

        }

        public void Start()
        {
            
        }
        public void Roll(int pins)
        {
            //results.Add(pins);
        }
        public int ScoreByFrame(int frameNo)
        {
            return frames.Where(f => f.Number == frameNo).FirstOrDefault().TotalScore;

        }
        public int TotalScore
        {
            get
            {
                int currentScore = 0;
                foreach (var frame in frames)
                {
                    frame.TotalScore = frame.Score();
                    if (frame.IsStrike())
                    {
                        frame.TotalScore += SumNextTwoThrows(frame);
                    }
                    else if (frame.IsSpare())
                    {
                        frame.TotalScore += NextFrame(frame).GetFirstThrow();
                    }
                    currentScore += frame.TotalScore;
                    frame.TotalScore = currentScore;
                }
                return currentScore;
            }
        }

        private int SumNextTwoThrows(IFrame frame)
        {

            var nextFrame = NextFrame(frame);
            if (nextFrame.Number == maxFrameCount)
            {
                 return nextFrame.ThrowsPerFrame() == 3 ? nextFrame.GetFirstTwoThrows() : nextFrame.Score();   
            }
            else
            {
                var nextplusOneFrame = NextFrame(nextFrame);
                return nextFrame.IsStrike() ? nextFrame.Score() + nextplusOneFrame.GetFirstThrow() : nextFrame.Score();
            }
        }
        private IFrame NextFrame(IFrame frame)
        {
            return frames.Skip(frame.Number).Take(1).FirstOrDefault();   
        }
        
        public void LoadFrames()
        {
            List<int> scores = ScoreString.Split(',').Select(s => int.Parse(s)).ToList();
            frame  = AddFrames(); 
            for (int i = 0; i < scores.Count; i++)
            {
                int noOfPins = scores[i];
                frame.Load(noOfPins);
                //special rule for last frame
                if (frame.Number == maxFrameCount && (frame.IsStrike() || frame.IsSpare()))
                    frame.IncreaseThrowCount(); 
                if (frame.IsStrike() || frame.ThrowCount() == frame.ThrowsPerFrame())
                {
                    frame = AddFrames();
                }
            }
        }

        private IFrame AddFrames()
        {
            if (currentFramecounter <= maxFrameCount)
            {
                frame = frame.Add(currentFramecounter);
                frames.Add(frame);
                currentFramecounter++;
                return frame;
            }
            return frame;
        }
        /// <summary>
        /// Debugging
        /// </summary>
        /// <returns></returns>
        public List<IFrame> PrintFramesScores()
        {
            for (int i = 0; i < frames.Count; i++)
            {
                Debug.WriteLine("Frame" + i.ToString());
                Debug.WriteLine("Score" + frames[i].Score());
            }
            return frames;
        }

        public List<IFrame> GetFrames()
        {
            return frames;
        }

    }
   
}
