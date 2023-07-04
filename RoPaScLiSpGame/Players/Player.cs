namespace RpslsGame.Players
{
    public abstract class Player
    {
        public string Name { get; set; }
        public int RoundWins { get; set; }
        public int MatchWins { get; set; }

        protected Player(string name)
        {
            Name = name;
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

        public abstract T MakeTurnDecision<T>() where T : Enum;
    }
}
