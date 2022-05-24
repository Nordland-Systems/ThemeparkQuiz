using System;
using UnityEngine;

namespace ThemeparkQuiz
{
    [Serializable]
    public class WordCategory
    {
        [SerializeField] private WordTypes type;
        [SerializeField] private string[] words;
    }
}