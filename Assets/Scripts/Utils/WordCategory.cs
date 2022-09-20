using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThemeparkQuiz
{
    [Serializable]
    public class WordCategory
    {
        [SerializeField] private string type;
        [SerializeField] private List<Word> words;

        public string Type
        {
            get => type;
            set => type = value;
        }

        public List<Word> Words
        {
            get => words;
            set => words = value;
        }

        public WordCategory(string type)
        {
            this.type = type;
            words = new List<Word>();
        }
    }
}