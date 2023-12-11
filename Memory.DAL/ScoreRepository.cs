using Memory.BLL.Interfaces;
using Memory.Model.BusinessObjects;
using Microsoft.Data.SqlClient;

namespace Memory.DAL;

public class ScoreRepository : IScoreRepository
{
    public string Connectionstring { get; set; } = @"Data Source=(localdb)\mssqllocaldb;Database=ScoreDb;Trusted_Connection=True;MultipleActiveResultSets=true";

    public ICollection<Score> GetAll()
    {
        var scores = new List<Score>();
        string sql = "SELECT Id, ScoreAmount, PlayerName FROM Score;";

        using (var connection = new SqlConnection(Connectionstring))
        {
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var item = new Score();
                    item.Id = reader.GetGuid(0);
                    item.ScoreAmount = reader.GetInt32(1);
                    item.PlayerName = reader.GetString(2);
                    scores.Add(item);
                }
                connection.Close();
            }
        }
        return scores;
    }

    public bool Create(Score score)
    {
        string sqlQuery = "INSERT INTO Score (Id, ScoreAmount, PlayerName) VALUES (@Id, @ScoreAmount, @PlayerName);";
        int recordsAffected = 0;

        using (var connection = new SqlConnection(Connectionstring))
        {
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("Id", score.Id);
                command.Parameters.AddWithValue("ScoreAmount", score.ScoreAmount);
                command.Parameters.AddWithValue("PlayerName", score.PlayerName);

                connection.Open();
                recordsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return recordsAffected > 0;
    }
}
