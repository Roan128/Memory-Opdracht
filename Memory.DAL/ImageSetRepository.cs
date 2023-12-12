using Memory.BLL.BusinessObjects;
using Microsoft.Data.SqlClient;

namespace Memory.DAL
{
    public class ImageSetRepository
    {
        public string Connectionstring { get; set; } = @"Data Source=(localdb)\mssqllocaldb;Database=ScoreDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        public bool Create(ImageSet set)
        {
            string sqlQuery = "INSERT INTO ImageSet (Id, Name) VALUES (@Id, @Name);";
            int recordsAffected = 0;

            using (var connection = new SqlConnection(Connectionstring))
            {
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("Id", set.Id);
                    command.Parameters.AddWithValue("Name", set.Name);

                    connection.Open();
                    recordsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return recordsAffected > 0;
        }
    }
}
