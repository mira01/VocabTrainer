using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using Vocabulary;
using Vocabulary.Comparators;
using Vocabulary.Model;
using Vocabulary.Quiz;

namespace Test
{
    [TestFixture]
    public class JvltQuizTests
    {
        private IQuiz<dictionaryEntry> _quiz;
        [SetUp]
        public void SetUp()
        {
            var entriesSet = new List<dictionaryEntry>()
            {
                new dictionaryEntry() {orth = "árbol"},
                new dictionaryEntry() {orth = "beso"},
                new dictionaryEntry() {orth = "cerezo"},
                new dictionaryEntry() {orth = "dar"}
            };
            var comparator = new JvltComparator();
            _quiz = new JvltQuiz(comparator);
            _quiz.AddEntries(entriesSet);
        }

        [Test]
        public void IterationTest()
        {
            var entry = _quiz.NextEntry();
            Assert.False(_quiz.IsRoundComplete());
            Assert.AreEqual(entry.orth, "árbol");
            entry = (dictionaryEntry)_quiz.NextEntry();
            Assert.False(_quiz.IsRoundComplete());
            Assert.AreEqual(entry.orth, "beso");
            entry = (dictionaryEntry)_quiz.NextEntry();
            Assert.False(_quiz.IsRoundComplete());
            Assert.AreEqual(entry.orth, "cerezo");
            entry = (dictionaryEntry)_quiz.NextEntry();
            Assert.False(_quiz.IsRoundComplete());
            Assert.AreEqual(entry.orth, "dar");
            entry = (dictionaryEntry)_quiz.NextEntry();
            Assert.True(_quiz.IsRoundComplete());
        }

        [Test]
        public void ComparisionTest()
        {
            _quiz.NextEntry();
            var answer = new dictionaryEntry() { orth = "árbol" };
            Assert.True(_quiz.EvaluateWord(answer));
            _quiz.NextEntry();
            var wrongAnswer = new dictionaryEntry() { orth = "nenene" };
            Assert.False(_quiz.EvaluateWord(wrongAnswer));
            _quiz.NextEntry();
            answer = new dictionaryEntry() { orth = "cerezo" };
            Assert.True(_quiz.EvaluateWord(answer));
            _quiz.NextEntry();
            wrongAnswer = new dictionaryEntry() { orth = "nene" };
            Assert.False(_quiz.EvaluateWord(wrongAnswer));
        }

        [Test]
        public void AfterLastWordTest()
        {
            _quiz.NextEntry();
            var answer = new dictionaryEntry() { orth = "árbol" };
            Assert.True(_quiz.EvaluateWord(answer));
            _quiz.NextEntry();
            var wrongAnswer = new dictionaryEntry() { orth = "nenene" };
            Assert.False(_quiz.EvaluateWord(wrongAnswer));
            _quiz.NextEntry();
            answer = new dictionaryEntry() { orth = "cerezo" };
            Assert.True(_quiz.EvaluateWord(answer));
            _quiz.NextEntry();
            wrongAnswer = new dictionaryEntry() { orth = "nene" };
            Assert.False(_quiz.EvaluateWord(wrongAnswer));
            _quiz.AfterLastWord();
            var entry = (dictionaryEntry)_quiz.NextEntry();
            Assert.AreEqual("beso", entry.orth);
            _quiz.IsQuizComplete();

        }
    }
}