using RpslsGame.GameLogic.Constants;
using RpslsGame.Players;

namespace RpslsGame.GameLogic
{
    public static class MatchService
    {
        public static void PlayMatch(Player playerA, Player playerB, int roundCount)
        {
            Console.WriteLine($"\n{playerA.Name} vs {playerB.Name}");
            var showInfo = playerA.GetType() == typeof(HumanPlayer) || playerB.GetType() == typeof(HumanPlayer);
            var currentRound = 1;
            var aWins = 0;
            var bWins = 0;
            var standardOutput = Console.Out;
            var standardError = Console.Error;

            if (!showInfo)
            {
                Console.SetOut(TextWriter.Null);
                Console.SetError(TextWriter.Null);
            }

            for (var i = 1; i <= roundCount; i++, currentRound++)
            {
                DrawStripedLine();
                Console.WriteLine($"Round {currentRound}\n");
                var winner = PlayOneRound(playerA, playerB);
                if (winner == playerA)
                {
                    aWins++;
                    playerA.WinRound();
                }
                else
                {
                    bWins++;
                    playerB.WinRound();
                }

                Console.WriteLine($"\nRound winner {winner.Name}!\n");
                Console.WriteLine($"After {currentRound}. round:");
                Console.WriteLine($"{playerA.Name}: {aWins}");
                Console.WriteLine($"{playerB.Name}: {bWins}\n");
            }

            if (aWins == bWins)
            {
                DrawStripedLine();
                Console.WriteLine($"Match can not end with tie!\nYou have to play extra round!\n");
                var extraRound = PlayOneRound(playerA, playerB);
                if (extraRound == playerA)
                {
                    aWins++;
                }
                else
                {
                    bWins++;
                }

                Console.WriteLine($"Extra round winner {extraRound.Name}!\n");
            }

            var matchWinner = aWins > bWins ? playerA : playerB;
            matchWinner.WinMatch();

            Console.SetOut(standardOutput);
            Console.SetError(standardError);
            Console.WriteLine($"{matchWinner.Name} won this match!");
            DrawStripedLine();
        }

        private static Player PlayOneRound(Player firstPlayer, Player secondPlayer)
        {
            RoundResult result;

            do
            {
                var firstPlayerChoice = firstPlayer.MakeTurnDecision<TurnOptions>();
                DrawStripedLine();
                PlayerChoiceText(firstPlayer.Name, firstPlayerChoice);

                var secondPlayerChoice = secondPlayer.MakeTurnDecision<TurnOptions>();
                PlayerChoiceText(secondPlayer.Name, secondPlayerChoice);

                result = TurnWinner(firstPlayerChoice, secondPlayerChoice);
                TurnResultText(firstPlayerChoice, secondPlayerChoice, result);

            } while (result == RoundResult.Draw);

            return result == RoundResult.PlayerA ? firstPlayer : secondPlayer;
        }

        private static void PlayerChoiceText(string name, TurnOptions choice)
        {
            Console.WriteLine($"{name} chose {choice}");
        }

        private static void TurnResultText(TurnOptions a, TurnOptions b, RoundResult result)
        {
            var win = "wins";
            if (result == RoundResult.Draw)
            {
                Console.WriteLine("Both players chose the same option: It is a draw!\nPlay this round again!\n");
            }
            else
            {
                if (result == RoundResult.PlayerB)
                {
                    win = "loses";
                }

                Console.WriteLine($"{a} {win} against {b}");
            }
        }

        private static void DrawStripedLine()
        {
            Console.WriteLine($"{new string('-', Console.WindowWidth)}");
        }

        private enum RoundResult
        {
            Draw,
            PlayerA,
            PlayerB,
        }

        private static RoundResult TurnWinner(TurnOptions aOption, TurnOptions bOption)
        {
            if (aOption == bOption)
            {
                return RoundResult.Draw;
            }

            switch (aOption)
            {
                case TurnOptions.Rock:
                    return (bOption == TurnOptions.Scissors || bOption == TurnOptions.Lizard)
                        ? RoundResult.PlayerA
                        : RoundResult.PlayerB;

                case TurnOptions.Paper:
                    return (bOption == TurnOptions.Rock || bOption == TurnOptions.Spock)
                        ? RoundResult.PlayerA
                        : RoundResult.PlayerB;


                case TurnOptions.Scissors:
                    return (bOption == TurnOptions.Paper || bOption == TurnOptions.Lizard)
                        ? RoundResult.PlayerA
                        : RoundResult.PlayerB;

                case TurnOptions.Lizard:
                    return (bOption == TurnOptions.Paper || bOption == TurnOptions.Spock)
                        ? RoundResult.PlayerA
                        : RoundResult.PlayerB;

                case TurnOptions.Spock:
                    return (bOption == TurnOptions.Rock || bOption == TurnOptions.Scissors)
                        ? RoundResult.PlayerA
                        : RoundResult.PlayerB;
            }

            return RoundResult.Draw;
        }
    }
}
