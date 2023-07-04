using RpslsGame.GameLogic.Constants;
using RpslsGame.HelperFunctions;
using RpslsGame.Players;

namespace RpslsGame
{
    public class RpslsGameSetup
    {
        private int minOpponentCount = 1;
        private int maxOpponentCount = 9;
        private int minRoundCount = 1;
        private int maxRoundCount = 5;
        private List<string> nameArray = Enum.GetNames(typeof(OpponentNames)).ToList();

        public (List<Player> participantList, int rounds) GameSetup()
        {
            Console.WriteLine("Hi, you are participating in championship of\n" +
                              "ROCK, PAPER, SCISSORS, LIZARD, SPOCK\n");
            Console.WriteLine("Tournament winner is player with most wins (tiebreaker round wins)\n");
            Console.WriteLine("Each player will play against each other.");
            Console.WriteLine($"How many opponents do you want ({minOpponentCount}-{maxOpponentCount})?");
            var opponents = GetValidCount(minOpponentCount, maxOpponentCount);

            var participantList = CreatePlayerList(opponents);

            Console.WriteLine($"How many rounds in each match do you want to play({minRoundCount}-{maxRoundCount})?");
            var rounds = GetValidCount(minRoundCount, maxRoundCount);

            return (participantList, rounds);
        }

        private int GetValidCount(int min, int max)
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
                playerList.Add(new AiPlayer(nameArray[i]));
            }

            return playerList;
        }

        private void ShuffleNameList()
        {
            var random = new Random();
            var n = nameArray.Count;

            while (n > 1)
            {
                n--;
                int oldValueIndex = random.Next(n + 1);
                string value = nameArray[oldValueIndex];
                nameArray[oldValueIndex] = nameArray[n];
                nameArray[n] = value;
            }
        }
    }
}
