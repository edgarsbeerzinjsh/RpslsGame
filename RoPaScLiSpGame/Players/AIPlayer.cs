using RpslsGame.HelperFunctions;

namespace RpslsGame.Players
{
    public class AiPlayer : Player
    {
        private Random Random { get; } = new Random();

        public AiPlayer(string name) : base(name)
        {
        }

        public override T MakeTurnDecision<T>()
        {
            var enumValues = EnumConversions.EnumToArray<T>();
            var randomValidNumber = Random.Next(enumValues.Length);

            return (T)enumValues.GetValue(randomValidNumber);
        }
    }
}
