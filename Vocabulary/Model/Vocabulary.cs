using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vocabulary.Comparators;

namespace Vocabulary.Model
{
    public class Vocabulary : IVocabulary
    {
        public String Language;

        private readonly ICollection<AbstractEntry> _entries;

        public Vocabulary()
        {
            _entries = new Collection<AbstractEntry>();
        }

        public void AddEntry(AbstractEntry entry)
        {
            _entries.Add(entry);
        }

        public ICollection<AbstractEntry> GetEntries()
        {
            return _entries;
        }

        public int Compare<TEntity>(IVocabComparator<TEntity> comparer, TEntity answer, TEntity quizzed)
            where TEntity : AbstractEntry
        {
            return comparer.Compare(answer, quizzed);
        }
    }
}
