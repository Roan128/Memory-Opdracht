namespace Memory.Model
{
    public class Player
    {
        public string Name { get; set; } = "";

        public int CardAmount { get; set; }

        //Console constructor
        public Player(string name)
        {
            Name = name;
            CardAmount = 5;
        }

        //Gui constructor
        public Player(string name, int cardamount)
        {
            Name = name;
            CardAmount = cardamount;
    }
}