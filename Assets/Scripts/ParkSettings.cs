using System.Collections.Generic;
using Unity.VisualScripting;

namespace ThemeparkQuiz
{
    public class ParkSettings
    {
        private Dictionary<WordTypes, bool> enabledWords;

        public ParkSettings(WordList list)
        {
            enabledWords = new Dictionary<WordTypes, bool>();
            enabledWords[WordTypes.Area] = list.AreaTitles.Length > 0;
            enabledWords[WordTypes.Park] = true;
            enabledWords[WordTypes.Coaster] = list.CoasterTitles.Length > 0;
            enabledWords[WordTypes.Flatride] = list.FlatridesTitles.Length > 0;
            enabledWords[WordTypes.Walkthrough] = list.WalkthroughTitles.Length > 0;
            enabledWords[WordTypes.Character] = list.CharacterTitles.Length > 0;
            enabledWords[WordTypes.Event] = list.EventTitles.Length > 0;
            enabledWords[WordTypes.Show] = list.ShowTitles.Length > 0;
            enabledWords[WordTypes.Area] = list.AreaTitles.Length > 0;
            enabledWords[WordTypes.ChangedName] = list.ChangedNamesTitles.Length > 0;
            enabledWords[WordTypes.Defunct] = list.DefunctTitles.Length > 0;
            enabledWords[WordTypes.Other] = list.OthersTitles.Length > 0;
        }

        public ParkSettings(bool parkEnabled, bool coastersEnabled, bool flatridesEnabled, bool walkthroughsEnabled, bool charactersEnabled, 
            bool eventsEnabled, bool showsEnabled, bool areasEnabled, bool changedNameEnabled, bool defunctEnabled, bool othersEnabled)
        {
            enabledWords = new Dictionary<WordTypes, bool>();
            enabledWords[WordTypes.Park] = parkEnabled;
            enabledWords[WordTypes.Coaster] = coastersEnabled;
            enabledWords[WordTypes.Flatride] = flatridesEnabled;
            enabledWords[WordTypes.Walkthrough] = walkthroughsEnabled;
            enabledWords[WordTypes.Character] = charactersEnabled;
            enabledWords[WordTypes.Event] = eventsEnabled;
            enabledWords[WordTypes.Show] = showsEnabled;
            enabledWords[WordTypes.Area] = areasEnabled;
            enabledWords[WordTypes.ChangedName] = changedNameEnabled;
            enabledWords[WordTypes.Defunct] = defunctEnabled;
            enabledWords[WordTypes.Other] = othersEnabled;
        }

        public Dictionary<WordTypes, bool> EnabledWords
        {
            get => enabledWords;
            set => enabledWords = value;
        }
    }
}