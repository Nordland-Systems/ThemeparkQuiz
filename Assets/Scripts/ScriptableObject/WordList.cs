using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThemeparkQuiz
{
    /// <summary>
    /// A scriptable object for all Words
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New Park", menuName = "ThemeparkQuiz/Park", order = 1)]
    public class WordList : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string iconPath;
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private string backgroundPath;
        [SerializeField] private Sprite backgroundSprite;
        [Header("Words")]
        [SerializeField] private List<WordCategory> wordCategories;

        public List<WordCategory> WordCategories
        {
            get => wordCategories;
            set => wordCategories = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string IconPath
        {
            get => iconPath;
            set => iconPath = value;
        }

        public Sprite IconSprite
        {
            get => iconSprite;
            set => iconSprite = value;
        }

        public string BackgroundPath
        {
            get => backgroundPath;
            set => backgroundPath = value;
        }

        public Sprite BackgroundSprite
        {
            get => backgroundSprite;
            set => backgroundSprite = value;
        }

        public WordList(string title, Sprite iconSprite, Sprite backgroundSprite, List<WordCategory> wordCategories)
        {
            this.title = title;
            this.iconSprite = iconSprite;
            this.backgroundSprite = backgroundSprite;
            this.wordCategories = wordCategories;
        }
        
        public List<Word> GetTitles(string type)
        {
            foreach (WordCategory cat in wordCategories)
            {
                if (cat.Type == type)
                {
                    return cat.Words;
                }
            }
            
            List<Word> s = new List<Word>(){new Word(title, null)};
            return s;
        }
    }
}
