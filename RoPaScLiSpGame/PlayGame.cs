using RpslsGame.GameLogic;

namespace RpslsGame
{
    public class PlayGame
    {
        private readonly TournamentService _tournamentService;

        public PlayGame()
        {
            var setup = new RpslsGameSetup();
            var (participantList, rounds) = setup.GameSetup();

            _tournamentService = new TournamentService(participantList, rounds);
        }

        public void Start()
        {
            _tournamentService.PlayTournament();
            _tournamentService.GetTournamentResults();
        }
    }
}
