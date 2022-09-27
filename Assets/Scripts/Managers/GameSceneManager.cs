using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace ThemeparkQuiz
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private float timeForRound;
        [SerializeField] private Image background;
        [SerializeField] private AspectRatioFitter backgroundRatio;
        [SerializeField] private TMP_Text wordText;
        [SerializeField] private TMP_Text wordDescription;
        [SerializeField] private TMP_Text countdownText;
        [SerializeField] private List<WordEntry> words;

        [Header("Rotation")] 
        [SerializeField] private Button correctButton;
        [SerializeField] private Button skipButton;
        
        [Header("End Screen")]
        [SerializeField] private GameObject endScreen;
        [SerializeField] private TMP_Text totalGuessedWordsText;
        [SerializeField] private Transform finishEntryHolder;
        [SerializeField] private GameObject finishEntryPrefab;
        
        private WordList[] parks;
        private Dictionary<WordList, ParkSettings> parkSettings;

        private GameStates currentGameState = GameStates.PREGAME;
        private int countdownNumbers = 3;
        private float timeLeft;
        private WordEntry currentWord;
        private List<WordGuessed> guessedWords;

        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            timeForRound = PlayerPrefs.GetInt("maxMinutes", 0) * 60 + PlayerPrefs.GetInt("maxSeconds");
            currentGameState = GameStates.STARTUP;
            parks = GameManager.Instance.Parks;
            parkSettings = GameManager.Instance.ParkSettings;
            words = new List<WordEntry>();
            guessedWords = new List<WordGuessed>();
            Rect backgroundRect = background.GetComponent<RectTransform>().rect;
            backgroundRatio.aspectRatio = backgroundRect.width / backgroundRect.height;
            timeLeft = timeForRound;
            LoadWords();
        }

        private void Update()
        {
            DetectMovement();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(GameScenes.PARKOVERVIEW);
            }

            if (currentGameState == GameStates.INGAME)
            {
                timeLeft -= Time.deltaTime;
                if ( timeLeft < 0 )
                {
                    EndGame();
                }

                int minutes = (int) (timeLeft / 60);
                int seconds = (int) (timeLeft - 60 * minutes);
                countdownText.text = minutes + ":" + seconds;
            }
        }

        private void LoadWords()
        {
            WordList[] allWordLists = GameManager.Instance.Parks;
            Dictionary<WordList, ParkSettings> settings = GameManager.Instance.ParkSettings;

            foreach (WordList wl in allWordLists)
            {
                if (settings[wl].EnabledWords.Count > 0)
                {
                    foreach (WordCategory wc in wl.WordCategories)
                    {
                        if (settings[wl].EnabledWords[wc.Type])
                        {
                            foreach (Word word in wc.Words)
                            {
                                words.Add(new WordEntry(word.Content, wl, wc.Type, word.BackgroundImage));
                            }
                        }
                    }
                }
            }
            
            words.Shuffle();
        }

        public void CallNewWord()
        {
            if (words.Count > 0)
            {
                WordEntry newWord = words[^1];

                wordText.text = newWord.word;
                wordDescription.text = newWord.park.Title + " (" + newWord.category + ")";
                if (newWord.backgroundImage != null)
                {
                    background.sprite = newWord.backgroundImage;
                }
                else if (newWord.park.BackgroundSprite != null)
                {
                    background.sprite = newWord.park.BackgroundSprite;
                }
                else
                {
                    background.sprite = null;
                }

                currentWord = newWord;

                words.RemoveAt(words.Count - 1);
                currentGameState = GameStates.INGAME;
            }
            else
            {
                wordDescription.text = "Oh...";
                wordText.text = "Keine Begriffe mehr...";
                currentGameState = GameStates.PASTGAME;
                EndGame();
            }

            currentGameState = GameStates.INGAME;
            correctButton.interactable = true;
            skipButton.interactable = true;
        }

        private void DetectMovement()
        {
            if (currentGameState == GameStates.STARTUP)
            {
                DOTween.To(() => countdownNumbers, x => countdownNumbers = x, 0, 4).OnComplete(StartGame);
                wordText.text = countdownNumbers.ToString();
                currentGameState = GameStates.PREGAME;
                wordDescription.text = "";
            }
            else if (currentGameState == GameStates.PREGAME)
            {
                wordText.text = countdownNumbers.ToString();
            }
            else if(currentGameState == GameStates.INGAME)
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }
            else if (currentGameState == GameStates.WAIT)
            {
                //background.DOColor(new Color(0.2f,0.2f,0.2f), 0.5f).OnComplete(CallNewWord);
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
        }

        public void CorrectAnswer()
        {
            correctButton.interactable = false;
            skipButton.interactable = false;
            currentGameState = GameStates.WAIT;
            background.DOColor(Color.green, 0.3f).OnComplete(TransferToNextWord);
            wordText.text = "KORREKT";
            guessedWords.Add(new WordGuessed(currentWord, true, timeLeft));
        }

        public void SkipAnswer()
        {
            correctButton.interactable = false;
            skipButton.interactable = false;
            currentGameState = GameStates.WAIT;
            background.DOColor(Color.yellow, 0.3f).OnComplete(TransferToNextWord);
            wordText.text = "ÜBERSPRUNGEN";
            guessedWords.Add(new WordGuessed(currentWord, false, timeLeft));
        }

        public void TransferToNextWord()
        {
            currentGameState = GameStates.INGAME;
            background.DOColor(new Color(0.2f,0.2f,0.2f), 0.5f).OnComplete(CallNewWord);
        }

        private void StartGame()
        {
            currentGameState = GameStates.INGAME;
            CallNewWord();
        }
        
        public void EndGame()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            currentGameState = GameStates.PASTGAME;
            endScreen.SetActive(true);
            int totalGuessedWords = 0;
            foreach (WordGuessed guess in guessedWords)
            {
                if (guess.Guessed)
                {
                    totalGuessedWords += 1;
                }
            }
            totalGuessedWordsText.text = "Erratene Begriffe: " + totalGuessedWords;
            
            foreach (Transform child in finishEntryHolder)
            {
                Destroy(child.gameObject);
            }
            
            foreach (WordGuessed word in guessedWords)
            {
                FinishEntry entry = Instantiate(finishEntryPrefab, finishEntryHolder).GetComponent<FinishEntry>();
                entry.Populate(word);
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene(GameScenes.MAINMENU);
        }
    }
}