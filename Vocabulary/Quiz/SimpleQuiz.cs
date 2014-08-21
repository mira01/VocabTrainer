using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Vocabulary.Comparators;
using Vocabulary.Model;

namespace Vocabulary.Quiz
{
    public class SimpleQuiz : IQuiz<AbstractEntry>
    {
        private IEnumerable<AbstractEntry> _entries;
        private readonly IVocabComparator<VocabEntry> _comparator;
        private readonly ICollection<AbstractEntry> _correctlyAnswerredEntries;
        private readonly ICollection<AbstractEntry> _incorrectlyAnswerredEntries;
        private readonly ICollection<AbstractEntry> _incorrectlyAnswerredInThisRound;
        private bool _isRoundComplete;
        private bool _isQuizComplete;
        private IEnumerator<AbstractEntry> _enumerator; 

        public SimpleQuiz(IVocabComparator<VocabEntry> comparator)
        {
            _comparator = comparator;
            _incorrectlyAnswerredInThisRound = new Collection<AbstractEntry>();
            _incorrectlyAnswerredEntries = new Collection<AbstractEntry>();
            _correctlyAnswerredEntries = new Collection<AbstractEntry>();
        }
        public void AddEntries(IEnumerable<AbstractEntry> entries)
        {
            _entries = entries;
            _enumerator = _entries.GetEnumerator();
        }

        public AbstractEntry NextEntry()
        {
            if (!_enumerator.MoveNext())
            {
                _isRoundComplete = true;
                if (_incorrectlyAnswerredInThisRound.Count == 0)
                {
                    _isQuizComplete = true;
                }
            }
            return _enumerator.Current;
        }

        public bool EvaluateWord(AbstractEntry answer)
        {
            var isCorrect = _comparator.Compare((VocabEntry) answer, (VocabEntry) _enumerator.Current) == 0;
            if (isCorrect)
            {
                if (!_incorrectlyAnswerredEntries.Contains(_enumerator.Current))
                {
                    _correctlyAnswerredEntries.Add(_enumerator.Current);
                }

            }
            else
            {
                _incorrectlyAnswerredInThisRound.Add(_enumerator.Current);
                if (!_incorrectlyAnswerredEntries.Contains(_enumerator.Current))
                {
                    _incorrectlyAnswerredEntries.Add(_enumerator.Current);
                }
            }
            return isCorrect;
        }

        public void AfterLastWord()
        {
           _entries = new List<AbstractEntry>();
            _entries = _incorrectlyAnswerredInThisRound.ToList();
            _incorrectlyAnswerredInThisRound.Clear();
            _enumerator = _entries.GetEnumerator();
        }

        public AbstractEntry Current()
        {
            return _enumerator.Current;
        }

        public bool IsRoundComplete()
        {
            return _isRoundComplete;
        }

        public bool IsQuizComplete()
        {
            return _isQuizComplete;
        }
    }
}
