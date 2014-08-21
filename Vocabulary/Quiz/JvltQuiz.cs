using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Vocabulary.Comparators;
using Vocabulary.Model;

namespace Vocabulary.Quiz
{
    public class JvltQuiz : IQuiz<dictionaryEntry>
    {
        private IEnumerable<dictionaryEntry> _entries;
        private readonly IVocabComparator<dictionaryEntry> _comparator;
        private readonly ICollection<dictionaryEntry> _correctlyAnswerredEntries;
        private readonly ICollection<dictionaryEntry> _incorrectlyAnswerredEntries;
        private readonly ICollection<dictionaryEntry> _incorrectlyAnswerredInThisRound;
        private bool _isRoundComplete;
        private bool _isQuizComplete;
        private IEnumerator<dictionaryEntry> _enumerator;

        public JvltQuiz(IVocabComparator<dictionaryEntry> comparator)
        {
            _comparator = comparator;
            _incorrectlyAnswerredInThisRound = new Collection<dictionaryEntry>();
            _incorrectlyAnswerredEntries = new Collection<dictionaryEntry>();
            _correctlyAnswerredEntries = new Collection<dictionaryEntry>();
        }
        public void AddEntries(IEnumerable<dictionaryEntry> entries)
        {
            _entries = entries;
            _enumerator = _entries.GetEnumerator();
        }

        public dictionaryEntry NextEntry()
        {
            _isRoundComplete = false;
            if (!_enumerator.MoveNext())
            {
                _isRoundComplete = true;
                if (_incorrectlyAnswerredInThisRound.Count == 0)
                {
                    _isQuizComplete = true;
                }
                _enumerator = _entries.GetEnumerator();
            }
            return _enumerator.Current;
        }

        public bool EvaluateWord(dictionaryEntry answer)
        {
            var isCorrect = _comparator.Compare((dictionaryEntry)answer, (dictionaryEntry)_enumerator.Current) == 0;
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
            _entries = new List<dictionaryEntry>();
            _entries = _incorrectlyAnswerredInThisRound.ToList();
            _incorrectlyAnswerredInThisRound.Clear();
            _enumerator = _entries.GetEnumerator();
            _isRoundComplete = false;
        }

        public dictionaryEntry Current()
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
