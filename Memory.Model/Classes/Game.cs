using Memory.Model.Exceptions;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Memory.Model.Classes
{
    //Deze klasse misschien opdelen in twee klasses. Een met logica voor de console en een met logica voor de GUI.
    public class Game
    {
        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; }

        public List<Card> Cards = new List<Card> { };

        public int Attempts { get; set; }

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
            while (Cards.Count() < cardamount * 2)
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

        //Shuffled na het genereren van de kaarten alle kaarten door elkaar.
        public List<Card> ShuffleCards(List<Card> cards)
        {
            return cards;
        }

        //Kaarten printen met Id erbij.
        public void DisplayCards()
        {
            foreach (Card card in Cards)
            {
                if (card.TurnedOver)
                {
                    Console.WriteLine($" [X] Val= {card.CardValue}");
                }
                else
                {
                    Console.WriteLine($" [{card.Id}] ");
                }
            }
        }

        //Functie om een kaart te kiezen.
        //TODO: Try - catch bouwen om out of bounds te voorkomen, CardNotFound gebruiken.
        public void ChooseCards()
        {
            Console.WriteLine("Pick a first card.");
            Card choice1 = CheckIfCardExists(Console.ReadLine());

            Console.WriteLine("Pick a second card.");
            Card choice2 = CheckIfCardExists(Console.ReadLine());


            if (choice1.CardValue == choice2.CardValue)
            {
                choice1.TurnedOver = true;
                choice2.TurnedOver = true;
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine("Value is not equal, try again.");
            }
            Attempts++;
        }

        //Met linq kijken of er een kaart het Id heeft.
        public Card CheckIfCardExists(string choice)
        {
            Card pickedCard = null;
            bool validChoice = false;

            while (!validChoice)
            {
                try
                {
                    int parsedChoice = int.Parse(choice);
                    pickedCard = Cards.FirstOrDefault(card => card.Id == parsedChoice);
                    if (pickedCard == null)
                    {
                        throw new CardNotFoundException($"The card with id {choice} does not exist.");
                    } else
                    {
                        validChoice = true;
                    }
                } catch (FormatException)
                {
                    Console.WriteLine($"{choice} is not a valid number.");
                } catch (CardNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (!validChoice)
                {
                    Console.Write("Enter a valid choice: ");
                    choice = Console.ReadLine();
                }
            }

            return pickedCard;
        }

        //Checked of alle kaarten omgedraaid zijn.
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
            }
            else
            {
                return true;
            }
        }
    }
}
