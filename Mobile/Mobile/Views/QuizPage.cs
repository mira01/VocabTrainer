using System;
using System.Collections.Generic;
using System.Linq;
using Vocabulary.Comparators;
using Vocabulary.Mobile;
using Vocabulary.Model;
using Vocabulary.Quiz;
using Xamarin.Forms;

namespace Mobile.Views
{
    public class QuizPage : ContentPage
    {
        private Entry _answerInput;
        private Label _exposedLabel;
        private Button _nextButton;
        private Button _evaluateButton;

        private IQuiz<dictionaryEntry> _quiz;
        private IVocabComparator<dictionaryEntry> _comparator;
        private IEnumerable<dictionaryEntry> _dataSet;
        public QuizPage()
        {
            Title = "Quiz";
            var startButton = new Button()
            {
                BorderRadius = 20,
                BackgroundColor = Color.FromHex("#00ff00"),
                Text = "Start quiz"
            };
            Content = new StackLayout()
            {
                Children = { startButton, }
            };
            startButton.Clicked += startButton_Clicked;
        }
        private void startButton_Clicked(object sender, EventArgs e)
        {
            _dataSet = App.Dict.entry.Where(x => x.lesson.Equals("test"));
            _comparator = new JvltComparator();
            _quiz = new JvltQuiz(_comparator);
            _quiz.AddEntries(_dataSet);
            InitQuizLayout();
            _nextButton_Clicked(sender,e);
        }
        private void _nextButton_Clicked(object sender, EventArgs e)
        {
            InitQuizLayout();
            BackgroundColor = Color.Default;
            var entry = _quiz.NextEntry(); //Possible NullReferenceException
            if (entry == null)
            {
                if (_quiz.IsRoundComplete())
                {
                    if (_quiz.IsQuizComplete())
                    {
                        OnQuizComplete();
                    }
                    OnRoundComplete();
                }
            }
            else
            {
                _exposedLabel.Text = entry.sense.Aggregate("", (current, sense) => current + (sense.trans + "\n"));
            }
        }

        private void InitQuizLayout()
        {
            _exposedLabel = new Label();

            _answerInput = new Entry() { Placeholder = "Answer" };
            _evaluateButton = new Button() { Text = "Vyhodnoť" };
            _evaluateButton.Clicked += delegate { Evaluate(); };
            _nextButton = new Button() { Text = "Další" };
            //_nextButton.IsVisible = false;
            _nextButton.Clicked += _nextButton_Clicked;

            var content = new StackLayout();
            content.Children.Add(_exposedLabel);
            content.Children.Add(_answerInput);
            content.Children.Add(_nextButton);
            content.Children.Add(_evaluateButton);

            Content = content;
        }
        private void Evaluate()
        {
            var answer = new dictionaryEntry() { orth = _answerInput.Text };
            if (_quiz.EvaluateWord(answer))
            {
                this.BackgroundColor = Color.Green;
            }
            else
            {
                this.BackgroundColor = Color.Red;
                _exposedLabel.Text = String.Format("Správná odpověď je {0}", _quiz.Current().orth);
            }
            _answerInput.Text = "";
            // _nextButton.IsVisible = true;
            //_evaluateButton.IsVisible = false;
        }
        private void OnRoundComplete()
        {
            var continueButton = new Button()
            {
                Text = "Znovu",
            };
            continueButton.Clicked += _nextButton_Clicked;
            var quizControls = new StackLayout();
            quizControls.Children.Add(continueButton);

            Content = quizControls;
            _exposedLabel.Text+="Kolo Hotovo";
            _quiz.AfterLastWord();
        }
        private void OnQuizComplete()
        {
            _exposedLabel.Text += "kviz Hotov";
        }
    }
}