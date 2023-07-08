using ConsoleTables;

namespace RoPaScLiSp.GameLogic
{
    public class TournamentService
    {
        private readonly List<Player> _playerList;
        private readonly int _roundCount;

        public TournamentService(List<Player> playerList, int roundCount)
        {
            _playerList = playerList;
            _roundCount = roundCount;
        }

        public void PlayTournament()
        {
            for (var i = 0; i < _playerList.Count - 1; i++)
            {
                for (var j = i + 1; j < _playerList.Count; j++)
                {
                    MatchService.PlayMatch(_playerList[i], _playerList[j], _roundCount);
                }

                if (_playerList[i].IsHuman)
                {
                    Console.WriteLine($"All {_playerList[i].Name} matches played!");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        public void GetTournamentResults()
        {
            var statistics = OrderPlayersByWins();
            Console.Clear();
            Console.WriteLine("\nTOURNAMENT RESULTS");

            var resultTable = new ConsoleTable("Wins", "RoundWins", "Name");
            statistics.ForEach(x => resultTable.AddRow(x.MatchWins, x.RoundWins, x.Name));

            Console.WriteLine(resultTable);
            Console.WriteLine($"\nTournament winner is {statistics[0].Name} with {statistics[0].MatchWins} wins");
        }

        private List<Player> OrderPlayersByWins()
        {
            return _playerList.OrderByDescending(p => p.MatchWins)
                .ThenByDescending(p => p.RoundWins).ToList();
        }
    }
}
