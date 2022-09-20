using System;
using UnityEngine;

namespace ThemeparkQuiz
{
    [Serializable]
    public class Word
    {
        private string content;
        private Sprite backgroundImage;

        public string Content
        {
            get => content;
            set => content = value;
        }

        public Sprite BackgroundImage
        {
            get => backgroundImage;
            set => backgroundImage = value;
        }

        public Word(String content, Sprite backgroundImage)
        {
            this.content = content;
            this.backgroundImage = backgroundImage;
        }
    }
}