using System.Collections.Generic;

namespace ThemeparkQuiz
{
    public class ParkSettings
    {
        private Dictionary<WordTypes, bool> enabledWords;

        public ParkSettings(WordList list)
        {
            enabledWords = new Dictionary<WordTypes, bool>();
            foreach (WordCategory cat in list.WordCategories)
            {
                enabledWords[cat.Type] = cat.Words.Length > 0;
            }
        }

        public Dictionary<WordTypes, bool> EnabledWords
        {
            get => enabledWords;
            set => enabledWords = value;
        }
    }
}