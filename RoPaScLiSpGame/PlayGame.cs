using RpslsGame.GameLogic;

namespace RpslsGame
{
    public class PlayGame
    {
        private readonly RpslsGameSetup _setup;
        private readonly TournamentService _tournamentService;

        public PlayGame()
        {
            _setup = new RpslsGameSetup();
            var parameters = _setup.GameSetup();

            _tournamentService = new TournamentService(parameters.participantList, parameters.rounds);
        }

        public void Start()
        {
            _tournamentService.PlayTournament();
            _tournamentService.GetTournamentResults();
        }
    }
}
