namespace RpslsGame
{
    internal class Program
    {
        private enum TurnOptions
        {
            Rock = 0,
            Paper = 1,
            Scissors = 2,
            Lizard = 3,
            Spock = 4,
        }
        
        static void Main(string[] args)
        {
            var gameLength = 3;
            var yourWins = 0; 
            var computerWins = 0;
            var currentRound = 1;
            var random = new Random();
            Array gameTurnOptions = Enum.GetValues(typeof(TurnOptions));


            Console.WriteLine("Hi, you are participating in championship of\n" +
                              "ROCK, PAPER, SCISSORS, LIZARD, SPOCK");

            do
            {
                Console.WriteLine($"\nRound {currentRound}");
                Console.WriteLine("Choose your option:\n");

                foreach (var turnOption in gameTurnOptions)
                {
                    var firstTwoLetters = FirstTwoLettersOfTurnOption((TurnOptions)turnOption);

                    Console.WriteLine($"{turnOption} ({turnOption} / {firstTwoLetters} / {(int)turnOption})");
                }

                if (currentRound == 1)
                {
                    Console.WriteLine("\nTo exit game: exit / ex");
                }

                var playerTurnOption = ReadPlayerTurnOption(currentRound);

                if (playerTurnOption == "")
                {
                    Console.WriteLine("\nOoh, maybe another time.");
                    Environment.Exit(0);
                }

                Console.WriteLine($"You chose: {playerTurnOption}");

                var computerTurnOption = (TurnOptions)random.Next(gameTurnOptions.Length);
                Console.WriteLine($"Computer chose: {computerTurnOption}");

                yourWins++;
                currentRound++;
                Console.WriteLine($"\nYour wins: {yourWins}\n" +
                                  $"Computer wins: {computerWins}");

            } while (currentRound <= gameLength);

            Console.WriteLine($"Winner is ...");

            string ReadPlayerTurnOption(int round)
            {
                var isValidOption = false;
                string playerOption = "";
                do
                {
                    var playerInput = Console.ReadLine().ToLower();
                    foreach (var turnOption in gameTurnOptions)
                    {
                        var turnOptionAsInt = (int)turnOption;

                        if (playerInput == turnOption.ToString().ToLower() ||
                            playerInput == FirstTwoLettersOfTurnOption((TurnOptions)turnOption) ||
                            playerInput == turnOptionAsInt.ToString())
                        {
                            playerOption = turnOption.ToString();
                            isValidOption = true;
                            break;
                        }
                    }

                    if ((playerInput == "exit" || playerInput == "ex") && round == 1)
                    {
                        isValidOption = true;
                    }

                    if (!isValidOption)
                    {
                        Console.WriteLine("That was not one of options ...");
                    }
                } while (!isValidOption);

                return playerOption;
            }


        }
        private static string FirstTwoLettersOfTurnOption<T>(T turnOption) where T : Enum
        {
            return turnOption.ToString().Substring(0, 2).ToLower();
        }
    }
}