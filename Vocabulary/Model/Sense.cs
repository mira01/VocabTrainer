namespace Vocabulary.Model
{
    public class Sense
    {
        public string Translation { get; set; }

        public string Definition { get; set; }

        public override string ToString()
        {
            return Translation;
        }
    }
}
