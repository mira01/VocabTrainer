using System;
using System.Collections.Generic;
using Vocabulary.Model;

namespace Vocabulary.Comparators
{
    public class SimpleComparator : IVocabComparator<VocabEntry>
    {
        public int Compare(VocabEntry answer, VocabEntry quizzed)
        {

            return System.String.CompareOrdinal(answer.Original, quizzed.Original);
            
        }

        public string QuizzedProperty()
        {
            return "napis original slova:";
        }

        public ICollection<string> ShownProperties()
        {
            throw new NotImplementedException();
        }
    }
}
