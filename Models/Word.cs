namespace LearnWordApp.Models
{
    public class Word
    {
        public int WordId { get; set; }
        public string EngWord { get; set; }
        public string VieWord { get; set; }
        public string TypeWord { get; set; }
        WordSet WordSet { get; set; }
    }
}
