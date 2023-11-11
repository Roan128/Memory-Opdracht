using Memory.Model.Classes;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Memory.Model.DataAccess
{
    public class ScoreRepository
    {
        public string Connectionstring { get; set; }

        public ScoreRepository(string connectionstring) 
        {
            Connectionstring = connectionstring;
        }

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

        public int Insert(Score score)
        {
            string sql = "INSERT INTO Score (Id, ScoreAmount, PlayerName) VALUES (@Id, @ScoreAmount, @PlayerName);";
            int recordsAffected = 0;

            using (var connection = new SqlConnection(Connectionstring))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("Id", score.Id);
                    command.Parameters.AddWithValue("ScoreAmount", score.ScoreAmount);
                    command.Parameters.AddWithValue("PlayerName", score.PlayerName);

                    connection.Open();
                    recordsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return recordsAffected;
        }
    }
}
