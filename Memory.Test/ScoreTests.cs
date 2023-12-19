using Memory.BLL.Interfaces;
using Moq;

namespace Memory.Test;

public class ScoreTests
{
    public Player _testplayer;
    public ScoreService _scoreService;
    public Score score1 { get; set; }
    private Mock<IScoreRepository> _scoreRepository = new Mock<IScoreRepository>();

    [SetUp]
    public void Setup()
    {
        score1 = new Score { Id = Guid.NewGuid(), ScoreAmount = 95, PlayerName = "John Doe" };
        Score score2 = new Score { Id = Guid.NewGuid(), ScoreAmount = 82, PlayerName = "Jane Smith" };
        Score score3 = new Score { Id = Guid.NewGuid(), ScoreAmount = 88, PlayerName = "Bob Johnson" };
        List<Score> scoreList = new List<Score>
        {
            score1,
            score2,
            score3
        };

        _scoreRepository.Setup(sr => sr.Create(It.IsAny<Score>())).Returns(true);
        _scoreRepository.Setup(sr => sr.GetAll()).Returns(scoreList);
        _testplayer = new Player("Test player");
        _scoreService = new ScoreService(_scoreRepository.Object);
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void Create_ShouldReturn()
    {
        //Setup
        Score score = new Score("Speler");

        //Assert
        Assert.That(_scoreService.SaveScore(score), Is.EqualTo(true));
    }

    [Test]
    public void GetAll_ShouldReturn()
    {
        //Act
        List<Score> scores = _scoreService.GetHighScores();

        //Assert
        Assert.That(scores.Count, Is.EqualTo(3));
        Assert.That(scores.Equals(null), Is.False);
        Assert.That(scores[0].Equals(score1), Is.True);
    }
}
