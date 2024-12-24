using System.Linq.Expressions;

namespace LearnWordApp.Models
{
    public static class WordExtension
    {
        public static string ConvertIEnumarableToString(this IEnumerable<Word> e)
        {
            string data = "";
            int ign = 0;
            foreach (var item in e)
            {
                if (ign == 0)
                {
                    ign = 1;
                    data += item.EngWord + item.TypeWord + item.VieWord;
                }
                else
                    data = data + ";" + item.EngWord + item.TypeWord + item.VieWord;
            }
            return data;
        }
        public static void SwapWord(this string[] words, int i1, int i2)
        {
            var tmp = words[i1]; 
            words[i1] = words[i2];
            words[i2] = tmp;
        }
    }
}
