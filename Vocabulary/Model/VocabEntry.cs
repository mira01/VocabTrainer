using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vocabulary.Model
{
    public  class VocabEntry : AbstractEntry
    {
        private readonly ICollection<Sense> _senses;
        public void AddSense(Sense sense)
        {
            _senses.Add(sense);
        }
        public IEnumerable<Sense> GetSenses()
        {
            return _senses;
        }
        public VocabEntry()
        {
            _senses = new Collection<Sense>();
        }

        public string Original { get; set; }
        
    }
}

