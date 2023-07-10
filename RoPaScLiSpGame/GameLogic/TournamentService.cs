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
            for (var i = 1; i < _playerList.Count; i++)
            {
                
                if (_playerList[0].MatchWins == i - 1)
                {
                    MatchService.PlayMatch(_playerList[0], _playerList[i], _roundCount);
                }
                else
                {
                    break;
                }
            }
        }

        public void GetTournamentResults()
        {
            var result = _playerList[0].MatchWins == _playerList.Count - 1
                ? "Congratulations! You won!"
                : "You lost! Your tournament is over!";
            
            Console.WriteLine(result);
        }
    }
}
