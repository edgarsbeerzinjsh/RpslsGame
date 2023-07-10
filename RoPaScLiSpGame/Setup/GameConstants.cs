namespace RoPaScLiSp.Setup
{
    public enum OpponentNames
    {
        Rocky
    }

    public static class GameConstants
    {
        public const int Opponents = 1;
        public const int Rounds = 3;
        public static readonly List<string> OpponentNameArray = Enum.GetNames(typeof(OpponentNames)).ToList();
    }
}
