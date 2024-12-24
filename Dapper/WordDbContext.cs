using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using LearnWordApp.Models;
using Dapper;
using System.Collections;
namespace LearnWordApp.Repository
{
    public class WordDbContext
    {
        public string connectionString;
        public WordDbContext(string connection)
        {
            this.connectionString = connection;
        }
        public IEnumerable<Word> GetAllWordInSet<T>(T setid)
        {
            IEnumerable<Word> values;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from Word where SetID = {setid}";
                values = connection.Query<Word>(sql);
                connection.Close();
            }
             return values;
        }
        public List<WordSet> GetAllWordSet()
        {
            IEnumerable<WordSet> wordSets;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "select * from WordSet";
                wordSets = connection.Query<WordSet>(sql);
                connection.Close();
            }
            return wordSets.ToList();
        }
    }
}
