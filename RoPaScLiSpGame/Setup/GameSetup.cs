using RoPaScLiSp.GameLogic;

namespace RoPaScLiSp.Setup
{
    public class GameSetup
    {
        public (List<Player> participantList, int rounds) NewGameSetup()
        {
            Console.WriteLine("Hi, you are participating in championship of\n" +
                                     "ROCK, PAPER, SCISSORS, LIZARD, SPOCK\n");
            Console.WriteLine("You play vs opponent three rounds. Winner is champion\n");

            var participantList = CreatePlayerList(GameConstants.Opponents);

            return (participantList, GameConstants.Rounds);
        }

        private List<Player> CreatePlayerList(int opponents)
        {
            var playerList = new List<Player>
            {
                new Player("Human", true)
            };

            var opponentNames = GameConstants.OpponentNameArray.ToList();

            for (var i = 0; i < opponents; i++)
            {
                playerList.Add(new Player(opponentNames[i], false));
            }

            return playerList;
        }
    }
}
