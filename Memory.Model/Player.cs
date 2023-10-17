namespace Memory.Model
{
    public class Player
    {
        public string Name { get; set; } = "";

        public int CardAmount { get; set; }

        public Player(string name)
        {
            Name = name;
            CardAmount = 5;
        }
    }
}