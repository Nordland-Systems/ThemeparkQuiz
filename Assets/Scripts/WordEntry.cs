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

        public string getCategoryName()
        {
            switch (category)
            {
                default:
                    return category.ToString();
                case WordTypes.Park:
                    return "Park";
                case WordTypes.Coaster:
                    return "Achterbahn";
                case WordTypes.Flatride:
                    return "Flatride";
                case WordTypes.Character:
                    return "Charakter";
                case WordTypes.Event:
                    return "Event";
                case WordTypes.Area:
                    return "Themenbereich";
                case WordTypes.Defunct:
                    return "Ehemalige";
            }
        }
    }
}