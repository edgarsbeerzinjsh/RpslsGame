using RoPaScLiSp.Setup;

namespace RoPaScLiSp.GameLogic
{
    public class PlayGame
    {
        private readonly TournamentService _tournamentService;

        public PlayGame()
        {
            var setup = new GameSetup();
            var (participantList, rounds) = setup.NewGameSetup();

            _tournamentService = new TournamentService(participantList, rounds);
        }

        public void Start()
        {
            _tournamentService.PlayTournament();
            _tournamentService.GetTournamentResults();
        }
    }
}
