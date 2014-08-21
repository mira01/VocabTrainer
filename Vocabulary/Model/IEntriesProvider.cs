using System.Collections.Generic;

namespace Vocabulary.Model
{
    public interface IEntriesProvider
    {
        IEnumerable<dictionaryEntry> GetEntries();
    }
}
