namespace RoPaScLiSp.GameLogic
{
    public static class MatchService
    {
        public static void PlayMatch(Player playerA, Player playerB, int roundCount)
        {
            Console.WriteLine($"\n{playerA.Name} vs {playerB.Name}");
            var showInfo = playerA.IsHuman || playerB.IsHuman;
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
                RoundService.DrawStripedLine();
                Console.WriteLine($"Round {currentRound}\n");
                var winner = RoundService.PlayOneRound(playerA, playerB);
                if (winner == playerA)
                {
                    aWins++;
                }
                else
                {
                    bWins++;
                }

                winner.WinRound();
                Console.WriteLine($"\nRound winner {winner.Name}!\n");
                Console.WriteLine($"After {currentRound}. round:");
                Console.WriteLine($"{playerA.Name}: {aWins}");
                Console.WriteLine($"{playerB.Name}: {bWins}\n");
            }

            if (aWins == bWins)
            {
                RoundService.DrawStripedLine();
                Console.WriteLine("Match can not end with tie!\nYou have to play extra round!\n");
                var extraRound = RoundService.PlayOneRound(playerA, playerB);
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
            RoundService.DrawStripedLine();
        }
    }
}
