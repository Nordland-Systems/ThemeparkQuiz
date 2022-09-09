using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private float rotationUp;
        [SerializeField] private float rotationDown;
        
        [Header("End Screen")]
        [SerializeField] private GameObject endScreen;
        [SerializeField] private TMP_Text totalGuessedWordsText;
        [SerializeField] private Transform finishEntryHolder;
        [SerializeField] private GameObject finishEntryPrefab;
        
        private WordList[] parks;
        private Dictionary<WordList, ParkSettings> parkSettings;

        private GameStates currentGameState = GameStates.PREGAME;
        private Random random;
        private int countdownNumbers = 3;
        private float timeLeft;
        private WordEntry currentWord;
        private List<WordGuessed> guessedWords;

        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            timeForRound = PlayerPrefs.GetInt("maxMinutes", 0) * 60 + PlayerPrefs.GetInt("maxSeconds");
            random = new Random();
            currentGameState = GameStates.STARTUP;
            parks = GameManager.Parks;
            parkSettings = GameManager.ParkSettings;
            words = new List<WordEntry>();
            guessedWords = new List<WordGuessed>();
            LoadWords();
            Rect backgroundRect = background.GetComponent<RectTransform>().rect;
            backgroundRatio.aspectRatio = backgroundRect.width / backgroundRect.height;
            timeLeft = timeForRound;
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
            foreach (WordList park in parks)
            {
                foreach (WordCategory cat in park.WordCategories)
                {
                    if (parkSettings[park].EnabledWords[cat.Type])
                    {
                        foreach (string entry in cat.Words)
                        {
                            words.Add(new WordEntry(entry, park, cat.Type));
                        }
                    }
                }
            }

            words = words.OrderBy(a => random.Next()).ToList();
        }

        public void CallNewWord()
        {
            if (words.Count > 0)
            {
                WordEntry newWord = words[^1];
                
                wordText.text = newWord.word;
                wordDescription.text = newWord.park.Title + " (" + newWord.category.GetNameSingular() + ")";
                if (newWord.park.BackgroundSprite != null)
                {
                    background.sprite = newWord.park.BackgroundSprite;
                }
                else
                {
                    background.sprite = null;
                }

                currentWord = newWord;
                
                words.RemoveAt(words.Count -1);
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
        }

        /*private void DetectMovementOld()
        {
            if (currentGameState == GameStates.STARTUP)
            {
                countdownText.text = "Macht euch bereit!";
                if (Input.deviceOrientation == DeviceOrientation.FaceDown)
                {
                    wordText.text = "Halte das Handy aufrecht!";
                    wordDescription.text = "";
                }
                else if (Input.deviceOrientation == DeviceOrientation.FaceUp)
                {
                    wordText.text = "Halte das Handy aufrecht!";
                    wordDescription.text = "";
                }
                else
                {
                    DOTween.To(() => countdownNumbers, x => countdownNumbers = x, 0, 4).OnComplete(StartGame);
                    wordText.text = countdownNumbers.ToString();
                    currentGameState = GameStates.PREGAME;
                    wordDescription.text = "";
                }
                
            }else if (currentGameState == GameStates.PREGAME)
            {
                wordText.text = countdownNumbers.ToString();
            }
            else if(currentGameState == GameStates.INGAME)
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                if (GyroManager.Instance.Rotation < rotationDown)
                {
                    currentGameState = GameStates.WAIT;
                    background.DOColor(Color.green, 0.3f);
                    wordText.text = "KORREKT";
                    guessedWords.Add(new WordGuessed(currentWord, true, timeLeft));
                }
                else if (GyroManager.Instance.Rotation > rotationUp)
                {
                    currentGameState = GameStates.WAIT;
                    background.DOColor(Color.yellow, 0.3f);
                    wordText.text = "ÜBERSPRUNGEN";
                    guessedWords.Add(new WordGuessed(currentWord, false, timeLeft));
                }
                else
                {
                }
            }
            else if (currentGameState == GameStates.WAIT)
            {
                if (GyroManager.Instance.Rotation < rotationDown || GyroManager.Instance.Rotation > rotationUp)
                {
                }
                else
                {
                    background.DOColor(new Color(0.2f,0.2f,0.2f), 0.5f).OnComplete(CallNewWord);
                    currentGameState = GameStates.INGAME;
                }
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
        }*/
        
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
            
            /*if (currentGameState == GameStates.STARTUP)
            {
                countdownText.text = "Macht euch bereit!";
                if (GyroManager.Instance.Rotation > rotationUp || GyroManager.Instance.Rotation < rotationDown)
                {
                    wordText.text = "Halte das Handy aufrecht!";
                    wordDescription.text = "";
                }
                else
                {
                    DOTween.To(() => countdownNumbers, x => countdownNumbers = x, 0, 4).OnComplete(StartGame);
                    wordText.text = countdownNumbers.ToString();
                    currentGameState = GameStates.PREGAME;
                    wordDescription.text = "";
                }
                
            }else if (currentGameState == GameStates.PREGAME)
            {
                wordText.text = countdownNumbers.ToString();
            }
            else if(currentGameState == GameStates.INGAME)
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                if (GyroManager.Instance.Rotation > rotationUp)
                {
                    CorrectAnswer();
                }
                else if (GyroManager.Instance.Rotation < rotationDown)
                {
                    SkipAnswer();
                }
            }*/
        }

        public void CorrectAnswer()
        {
            currentGameState = GameStates.WAIT;
            background.DOColor(Color.green, 0.3f).OnComplete(TransferToNextWord);
            wordText.text = "KORREKT";
            guessedWords.Add(new WordGuessed(currentWord, true, timeLeft));
        }

        public void SkipAnswer()
        {
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