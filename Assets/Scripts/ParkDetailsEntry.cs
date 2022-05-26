using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThemeparkQuiz
{
    public class ParkDetailsEntry : MonoBehaviour
    {
        [SerializeField] private TMP_Text detailsTitleText;
        [SerializeField] private TMP_Text wordCountText;
        [SerializeField] private Toggle detailActive;

        public Toggle DetailActive => detailActive;

        private WordList parkList;
        private WordTypes type;
        private ParkOverviewManager overviewManager;

        public void PopulateDetail(WordList parkList, WordTypes type, ParkSettings settings, ParkOverviewManager overviewManager)
        {
            this.parkList = parkList;
            this.type = type;
            this.overviewManager = overviewManager;
            detailsTitleText.text = type.GetNamePlural();
            wordCountText.text = " (" + parkList.GetTitles(type).Length + " Begriffe)";
            detailActive.isOn = settings.EnabledWords[type];
        }

        public void OnToggleChange()
        {
            overviewManager.ParkSettings[parkList].EnabledWords[type] = detailActive.isOn;
        }
    }
}