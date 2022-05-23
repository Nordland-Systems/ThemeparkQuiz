using System.Collections.Generic;

namespace ThemeparkQuiz
{
    public static class GameManager
    {
        private static WordList[] parks;
        private static Dictionary<WordList, ParkSettings> parkSettings;

        public static WordList[] Parks
        {
            get => parks;
            set => parks = value;
        }

        public static Dictionary<WordList, ParkSettings> ParkSettings
        {
            get => parkSettings;
            set => parkSettings = value;
        }
    }
}