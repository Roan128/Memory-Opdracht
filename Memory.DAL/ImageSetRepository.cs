namespace Memory.DAL;

public class ImageSetRepository : IImageSetRepository
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

    public ICollection<ImageSet> GetAll()
    {
        var sets = new List<ImageSet>();
        string sql = "SELECT Id, Name FROM ImageSet;";

        using (var connection = new SqlConnection(Connectionstring))
        {
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var item = new ImageSet();
                    item.Id = reader.GetGuid(0);
                    item.Name = reader.GetString(1);

                    sets.Add(item);
                }
                connection.Close();
            }
        }
        return sets;
    }
}
