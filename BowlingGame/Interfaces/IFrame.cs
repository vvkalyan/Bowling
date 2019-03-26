namespace BowlingGame
{
    public interface IFrame
    {
        bool IsSpare();
        bool IsStrike();
        void Load(int pinCount);
        IFrame Add(int number);
        int Score();
        int ThrowCount();
        void IncreaseThrowCount();
        int Number { get; set; }
        int ThrowsPerFrame();
        int TotalScore { get; set; }
        int GetFirstTwoThrows();
        int GetFirstThrow();

    }
}