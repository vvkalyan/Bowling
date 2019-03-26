using System.Collections.Generic;

namespace BowlingGame
{
    public interface IGame
    {
        void Start(); 
        void Roll(int pins);
        int ScoreByFrame(int frameNo);
        int TotalScore { get; }
        void LoadFrames();
        List<IFrame> GetFrames();
        string ScoreString { get; set; }
    }
}