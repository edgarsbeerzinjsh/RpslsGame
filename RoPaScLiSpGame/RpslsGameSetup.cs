using RpslsGame.GameLogic.Constants;
using RpslsGame.HelperFunctions;
using RpslsGame.Players;

namespace RpslsGame
{
    public class RpslsGameSetup
    {
        private const int MinOpponentCount = 1;
        private const int MaxOpponentCount = 9;
        private const int MinRoundCount = 1;
        private const int MaxRoundCount = 5;
        private readonly List<string> _nameArray = Enum.GetNames(typeof(OpponentNames)).ToList();

        public (List<Player> participantList, int rounds) GameSetup()
        {
            Console.WriteLine("Hi, you are participating in championship of\n" +
                              "ROCK, PAPER, SCISSORS, LIZARD, SPOCK\n");
            Console.WriteLine("Tournament winner is player with most wins (tiebreaker round wins)\n");
            Console.WriteLine("Each player will play against each other.");
            Console.WriteLine($"How many opponents do you want ({MinOpponentCount}-{MaxOpponentCount})?");
            var opponents = GetValidCount(MinOpponentCount, MaxOpponentCount);

            var participantList = CreatePlayerList(opponents);

            Console.WriteLine($"How many rounds in each match do you want to play({MinRoundCount}-{MaxRoundCount})?");
            var rounds = GetValidCount(MinRoundCount, MaxRoundCount);

            return (participantList, rounds);
        }

        private static int GetValidCount(int min, int max)
        {
            var isValidCount = false;
            string input;

            do
            {
                input = Console.ReadLine();

                if (UserInputValidations.IsInputNumber(input))
                {
                    input = UserInputValidations
                        .NumberRangeValidation(input, min, max);

                    if (input != null)
                    {
                        isValidCount = true;
                    }
                }

                if (!isValidCount)
                {
                    Console.WriteLine($"\nInput must be number between {min} and {max}!");
                }

            } while (!isValidCount);

            return int.Parse(input);
        }

        private List<Player> CreatePlayerList(int opponents)
        {
            var playerList = new List<Player>();

            playerList.Add(new HumanPlayer("Human"));

            ShuffleNameList();

            for (int i = 0; i < opponents; i++)
            {
                playerList.Add(new AiPlayer(_nameArray[i]));
            }

            return playerList;
        }

        private void ShuffleNameList()
        {
            var random = new Random();
            var n = _nameArray.Count;

            while (n > 1)
            {
                n--;
                int oldValueIndex = random.Next(n + 1);
                string value = _nameArray[oldValueIndex];
                _nameArray[oldValueIndex] = _nameArray[n];
                _nameArray[n] = value;
            }
        }
    }
}
