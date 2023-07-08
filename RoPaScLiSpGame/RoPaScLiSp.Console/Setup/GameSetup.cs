using RoPaScLiSp.GameLogic;
using RoPaScLiSp.HelperFunctions;

namespace RoPaScLiSp.Setup
{
    public class GameSetup
    {
        public (List<Player> participantList, int rounds) NewGameSetup()
        {
            Console.WriteLine("Hi, you are participating in championship of\n" +
                                     "ROCK, PAPER, SCISSORS, LIZARD, SPOCK\n");
            Console.WriteLine("Tournament winner is player with most wins (tiebreaker round wins)\n");
            Console.WriteLine("Each player will play against each other.");
            Console.WriteLine($"How many opponents do you want ({GameConstants.MinOpponentCount}-{GameConstants.MaxOpponentCount})?");
            var opponents = GetValidCount(GameConstants.MinOpponentCount, GameConstants.MaxOpponentCount);

            var participantList = CreatePlayerList(opponents);

            Console.WriteLine($"How many rounds in each match do you want to play({GameConstants.MinRoundCount}-{GameConstants.MaxRoundCount})?");
            var rounds = GetValidCount(GameConstants.MinRoundCount, GameConstants.MaxRoundCount);

            return (participantList, rounds);
        }

        private static int GetValidCount(int min, int max)
        {
            var isValidCount = false;
            string input;

            do
            {
                input = Console.ReadLine();

                if (int.TryParse(input, out var inputNumber))
                {
                    if (UserInputValidations.IsNumberInRangeValidation(inputNumber, min, max))
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
            var playerList = new List<Player>
            {
                new Player("Human", true)
            };

            var opponentNames = GameConstants.OpponentNameArray.ToList();
            ShuffleNameList(opponentNames);

            for (var i = 0; i < opponents; i++)
            {
                playerList.Add(new Player(opponentNames[i], false));
            }

            return playerList;
        }

        private void ShuffleNameList(List<string> oldList)
        {
            var random = new Random();
            var n = oldList.Count;

            while (n > 1)
            {
                n--;
                int oldValueIndex = random.Next(n + 1);
                string value = oldList[oldValueIndex];
                oldList[oldValueIndex] = oldList[n];
                oldList[n] = value;
            }
        }
    }
}
