using UnityEngine.UIElements;

namespace ThemeparkQuiz
{
    public class WordGuessed
    {
        private WordEntry entry;
        private bool guessed;
        private float timeLeft;

        public WordGuessed(WordEntry entry, bool guessed, float timeLeft)
        {
            this.entry = entry;
            this.guessed = guessed;
            this.timeLeft = timeLeft;
        }

        public WordEntry Entry
        {
            get => entry;
            set => entry = value;
        }

        public bool Guessed
        {
            get => guessed;
            set => guessed = value;
        }

        public float TimeLeft
        {
            get => timeLeft;
            set => timeLeft = value;
        }
    }
}