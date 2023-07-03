using RpslsGame.HelperFunctions;

namespace RpslsGame.Players
{
    public class AIPlayer : Player
    {
        private Random Random { get; set; } = new Random();

        public AIPlayer(string name) : base(name)
        {
        }

        public T MakeTurnDecision<T>() where T : Enum
        {
            Array enumValues = Enum.GetValues(typeof(T));
            T firstValue =  (T)enumValues.GetValue(0);
            return firstValue;
        }
    }
}
