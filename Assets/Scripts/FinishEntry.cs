using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThemeparkQuiz
{
    public class FinishEntry : MonoBehaviour
    {
        [SerializeField] private TMP_Text wordTitle;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Color colorCorrect;
        [SerializeField] private Color colorSkipped;
        [SerializeField] private Sprite correctIcon;
        [SerializeField] private Sprite skippedIcon;
        private WordEntry entry;

        public void Populate(WordGuessed guessedState)
        {
            entry = guessedState.Entry;
            wordTitle.text = entry.word;
            float timeLeft = guessedState.TimeLeft;
            int minutes = (int) (timeLeft / 60);
            int seconds = (int) (timeLeft - 60 * minutes);
            timeText.text = minutes + ":" + seconds + " übrig";
            backgroundImage.sprite = entry.park.BackgroundSprite;
            iconImage.sprite = guessedState.Guessed ? correctIcon : skippedIcon;
            backgroundImage.color = guessedState.Guessed ? colorCorrect : colorSkipped;
        }
    }
}