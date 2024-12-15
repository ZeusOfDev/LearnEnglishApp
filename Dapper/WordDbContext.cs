using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using LearnWordApp.Models;
using Dapper;
namespace LearnWordApp.Repository
{
    public class WordDbContext
    {
        private readonly SqlConnection connection;
        public WordDbContext(string connection)
        {
            this.connection = new SqlConnection(connection);
        }
        public List<Word> GetNumberWordRandom(int number)
        {
            if (number > 20)
            {
                throw new ArgumentException();
            }
            if (this.connection == null)
            {
                throw new Exception();
            }
            string sql = $@"SELECT TOP(@Number) *
                            FROM (
                                SELECT TOP(20) *
                                FROM Words
                            ) as table1
                            ORDER BY NEWID();";
            return connection.Query<Word>(sql, new { Number = number }).ToList();
        }
    }
}
