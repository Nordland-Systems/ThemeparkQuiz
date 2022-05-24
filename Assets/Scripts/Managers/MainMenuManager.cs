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
            backgroundImage.sprite = backgroundSprites[random.Next(backgroundSprites.Length)];
            Rect imageRect = backgroundImage.rectTransform.rect;
            backgroundImage.GetComponent<AspectRatioFitter>().aspectRatio = imageRect.width/imageRect.height;
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