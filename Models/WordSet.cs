namespace LearnWordApp.Models
{
    public class WordSet
    {
        public int SetID { get; set; }
        public string SetName { get; set; }
        public int Amount { get; set; }
        public List<Word> Words { get; set;} 
    }
}
