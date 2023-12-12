using Memory.BLL.BusinessObjects;
using Memory.BLL.Interfaces;
using Microsoft.Data.SqlClient;

namespace Memory.DAL
{
    public class CardImageRepository : ICardImageRepository
    {
        public string Connectionstring { get; set; } = @"Data Source=(localdb)\mssqllocaldb;Database=ScoreDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        public bool Create(CardImage businessObject)
        {
            throw new NotImplementedException();
        }

        public bool CreateMultiple(List<CardImage> cardImages)
        {
            foreach (var cardImage in cardImages)
            {
                string sqlQuery = "INSERT INTO CardImages (Id, SetId, ImageData) VALUES (@Id, @SetId, @ImageData);";

                using (var connection = new SqlConnection(Connectionstring))
                {
                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("Id", cardImage.Id);
                        command.Parameters.AddWithValue("SetId", cardImage.SetId);
                        command.Parameters.AddWithValue("ImageData", cardImage.ImageData);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            return true;
        }

        public ICollection<CardImage> GetAll()
        {
            var cardImages = new List<CardImage>();
            string sql = "SELECT Id, ScoreAmount, PlayerName FROM Score;";

            using (var connection = new SqlConnection(Connectionstring))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new CardImage();
                        item.Id = reader.GetGuid(0);
                        item.SetId = reader.GetGuid(1);
                        int columnIndex = reader.GetOrdinal("ImageData");

                        if (!reader.IsDBNull(columnIndex))
                        {
                            long length = reader.GetBytes(columnIndex, 0, null, 0, 0);
                            byte[] imageData = new byte[length];
                            reader.GetBytes(columnIndex, 0, imageData, 0, (int)length);
                            item.ImageData = imageData;
                        }

                        cardImages.Add(item);
                    }
                    connection.Close();
                }
            }
            return cardImages;
        }

        public List<CardImage> GetCardsBySetId(Guid setId)
        {
            throw new NotImplementedException();
        }
    }
}
