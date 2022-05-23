using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace ThemeparkQuiz
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject preStartText;
        [SerializeField] private TMP_Text wordText;
        private WordList[] parks;
        private Dictionary<WordList, ParkSettings> parkSettings;

        private List<string> words;

        private void Start()
        {
            Random random = new Random();
            
            foreach (WordList park in parks)
            {
                if (parkSettings[park].ParkEnabled)
                {
                    words.Add(park.Title);
                }

                if (parkSettings[park].CoastersEnabled)
                {
                    foreach (string entry in park.CoasterTitles)
                    {
                        words.Add(entry);
                    }
                }
                
                if (parkSettings[park].FlatridesEnabled)
                {
                    foreach (string entry in park.FlatridesTitles)
                    {
                        words.Add(entry);
                    }
                }
                
                if (parkSettings[park].CharactersEnabled)
                {
                    foreach (string entry in park.CharacterTitles)
                    {
                        words.Add(entry);
                    }
                }
                
                if (parkSettings[park].EventsEnabled)
                {
                    foreach (string entry in park.EventTitles)
                    {
                        words.Add(entry);
                    }
                }
                
                if (parkSettings[park].AreasEnabled)
                {
                    foreach (string entry in park.AreaTitles)
                    {
                        words.Add(entry);
                    }
                }
            }

            words = words.OrderBy(a => random.Next()).ToList();
            CallNewWord();
        }

        private void CallNewWord()
        {
            string newWord = "";
            if (words.Count > 0)
            {
                newWord = words[^1];
                words.RemoveAt(words.Count -1);
            }

            wordText.text = newWord;
        }
    }
}