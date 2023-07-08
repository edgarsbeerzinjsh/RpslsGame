using RoPaScLiSp.HelperFunctions;

namespace RoPaScLiSp.GameLogic
{
    public static class RoundService
    {
        public static void DrawStripedLine()
        {
            Console.WriteLine($"{new string('-', Console.WindowWidth)}");
        }

        public static Player PlayOneRound(Player firstPlayer, Player secondPlayer)
        {
            string result;

            do
            {
                var firstPlayerChoice = MakeTurnDecision(firstPlayer);
                var secondPlayerChoice = MakeTurnDecision(secondPlayer);
                result = GameRoundDecision.TurnWinner(firstPlayerChoice, secondPlayerChoice);

                DrawStripedLine();
                PlayerChoiceText(firstPlayer.Name, firstPlayerChoice);
                PlayerChoiceText(secondPlayer.Name, secondPlayerChoice);
                TurnResultText(firstPlayerChoice, secondPlayerChoice, result);

            } while (result == GameRoundDecision.Draw);

            return result == GameRoundDecision.AWins ? firstPlayer : secondPlayer;
        }

        private static string MakeTurnDecision(Player player)
        {
            if (player.IsHuman)
            {
                return ValidTurnDecision();
            }

            return RandomTurnDecision();
        }

        private static string RandomTurnDecision()
        {
            var random = new Random();
            var randomValidNumber = random.Next(GameRoundDecision.DecisionOptionCount);

            return GameRoundDecision.DecisionOptionNames[randomValidNumber];
        }

        private static string ValidTurnDecision()
        {
            Console.WriteLine("Choose your option:");
            Console.WriteLine("([exit] or [ex] to exit from game)\n");

            foreach (var turnOption in GameRoundDecision.DecisionOptionNames)
            {
                var firstTwoLetters = FirstTwoLettersOfString(turnOption);
                Console.WriteLine($"{turnOption} ({turnOption} / {firstTwoLetters} / {GameRoundDecision.DecisionOptionNames.IndexOf(turnOption)})");
            }

            var input = GetValidInput();

            return input;
        }

        private static string FirstTwoLettersOfString(string word)
        {
            return word.Substring(0, 2);
        }

        private static string? DecisionNameFromFirstTwoLetters(string input)
        {
            foreach (var turnOption in GameRoundDecision.DecisionOptionNames)
            {
                if (input.ToLower() == FirstTwoLettersOfString(turnOption).ToLower())
                {
                    return turnOption;
                }
            }

            return null;
        }

        private static string GetValidInput()
        {
            var validInput = false;
            string input;
            do
            {
                input = Console.ReadLine().Trim().ToLower();
                var inputTwoCharOption = DecisionNameFromFirstTwoLetters(input);

                if (input == "exit" || input == "ex")
                {
                    Console.WriteLine("\nOoh, maybe another time.");
                    Environment.Exit(0);
                }
                else if (GameRoundDecision.IsTurnDecisionValid(input))
                {
                    validInput = true;
                }
                else if (inputTwoCharOption != null)
                {
                    validInput = true;
                    input = inputTwoCharOption;
                }
                else if (int.TryParse(input, out var numberInput))
                {
                    if (UserInputValidations.IsNumberInRangeValidation(numberInput, 0, GameRoundDecision.DecisionOptionCount - 1))
                    {
                        input = GameRoundDecision.DecisionOptionNames[numberInput];
                        validInput = true;
                    }
                }

                if (!validInput)
                {
                    Console.WriteLine("That was not one of options...");
                }

            } while (!validInput);

            return input;
        }

        private static void PlayerChoiceText(string name, string choice)
        {
            Console.WriteLine($"{name} chose {choice}");
        }

        private static void TurnResultText(string a, string b, string result)
        {
            var win = "wins";
            if (result == GameRoundDecision.Draw)
            {
                Console.WriteLine("Both players chose the same option: It is a draw!\nPlay this round again!\n");
            }
            else
            {
                if (result == GameRoundDecision.BWins)
                {
                    win = "loses";
                }

                Console.WriteLine($"{a} {win} against {b}");
            }
        }
    }
}

