using System;
using System.Collections.Generic;

namespace Vocabulary.Model
{
    public abstract class AbstractEntry
    {
        public Guid Id { get; set; }

        private Dictionary<String, Object>.KeyCollection Properties;

    }
}