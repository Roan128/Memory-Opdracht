using Memory.BLL.BusinessObjects;
using Memory.Model.BusinessObjects;
using Memory.Model.Exceptions;

namespace Memory.DAL.Services
{
    public class GameService
    {
        public Game Game { get; set; }

        public ImageService ImageService { get; set; } = new ImageService();

        private static Random _rng = new Random();

        public GameService(Game game)
        {
            this.Game = game;
        }

        //Functie om een game te starten. Wordt alleen in console gebruikt.
        public bool PlayGame()
        {
            while (CheckIfGameOver())
            {
                DisplayCards();
                ChooseCards();
                return false;
            }
            return true;
        }



        //Aanmaken van kaarten op basis van hoeveelheid kaarten. Dit verdubbelen en randomizen.
        //i wordt gebruik om een uniek Id te maken, hierdoor kan de kaart niet twee keer gekozen worden.
        //value wordt gebruikt om eenzelfde value te maken per paar. 
        public List<Card> GenerateCards(string cardamount)
        {
            int parsedAmount = int.Parse(cardamount);

            int i = 0;
            int value = 0;
            while (Game.Cards.Count() < parsedAmount * 2)
            {
                if (i % 2 == 0)
                {
                    value++;
                }

                i++;
                Card card = new Card(i, value);
                Game.Cards.Add(card);
            }
            return Game.Cards;
        }

        //Aanmaken van kaarten op basis van de meegegeven set.
        //Eerst kijken hoeveel images set id hebben.
        //Per image twee kaarten maken. 
        public List<Card> GenerateCards(ImageSet set)
        {
            int i = 0;
            var images = ImageService.getImagesBySetId(set);

            foreach (CardImage image in images)
            {
                int id2 = i + 1;
                // Create two cards for each image
                Card card1 = new Card(i, image, i);
                Card card2 = new Card(id2, image, i);
                // Add the cards to the list
                Game.Cards.Add(card1);
                Game.Cards.Add(card2);
                i++;
            }

            return Game.Cards;
        }


        //Kaarten printen met list positie (+ 1) erbij.
        private void DisplayCards()
        {
            Console.Clear();
            int i = 9;
            Console.WriteLine($"Attempt number: {Game.Attempts + 1}");
            foreach (Card card in Game.Cards)
            {
                Console.WriteLine(card.TurnedOver ? $" [X] Val= {card.CardValue}" : $" [{Game.Cards.Count() - i}] ");
                i--;
            }
        }

        //Schud alle kaarten door elkaar heen.
        public void ShuffleCards()
        {
            var shuffledcards = Game.Cards.OrderBy(a => _rng.Next()).ToList();
            Game.Cards = shuffledcards;
        }

        //Functie om een kaart te kiezen.
        //Functie is alleen voor de console version.
        private void ChooseCards()
        {
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Pick the first card.");
                Card choice1 = null;
                try
                {
                    choice1 = GetCardFromCards(Console.ReadLine());
                    Console.WriteLine($"Chosen card value was: {choice1.CardValue}");
                }
                catch (CardNotFoundException e)
                {
                    Console.Clear();
                    DisplayCards();
                    Console.WriteLine($"Error: {e.Message}");
                    continue;
                }

                Console.WriteLine("Pick the second card.");
                Card choice2 = null;
                try
                {
                    choice2 = GetCardFromCards(Console.ReadLine());
                    Console.WriteLine($"Chosen card value was: {choice2.CardValue}");
                }
                catch (CardNotFoundException e)
                {
                    Console.Clear();
                    DisplayCards();
                    Console.WriteLine($"Error: {e.Message}");
                    continue;
                }

                if (CompareCards(choice1, choice2))
                {
                    validInput = true;
                    Console.WriteLine("Press a key to go the next attempt.");
                    Console.ReadKey();
                }
            }
        }

        //Functie om de kaart uit de list te halen. 
        public Card GetCardFromCards(string number)
        {
            try
            {
                Card card = Game.Cards[int.Parse(number) - 1];
                return card;
            }
            catch (Exception) //Uit parse kunnen meerdere exceptions ontstaan, daarom checken op algemene exception.
            {
                throw new CardNotFoundException("The card could not be found.");
            }
        }

        //Functie om gekozen kaarten te vergelijken qua value.
        protected bool CompareCards(Card choice1, Card choice2)
        {
            if (choice1.CardValue == choice2.CardValue &&
               choice1.Id != choice2.Id &&
               !choice1.TurnedOver &&
               !choice2.TurnedOver)
            {
                choice1.TurnedOver = true;
                choice2.TurnedOver = true;
                Console.WriteLine("Correct!");
                Game.Attempts++;
                return true;
            }
            else if (choice1.Id == choice2.Id)
            {
                Console.WriteLine("You cannot pick the same card twice, try again. An attempt has not been added.");
                return false;
            }
            else if (choice1.TurnedOver || choice2.TurnedOver)
            {
                Console.WriteLine("One of the cards was already turned over, try again. An attempt has not been added.");
                return false;
            }
            else
            {
                Console.WriteLine("Those are not the same cards! Try again. An attempt has been added.");
                Game.Attempts++;
                return true;
            }
        }

        //Checked na elke beurt of alle kaarten omgedraaid zijn.
        public bool CheckIfGameOver()
        {
            int turnedOverCards = Game.Cards.Where(c => c.TurnedOver.Equals(true)).ToList().Count();

            if (turnedOverCards == Game.Cards.Count())
            {
                Game.EndTime = DateTime.Now;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

