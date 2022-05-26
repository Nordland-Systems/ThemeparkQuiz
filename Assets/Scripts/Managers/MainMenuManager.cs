using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace ThemeparkQuiz
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Sprite[] backgroundSprites;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Slider sliderMinutes;
        [SerializeField] private Slider sliderSeconds;

        private void Start()
        {
            Random random = new Random();
            backgroundImage.sprite = backgroundSprites[random.Next(backgroundSprites.Length)];
            Rect imageRect = backgroundImage.rectTransform.rect;
            backgroundImage.GetComponent<AspectRatioFitter>().aspectRatio = imageRect.width/imageRect.height;
            
            sliderMinutes.value = PlayerPrefs.GetInt("maxMinutes", 0);
            sliderSeconds.value = PlayerPrefs.GetInt("maxSeconds", 30);
            timeText.text = (int) sliderMinutes.value + ":" + (int) sliderSeconds.value;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Quitting...");
                Application.Quit();
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(GameScenes.PARKOVERVIEW);
        }

        public void ShowCredits()
        {
            SceneManager.LoadScene(GameScenes.CREDITS);
        }

        public void ShowTutorial()
        {
            SceneManager.LoadScene(GameScenes.TUTORIAL);
        }

        public void ChangeSliders()
        {
            PlayerPrefs.SetInt("maxMinutes", (int)sliderMinutes.value);
            PlayerPrefs.SetInt("maxSeconds", (int)sliderSeconds.value);
            timeText.text = (int) sliderMinutes.value + ":" + (int) sliderSeconds.value;
        }
    }
}