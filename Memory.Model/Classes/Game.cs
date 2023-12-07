using Memory.Model.Exceptions;

namespace Memory.Model.Classes;

//Deze klasse misschien opdelen in twee klasses. Een met logica voor de console en een met logica voor de GUI.
public class Game
{
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; }

    public List<Card> Cards = new List<Card> { };
    public int Attempts { get; set; }

    private static Random rng = new Random();

    //Game starten zodra player aangemaakt is en kaarten gemaakt zijn.
    public void StartGame(Player player)
    {
        GenerateCards(player.CardAmount);
        ShuffleCards();

        while (CheckIfGameOver())
        {
            DisplayCards();
            ChooseCards();
        }

        Console.Clear();
        Console.WriteLine("Game over now, calculating score.");
        Score score = new Score(player.Name);
        score.GetScore(this);
        score.SaveScore(score);
        Console.WriteLine("Score " + score.ScoreAmount + "\n");

        score.GetHighScores();

        Console.WriteLine("Game over");
    }

    //Aanmaken van kaarten op basis van hoeveelheid kaarten. Dit verdubbelen en randomizen.
    //i wordt gebruik om een uniek Id te maken, hierdoor kan de kaart niet twee keer gekozen worden.
    //value wordt gebruikt om eenzelfde value te maken per paar. 
    public List<Card> GenerateCards(string cardamount)
    {
        int parsedAmount = ParseStringToInt(cardamount);

        int i = 0;
        int value = 0;
        while (Cards.Count() < parsedAmount * 2)
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


    //Kaarten printen met list positie erbij.
    public void DisplayCards()
    {
        Console.Clear();
        int i = 9;
        Console.WriteLine($"Attempt number: {Attempts + 1}");
        foreach (Card card in Cards)
        {

            if (card.TurnedOver)
            {
                Console.WriteLine($" [X] Val= {card.CardValue}");
            }
            else
            {
                Console.WriteLine($" [{Cards.Count() - i}] ");
            }
            i--;
        }
    }

    //Schud alle kaarten door elkaar heen.
    public void ShuffleCards()
    {
        var shuffledcards = Cards.OrderBy(a => rng.Next()).ToList();
        Cards = shuffledcards;
    }

    //Functie om een kaart te kiezen.
    //Deze functie moet eigenlijk alleen voor de console versie gebruikt worden.
    public void ChooseCards()
    {
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Pick the first card.");
            Card choice1 = null;
            try
            {
                choice1 = Check(Console.ReadLine());
                Console.WriteLine($"Chosen card value was: {choice1.CardValue}");
            }
            catch (CardNotFoundException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                continue;
            }

            Console.WriteLine("Pick the second card.");
            Card choice2 = null;
            try
            {
                choice2 = Check(Console.ReadLine());
                Console.WriteLine($"Chosen card value was: {choice2.CardValue}");
            }
            catch (CardNotFoundException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                continue;
            }

            if (choice1 != null && choice2 != null)
            {
                if (CompareCards(choice1, choice2))
                {
                    validInput = true;
                    Console.WriteLine("Press a key to go the next attempt.");
                    Console.ReadKey();
                }
            }
        }
    }

    public bool Compare(Card card1, Card card2)
    {
        if (card1.CardValue.Equals(card2.CardValue))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Voert de functies uit die de kaart zoeken.
    //Indien fout, exception.
    public Card Check(string input)
    {
        try
        {
            int parsed = ParseStringToInt(input);
            Card card = GetCardFromCards(parsed);
            return card;
        }
        catch (CardNotFoundException e)
        {
            throw new CardNotFoundException($"{e.Message}");
        }

    }

    //Functie om input van console.readline() of andere string inputs naar int te maken.
    public int ParseStringToInt(string numberAsString)
    {
        try
        {
            int number = int.Parse(numberAsString);
            return number;
        }
        catch
        {
            throw new CardNotFoundException("Number is not valid.");
        }
    }

    //Functie om de kaart uit de list te halen. 
    public Card GetCardFromCards(int number)
    {
        try
        {
            Card card = Cards[number - 1];
            return card;
        }
        catch (Exception)
        {
            throw new CardNotFoundException("The card could not be found.");
        }
    }

    //Functie om gekozen kaarten te vergelijken qua value.
    public bool CompareCards(Card choice1, Card choice2)
    {
        if (choice1.CardValue == choice2.CardValue &&
           choice1.Id != choice2.Id &&
           !choice1.TurnedOver &&
           !choice2.TurnedOver)
        {
            choice1.TurnedOver = true;
            choice2.TurnedOver = true;
            Console.WriteLine("Correct!");
            Attempts++;
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
            Attempts++;
            return true;
        }
    }

    //Checked na elke beurt of alle kaarten omgedraaid zijn.
    public bool CheckIfGameOver()
    {
        int turnedOverCards = Cards.Where(c => c.TurnedOver.Equals(true)).ToList().Count();

        if (turnedOverCards == Cards.Count())
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
