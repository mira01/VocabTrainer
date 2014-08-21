using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Mobile.Views;
using Vocabulary.Model;
using Xamarin.Forms;

namespace Vocabulary.Mobile
{
    public class App
    {
        public static dictionary Dict;
        public static Page GetMainPage()
        {

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Mobile.dict2.xml");
            List<dictionaryEntry> entries;
            using (var reader = new System.IO.StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(dictionary));
                Dict = (dictionary)serializer.Deserialize(reader);
                entries = Dict.entry.ToList();
            }
            var listView = new ListView();
            listView.ItemsSource = entries.Where(entry => entry.lesson.Equals("19"));

            var quizPage = new QuizPage();
            
            var mainPage = new NavigationPage(quizPage);
           
            
            return mainPage;
        }
    }
}
