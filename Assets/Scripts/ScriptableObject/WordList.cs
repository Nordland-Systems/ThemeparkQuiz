using UnityEngine;

namespace ThemeparkQuiz
{
    /// <summary>
    /// A scriptable object for all Words
    /// </summary>
    [CreateAssetMenu(fileName = "New Wordlist", menuName = "ThemeparkQuiz/Wordlist", order = 1)]

    public class WordList : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private Sprite backgroundSprite;
        [SerializeField] private string[] coasterTitles;
        [SerializeField] private string[] flatridesTitles;
        [SerializeField] private string[] characterTitles;
        [SerializeField] private string[] eventTitles;
        [SerializeField] private string[] areaTitles;
        [SerializeField] private string[] defunctTitles;

        public string Title => title;
        public Sprite IconSprite => iconSprite;
        public Sprite BackgroundSprite => backgroundSprite;
        public string[] CoasterTitles => coasterTitles;
        public string[] FlatridesTitles => flatridesTitles;
        public string[] CharacterTitles => characterTitles;
        public string[] EventTitles => eventTitles;
        public string[] AreaTitles => areaTitles;
        public string[] DefunctTitles => defunctTitles;
    }
}
