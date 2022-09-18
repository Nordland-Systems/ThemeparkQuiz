using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThemeparkQuiz
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private WordList[] parks;
        private Dictionary<WordList, ParkSettings> parkSettings;
        private static GameManager instance;

        public static GameManager Instance => instance;

        private void OnEnable()
        {
            if (instance != this && instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public WordList[] Parks
        {
            get => parks;
            set => parks = value;
        }

        public Dictionary<WordList, ParkSettings> ParkSettings
        {
            get => parkSettings;
            set => parkSettings = value;
        }
    }
}