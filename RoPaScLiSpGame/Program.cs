using RpslsGame.HelperFunctions;
using RpslsGame.Players;

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

        private enum RoundResult
        {
            Draw,
            PlayerA,
            PlayerB,
        }

        private enum OpponentNames
        {
            Spock,
            Rock,
            Lizard,
        }

        static void Main(string[] args)
        {
            var gameLength = 3;
            var you = new HumanPlayer("You");
            var computer = new AIPlayer("Computer");
            var currentRound = 1;
            var opponentNr = 0;
            var random = new Random();
            Array gameTurnOptions = Enum.GetValues(typeof(TurnOptions));


            Console.WriteLine("Hi, you are participating in championship of\n" +
                              "ROCK, PAPER, SCISSORS, LIZARD, SPOCK\n");
            Console.WriteLine("Game consists of three winning rounds and winner is player " +
                              "with most wins!\nGood luck!");
            Console.WriteLine("To be champion you must win against three opponents!");

            do
            {
                Console.WriteLine($"\nYour {opponentNr + 1}. opponent is {(OpponentNames)opponentNr}");
                NewGame();
                OneOpponentGame();

                opponentNr++;
            } while (opponentNr < 3);

            if (you.MatchWins > computer.MatchWins)
            {
                Console.WriteLine("Congratulations you are champion!");
            }

            Console.ReadLine();

            void NewGame()
            {
                you.RoundWins = 0;
                computer.RoundWins = 0;
                currentRound = 1;
            }
            void OneOpponentGame()
            {
                OpponentNames currentOpponent = (OpponentNames)opponentNr;
                var opponentName = currentOpponent.ToString();

                do
                {
                    Console.WriteLine($"\nGame {opponentNr + 1}, Round {currentRound}");

                    var playerTurnOption = you.MakeTurnDecision<TurnOptions>();
                    Console.WriteLine($"You chose: {playerTurnOption}");

                    //var computerTurnOption = TurnOptions.Rock;
                    var computerTurnOption = computer.MakeTurnDecision<TurnOptions>();
                    Console.WriteLine($"{opponentName} chose: {computerTurnOption}");

                    var roundWinner = TurnWinner((TurnOptions)playerTurnOption, computerTurnOption);

                    if (roundWinner == RoundResult.Draw)
                    {
                        Console.WriteLine("\nBoth players chose the same option: It is a draw!" +
                                          "\nPlay this round again!");
                        continue;
                    }
                    else if (roundWinner == RoundResult.PlayerA)
                    {
                        Console.WriteLine($"\n{playerTurnOption} wins against {computerTurnOption}!" +
                                          $"\nYou win this round!");
                        you.WinRound();
                    }
                    else
                    {
                        Console.WriteLine($"\n{computerTurnOption} wins against {playerTurnOption}!" +
                                          $"\n{opponentName} wins this round!");
                        computer.WinRound();
                    }
                    
                    currentRound++;
                    Console.WriteLine($"\nYour wins: {you.RoundWins}\n" +
                                      $"{opponentName} wins: {computer.RoundWins}");

                } while (currentRound <= gameLength);

                var matchWinner = you.Name;
                if (you.RoundWins > computer.RoundWins)
                {
                    you.WinMatch();
                }
                else
                {
                    computer.WinMatch();
                    matchWinner = computer.Name;
                }

                Console.WriteLine($"{matchWinner} won this match!");
            }
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