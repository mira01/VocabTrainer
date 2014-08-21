using System;
using System.Collections.Generic;
using Vocabulary.Model;

namespace Vocabulary.Comparators
{
    public class JvltComparator : IVocabComparator<dictionaryEntry>
    {
        public int Compare(dictionaryEntry answer, dictionaryEntry quizzed)
        {
            return System.String.CompareOrdinal(answer.orth, quizzed.orth);
        }

        public string QuizzedProperty()
        {
            return "napis original";
        }

        public ICollection<string> ShownProperties()
        {
            throw new NotImplementedException();
        }
    }
}
