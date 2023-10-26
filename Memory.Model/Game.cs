using System.Numerics;

namespace Memory.Model
{
    //Deze klasse misschien opdelen in twee klasses. Een met logica voor de console en een met logica voor de GUI.
    public class Game
    {
        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; } 

        public List<Card> Cards = new List<Card>{ };

        public int Attempts {  get; set; }

        //Game starten zodra player aangemaakt is en kaarten gemaakt zijn.
        public void StartGame(Player player)
        {
            GenerateCards(player.CardAmount);
            ShuffleCards(Cards);

            while (CheckIfGameOver())
            {
                DisplayCards();
                ChooseCards();
            }

            Console.WriteLine("Game over");
        }

        //Aanmaken van kaarten op basis van hoeveelheid kaarten. Dit verdubbelen en randomizen.
        public List<Card> GenerateCards(int cardamount)
        {
            int i = 0;
            int value = 0;
            while (Cards.Count() < (cardamount * 2))
            {
                if (i % 2 == 0)
                {
                    value++;
                }

                i++;
                Card card = new Card(i, value);
                Cards.Add(card);
            }
            return Cards;
        }

        //Kaarten printen met Id erbij.
        public void DisplayCards()
        {
            foreach(Card card in Cards) {
                if (card.TurnedOver)
                {
                    Console.WriteLine($" [X] Val= {card.CardValue}");
                } else {
                    Console.WriteLine($" [{card.Id}] ");
                }
            }
        }

        public void ChooseCards()
        {
            Console.WriteLine("Pick the first card from the available cards");
            Card choice1 = Cards[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Pick the second card from the available cards");
            Card choice2 = Cards[int.Parse(Console.ReadLine()) - 1];


            if (choice1.CardValue == choice2.CardValue)
            {
                choice1.TurnedOver = true;
                choice2.TurnedOver = true;
                Console.WriteLine("Correct!");
            } else {
                Console.WriteLine("Value is not equal, try again.");
            }
            Attempts++;
        }

        public bool CheckIfGameOver()
        {
            int i = 0;
            foreach (Card card in Cards)
            {
                if (card.TurnedOver)
                {
                    i++;
                }
            }
            
            if (i == Cards.Count())
            {
                EndTime = DateTime.Now; 
                return false;
            } else {
                return true;
            }
        }

        public List<Card> ShuffleCards(List<Card> cards)
        {
            return cards;
        }

        public void CheckIfCardExists(int choice)
        {

        }

    }
}
