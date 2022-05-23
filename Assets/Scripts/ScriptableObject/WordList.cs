using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThemeparkQuiz
{
    /// <summary>
    /// A scriptable object for all Words
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New Wordlist", menuName = "ThemeparkQuiz/Wordlist", order = 1)]
    public class WordList : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private Sprite backgroundSprite;
        [Header("Words")]
        [SerializeField] private string[] coasterTitles;
        [SerializeField] private string[] flatridesTitles;
        [SerializeField] private string[] walkthroughTitles;
        [SerializeField] private string[] characterTitles;
        [SerializeField] private string[] eventTitles;
        [SerializeField] private string[] showTitles;
        [SerializeField] private string[] areaTitles;
        [SerializeField] private string[] changedNamesTitles;
        [SerializeField] private string[] defunctTitles;
        [SerializeField] private string[] othersTitles;

        public string Title => title;
        public Sprite IconSprite => iconSprite;
        public Sprite BackgroundSprite => backgroundSprite;
        public string[] CoasterTitles => coasterTitles;
        public string[] FlatridesTitles => flatridesTitles;
        public string[] WalkthroughTitles => walkthroughTitles;
        public string[] CharacterTitles => characterTitles;
        public string[] EventTitles => eventTitles;
        public string[] ShowTitles => showTitles;
        public string[] AreaTitles => areaTitles;
        public string[] ChangedNamesTitles => changedNamesTitles;
        public string[] DefunctTitles => defunctTitles;
        public string[] OthersTitles => othersTitles;

        public string[] GetTitles(WordTypes type)
        {
            switch (type)
            {
                default:
                    return null;
                case WordTypes.Area:
                    return areaTitles;
                case WordTypes.Character:
                    return characterTitles;
                case WordTypes.Coaster:
                    return coasterTitles;
                case WordTypes.Defunct:
                    return defunctTitles;
                case WordTypes.Event:
                    return eventTitles;
                case WordTypes.Flatride:
                    return flatridesTitles;
                case WordTypes.Other:
                    return othersTitles;
                case WordTypes.Park:
                    string[] s = {title};
                    return s;
                case WordTypes.Show:
                    return showTitles;
                case WordTypes.Walkthrough:
                    return walkthroughTitles;
                case WordTypes.ChangedName:
                    return changedNamesTitles;
            }
        }
    }
}
