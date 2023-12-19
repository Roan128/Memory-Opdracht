namespace Memory.DAL.Services;

public class ScoreService
{
    IScoreRepository ScoreRepository { get; set; }

    public ScoreService()
    {
        ScoreRepository = new ScoreRepository();
    }

    public ScoreService(IScoreRepository scoreRepository)
    {
        ScoreRepository = scoreRepository;
    }

    public List<Score> GetHighScores()
    {
        List<Score> topScoreList = ScoreRepository.GetAll().OrderByDescending(c => c.ScoreAmount).Take(10).ToList();
        return topScoreList;
    }

    public bool SaveScore(Score score)
    {
        if (ScoreRepository.Create(score))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
