using System.Collections.Generic;
using Vocabulary.Model;

namespace Vocabulary.Quiz
{
    public interface IQuiz<TEntry> where TEntry : AbstractEntry
    {
        void AddEntries(IEnumerable<TEntry> entries);

        TEntry NextEntry();

        bool EvaluateWord(TEntry answer);

        bool IsRoundComplete();

        bool IsQuizComplete();

        void AfterLastWord();

        TEntry Current();


    }

   
}
