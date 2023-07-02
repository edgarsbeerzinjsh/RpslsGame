﻿using System.Diagnostics;

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
            var yourWins = 0; 
            var computerWins = 0;
            var currentRound = 1;
            var didWinRound = true;
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
            } while (didWinRound && opponentNr < 3);

            if (didWinRound == true)
            {
                Console.WriteLine("Congratulations you are champion!");
            }

            Console.ReadLine();

            void NewGame()
            {
                yourWins = 0;
                computerWins = 0;
                currentRound = 1;
            }
            void OneOpponentGame()
            {
                OpponentNames currentOpponent = (OpponentNames)opponentNr;
                var opponentName = currentOpponent.ToString();

                do
                {
                    Console.WriteLine($"\nGame {opponentNr + 1}, Round {currentRound}");
                    Console.WriteLine("Choose your option:\n");

                    foreach (var turnOption in gameTurnOptions)
                    {
                        var firstTwoLetters = FirstTwoLettersOfTurnOption((TurnOptions)turnOption);

                        Console.WriteLine($"{turnOption} ({turnOption} / {firstTwoLetters} / {(int)turnOption})");
                    }

                    if (currentRound == 1)
                    {
                        Console.WriteLine("\nOnly in first round you also have option" +
                                          "\nto exit game: exit / ex");
                    }

                    var playerTurnOption = ReadPlayerTurnOption(currentRound);

                    if (playerTurnOption == null)
                    {
                        Console.WriteLine("\nOoh, maybe another time.");
                        Environment.Exit(0);
                    }

                    Console.WriteLine($"You chose: {playerTurnOption}");

                    //var computerTurnOption = TurnOptions.Rock;
                    var computerTurnOption = (TurnOptions)random.Next(gameTurnOptions.Length);
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
                        yourWins++;
                    }
                    else
                    {
                        Console.WriteLine($"\n{computerTurnOption} wins against {playerTurnOption}!" +
                                          $"\n{opponentName} wins this round!");
                        computerWins++;
                    }
                    
                    currentRound++;
                    Console.WriteLine($"\nYour wins: {yourWins}\n" +
                                      $"{opponentName} wins: {computerWins}");

                } while (currentRound <= gameLength);

                if (yourWins > computerWins)
                {
                    Console.WriteLine($"You won {opponentNr + 1}. game!");
                }
                else
                {
                    Console.WriteLine($"{opponentName} won this game!\n" +
                                      $"You lost all tournament!");
                    didWinRound = false;
                }
            }

            TurnOptions? ReadPlayerTurnOption(int round)
            {
                var isValidOption = false;
                TurnOptions? playerOption = null;

                do
                {
                    var playerInput = Console.ReadLine().Trim().ToLower();
                    var inputTwoCharOption = EnumNameFromFirstTwoLettersCoversation(playerInput);
                    if (inputTwoCharOption != null)
                    {
                        playerInput = inputTwoCharOption;
                    }

                    if (IsPlayerInputNumber(playerInput))
                    {
                        playerInput = ValidPlayerInputNumber(playerInput);
                    }

                    var isPlayerOptionParsed = Enum.TryParse(playerInput, true, out TurnOptions playerParsedOption);
                    if (isPlayerOptionParsed)
                    {
                        playerOption = playerParsedOption;
                        isValidOption = true;
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


            string EnumNameFromFirstTwoLettersCoversation(string firstTwoLetters)
            {
                foreach (var turnOption in gameTurnOptions)
                {
                    if (firstTwoLetters.ToLower() == FirstTwoLettersOfTurnOption((TurnOptions)turnOption))
                    {
                        return turnOption.ToString();
                    }
                }

                return null;
            }

            bool IsPlayerInputNumber(string playerInput)
            {
                return Int32.TryParse(playerInput, out _);
            }

            string ValidPlayerInputNumber(string playerInput)
            {
                if (Int32.TryParse(playerInput, out int playerInputNumber))
                {
                    if (playerInputNumber >= 0 && playerInputNumber < gameTurnOptions.Length)
                    {
                        return playerInput;
                    }
                };

                return null;
            }
        }
        private static string FirstTwoLettersOfTurnOption<T>(T turnOption) where T : Enum
        {
            return turnOption.ToString().Substring(0, 2).ToLower();
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