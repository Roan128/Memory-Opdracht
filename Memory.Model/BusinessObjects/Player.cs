namespace Memory.BLL.BusinessObjects;

public class Player
{
    public string Name { get; set; } = "";

    public string CardAmount { get; set; }

    //Console constructor
    public Player(string name)
    {
        Name = name;
        CardAmount = "5";
    }

    //Gui constructor
    public Player(string name, string cardamount)
    {
        Name = name;
        CardAmount = cardamount;
    }
}