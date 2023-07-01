namespace RpslsGame
{
    public class Player
    {
        public string Name { get; set; }
        public int Wins { get; set; } = 0;
        public int Loses { get; set; } = 0;

        public Player(string name)
        {
            Name = name;
        }


    }
}
