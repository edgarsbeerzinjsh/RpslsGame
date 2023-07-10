namespace RoPaScLiSp.GameLogic
{
    public static class MatchService
    {
        public static void PlayMatch(Player playerA, Player playerB, int roundCount)
        {
            Console.WriteLine($"\n{playerA.Name} vs {playerB.Name}");
            var currentRound = 1;
            var aWins = 0;
            var bWins = 0;

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

            var matchWinner = aWins > bWins ? playerA : playerB;
            matchWinner.WinMatch();

            Console.WriteLine($"{matchWinner.Name} won this match!");
            RoundService.DrawStripedLine();
        }
    }
}
