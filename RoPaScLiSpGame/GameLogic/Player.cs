namespace RoPaScLiSp.GameLogic
{
    public class Player
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public int RoundWins { get; set; }
        public int MatchWins { get; set; }

        public Player(string name, bool isHuman)
        {
            Name = name;
            IsHuman = isHuman;
            RoundWins = 0;
            MatchWins = 0;
        }

        public void WinRound()
        {
            RoundWins++;
        }

        public void WinMatch()
        {
            MatchWins++;
        }
    }
}
