namespace Memory.DAL.Services;

public class ScoreService
{
    ScoreRepository ScoreRepository = new ScoreRepository();

    public List<Score> GetHighScores()
    {
        List<Score> topScoreList = ScoreRepository.GetAll().OrderByDescending(c => c.ScoreAmount).Take(10).ToList();
        return topScoreList;
    }

    public void SaveScore(Score score)
    {
        ScoreRepository.Create(score);
    }
}
