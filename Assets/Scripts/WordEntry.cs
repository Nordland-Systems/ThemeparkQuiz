using System;

namespace ThemeparkQuiz
{
    [Serializable]
    public class WordEntry
    {
        public string word;
        public WordList park;
        public WordTypes category;

        public WordEntry()
        { }

        public WordEntry(string word, WordList park, WordTypes category)
        {
            this.word = word;
            this.park = park;
            this.category = category;
        }
    }
}