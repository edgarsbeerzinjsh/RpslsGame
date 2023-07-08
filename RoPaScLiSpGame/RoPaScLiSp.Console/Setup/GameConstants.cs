namespace RoPaScLiSp.Setup
{
    public enum OpponentNames
    {
        Rocky,
        Edward,
        Spock,
        Gecko,
        Wolverine,
        Godzilla,
        Rock,
        Luke,
        Sherlock
    }

    public static class GameConstants
    {
        public const int MinOpponentCount = 1;
        public const int MaxOpponentCount = 9;
        public const int MinRoundCount = 1;
        public const int MaxRoundCount = 5;
        public static readonly List<string> OpponentNameArray = Enum.GetNames(typeof(OpponentNames)).ToList();
    }
}
