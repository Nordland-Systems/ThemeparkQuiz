using System;
using UnityEngine;
using SimpleJSON;

namespace ThemeparkQuiz
{
    [Serializable]
    public class WordEntry
    {
        public string word;
        public WordList park;
        public string category;
        public Sprite backgroundImage;

        public WordEntry()
        { }

        public WordEntry(string word, WordList park, string category, Sprite backgroundImage)
        {
            this.word = word;
            this.park = park;
            this.category = category;
            this.backgroundImage = backgroundImage;
        }
    }
}