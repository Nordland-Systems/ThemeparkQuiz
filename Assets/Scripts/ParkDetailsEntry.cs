using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThemeparkQuiz
{
    public class ParkDetailsEntry : MonoBehaviour
    {
        [SerializeField] private TMP_Text detailsTitleText;
        [SerializeField] private Toggle detailActive;
        private WordList parkList;
        private WordTypes type;
        private ParkOverviewManager overviewManager;

        public void PopulateDetail(WordList parkList, WordTypes type, ParkSettings settings, ParkOverviewManager overviewManager)
        {
            this.parkList = parkList;
            this.type = type;
            this.overviewManager = overviewManager;
            switch (type)
            {
                default:
                    detailsTitleText.text = "ERROR";
                    break;
                case WordTypes.Park:
                    detailsTitleText.text = "Park (1 Begriff)";
                    detailActive.isOn = settings.ParkEnabled;
                    break;
                case WordTypes.Coaster:
                    detailsTitleText.text = "Rides (" + parkList.CoasterTitles.Length + " Begriffe)";
                    detailActive.isOn = settings.CoastersEnabled;
                    break;
                case WordTypes.Flatride:
                    detailsTitleText.text = "Flatrides & Walkthroughs (" + parkList.FlatridesTitles.Length + " Begriffe)";
                    detailActive.isOn = settings.FlatridesEnabled;
                    break;
                case WordTypes.Character:
                    detailsTitleText.text = "Charaktere (" + parkList.CharacterTitles.Length + " Begriffe)";
                    detailActive.isOn = settings.CharactersEnabled;
                    break;
                case WordTypes.Event:
                    detailsTitleText.text = "Events (" + parkList.EventTitles.Length + " Begriffe)";
                    detailActive.isOn = settings.EventsEnabled;
                    break;
                case WordTypes.Area:
                    detailsTitleText.text = "Themenbereiche (" + parkList.AreaTitles.Length + " Begriffe)";
                    detailActive.isOn = settings.AreasEnabled;
                    break;
                case WordTypes.Defunct:
                    detailsTitleText.text = "Ehemalige (" + parkList.DefunctTitles.Length + " Begriffe)";
                    detailActive.isOn = settings.DefunctEnabled;
                    break;
            }
        }

        public void OnToggleChange()
        {
            switch (type)
            {
                default:
                    break;
                case WordTypes.Park:
                    overviewManager.ParkSettings[parkList].ParkEnabled = detailActive.isOn;
                    break;
                case WordTypes.Coaster:
                    overviewManager.ParkSettings[parkList].CoastersEnabled = detailActive.isOn;
                    break;
                case WordTypes.Flatride:
                    overviewManager.ParkSettings[parkList].FlatridesEnabled = detailActive.isOn;
                    break;
                case WordTypes.Character:
                    overviewManager.ParkSettings[parkList].CharactersEnabled = detailActive.isOn;
                    break;
                case WordTypes.Event:
                    overviewManager.ParkSettings[parkList].EventsEnabled = detailActive.isOn;
                    break;
                case WordTypes.Area:
                    overviewManager.ParkSettings[parkList].AreasEnabled = detailActive.isOn;
                    break;
                case WordTypes.Defunct:
                    overviewManager.ParkSettings[parkList].DefunctEnabled = detailActive.isOn;
                    break;
            }
        }
    }
}