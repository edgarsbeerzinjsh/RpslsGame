namespace RoPaScLiSp.Setup
{
    public enum OpponentNames
    {
        Rocky,
        Edward,
        Spock
    }

    public static class GameConstants
    {
        public const int Opponents = 3;
        public const int Rounds = 3;
        public static readonly List<string> OpponentNameArray = Enum.GetNames(typeof(OpponentNames)).ToList();
    }
}
