using System;
using System.Collections.Generic;

namespace Vocabulary.Comparators
{
    public interface IVocabComparator<in TEntity> : IComparer<TEntity>
    {
        String QuizzedProperty();
        ICollection<String> ShownProperties();

    }
}
