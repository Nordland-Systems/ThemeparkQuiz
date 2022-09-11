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
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private Sprite backgroundSprite;
        [Header("Words")]
        [SerializeField] private List<WordCategory> wordCategories;

        public List<WordCategory> WordCategories => wordCategories;

        public string Title => title;
        public Sprite IconSprite => iconSprite;
        public Sprite BackgroundSprite => backgroundSprite;

        public WordList(string title, Sprite iconSprite, Sprite backgroundSprite, List<WordCategory> wordCategories)
        {
            this.title = title;
            this.iconSprite = iconSprite;
            this.backgroundSprite = backgroundSprite;
            this.wordCategories = wordCategories;
        }
        
        public string[] GetTitles(WordTypes type)
        {
            foreach (WordCategory cat in wordCategories)
            {
                if (cat.Type == type)
                {
                    return cat.Words;
                }
            }
            
            string[] s = {title};
            return s;
        }
    }
}
