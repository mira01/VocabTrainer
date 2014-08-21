using System.Collections.Generic;
using Vocabulary.Comparators;

namespace Vocabulary.Model
{
    public interface IVocabulary
    {
        void AddEntry(AbstractEntry entry);
        ICollection<AbstractEntry> GetEntries();

        int Compare<TEntity>(IVocabComparator<TEntity> comparator, TEntity answer, TEntity original) where TEntity : AbstractEntry;
    }
}