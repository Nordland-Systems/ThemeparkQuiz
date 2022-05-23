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
        [SerializeField] private Image background;
        [SerializeField] private AspectRatioFitter backgroundRatio;
        [SerializeField] private TMP_Text wordText;
        [SerializeField] private TMP_Text wordDescription;
        [SerializeField] private List<WordEntry> words;
        private WordList[] parks;
        private Dictionary<WordList, ParkSettings> parkSettings;

        private GameStates currentGameState = GameStates.PREGAME;
        private Random random;
        int countdownNumbers = 3;
        

        private void Start()
        {
            random = new Random();
            currentGameState = GameStates.STARTUP;
            parks = GameManager.Parks;
            parkSettings = GameManager.ParkSettings;
            words = new List<WordEntry>();
            LoadWords();
            Rect backgroundRect = background.GetComponent<RectTransform>().rect;
            backgroundRatio.aspectRatio = backgroundRect.width / backgroundRect.height;
        }

        private void Update()
        {
            DetectMovement();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(GameScenes.PARKOVERVIEW);
            }
        }

        private void LoadWords()
        {
            foreach (WordList park in parks)
            {
                if (parkSettings[park].ParkEnabled)
                {
                    words.Add(new WordEntry(park.Title, park, WordTypes.Park));
                }

                if (parkSettings[park].CoastersEnabled)
                {
                    foreach (string entry in park.CoasterTitles)
                    {
                        words.Add(new WordEntry(entry, park, WordTypes.Coaster));
                    }
                }
                
                if (parkSettings[park].FlatridesEnabled)
                {
                    foreach (string entry in park.FlatridesTitles)
                    {
                        words.Add(new WordEntry(entry, park, WordTypes.Flatride));
                    }
                }
                
                if (parkSettings[park].CharactersEnabled)
                {
                    foreach (string entry in park.CharacterTitles)
                    {
                        words.Add(new WordEntry(entry, park, WordTypes.Character));
                    }
                }
                
                if (parkSettings[park].EventsEnabled)
                {
                    foreach (string entry in park.EventTitles)
                    {
                        words.Add(new WordEntry(entry, park, WordTypes.Event));
                    }
                }
                
                if (parkSettings[park].AreasEnabled)
                {
                    foreach (string entry in park.AreaTitles)
                    {
                        words.Add(new WordEntry(entry, park, WordTypes.Area));
                    }
                }
                
                if (parkSettings[park].DefunctEnabled)
                {
                    foreach (string entry in park.DefunctTitles)
                    {
                        words.Add(new WordEntry(entry, park, WordTypes.Defunct));
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
                wordDescription.text = newWord.park.Title + " (" + newWord.getCategoryName() + ")";
                if (newWord.park.BackgroundSprite != null)
                {
                    background.sprite = newWord.park.BackgroundSprite;
                }
                else
                {
                    background.sprite = null;
                }
                
                words.RemoveAt(words.Count -1);
                currentGameState = GameStates.INGAME;
            }
            else
            {
                wordDescription.text = "Oh...";
                wordText.text = "Keine Begriffe mehr...";
                currentGameState = GameStates.PASTGAME;
            }
        }

        private void DetectMovement()
        {
            if (currentGameState == GameStates.STARTUP)
            {
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
                    DOTween.To(() => countdownNumbers, x => countdownNumbers = x, 0, 3).OnComplete(StartGame);
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
                if (Input.deviceOrientation == DeviceOrientation.FaceDown)
                {
                    currentGameState = GameStates.WAIT;
                    background.DOColor(Color.green, 0.3f);
                    wordText.text = "KORREKT";
                }
                else if (Input.deviceOrientation == DeviceOrientation.FaceUp)
                {
                    currentGameState = GameStates.WAIT;
                    background.DOColor(Color.yellow, 0.3f);
                    wordText.text = "ÜBERSPRUNGEN";
                }
                else
                {
                    Debug.Log("Neutral");
                }
            }
            else if (currentGameState == GameStates.WAIT)
            {
                if (Input.deviceOrientation == DeviceOrientation.FaceDown)
                {
                }
                else if (Input.deviceOrientation == DeviceOrientation.FaceUp)
                {
                }
                else
                {
                    background.DOColor(new Color(0.2f,0.2f,0.2f), 1f).OnComplete(CallNewWord);
                    currentGameState = GameStates.INGAME;
                }
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
        }

        private void StartGame()
        {
            currentGameState = GameStates.INGAME;
            CallNewWord();
        }
    }
}