using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Model
{
    public partial class dictionaryEntry : AbstractEntry
    {
        public override string ToString()
        {
            string senses = "";
            foreach (var dictionaryEntrySense in sense)
            {
                senses += " " + dictionaryEntrySense.trans;
            }
            return String.Format("{0}\t:\t{1}", orth, senses);
        }
    }
}
