using System.Collections.Generic;

namespace ThemeparkQuiz
{
    public class ParkSettings
    {
        private Dictionary<string, bool> enabledWords;

        public ParkSettings(WordList list)
        {
            enabledWords = new Dictionary<string, bool>();
            foreach (WordCategory cat in list.WordCategories)
            {
                enabledWords[cat.Type] = cat.Words.Count > 0;
            }
        }

        public Dictionary<string, bool> EnabledWords
        {
            get => enabledWords;
            set => enabledWords = value;
        }
    }
}