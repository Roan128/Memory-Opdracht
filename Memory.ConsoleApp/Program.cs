﻿using Memory.DAL.Services;
using Memory.Model.BusinessObjects;

Console.WriteLine("Welkom, geef je naam op.");
string name = Console.ReadLine();

Player player = new Player(name);

Game game = new Game();
ScoreService scoreService = new ScoreService();
game.GenerateCards(player.CardAmount);
game.ShuffleCards();

while (!game.PlayGame())
{
    continue;
}

Score score = new Score(player.Name);
Console.Clear();
Console.WriteLine("Now calculating score and saving score...");
score.CalculateScore(game);
scoreService.SaveScore(score);
Console.WriteLine($"Your score is: {score.ScoreAmount}\n");
Console.WriteLine("------ \n Top 10: \n------");
foreach (Score scoreItem in scoreService.GetHighScores())
{
    if (scoreItem.Id != score.Id)
    {
        Console.WriteLine($"{scoreItem.PlayerName} - {scoreItem.ScoreAmount}");
    }
    else
    {
        Console.WriteLine($"{scoreItem.PlayerName} - {scoreItem.ScoreAmount} <---- Your new score!");
    }

}

