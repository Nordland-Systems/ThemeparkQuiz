using System;
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

        private void Start()
        {
            Random random = new Random();
            RectTransform imageRect = backgroundImage.rectTransform;
            backgroundImage.GetComponent<AspectRatioFitter>().aspectRatio = imageRect.rect.width / imageRect.rect.height;
            backgroundImage.sprite = backgroundSprites[random.Next(backgroundSprites.Length)];
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
    }
}