using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThemeparkQuiz
{
    public class ParkListEntry : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text numberOfWordsText;
        [SerializeField] private Image parkIcon;
        [SerializeField] private Image parkBanner;
        [SerializeField] private Image parkActivatedIndicator;
        private string title;
        private ParkOverviewManager parkOverviewManager;
        private WordList wordList;
        private int activatedWords = 0;
        private int maxNumberOfWords;

        private void Update()
        {
            UpdateEntry();
        }

        public void PopulateEntry(WordList listEntry, ParkOverviewManager parkOverviewManager)
        {
            title = listEntry.name;
            maxNumberOfWords = listEntry.CoasterTitles.Length
                               + listEntry.FlatridesTitles.Length
                               + listEntry.CharacterTitles.Length
                               + listEntry.AreaTitles.Length
                               + listEntry.EventTitles.Length
                               + listEntry.DefunctTitles.Length
                               + 1;//For the parkname itself
            numberOfWordsText.text = "(" + activatedWords + " / " + maxNumberOfWords + " Begriffe)";
            titleText.text = title;
            this.parkOverviewManager = parkOverviewManager;
            wordList = listEntry;
            if (listEntry.IconSprite != null)
            {
                parkIcon.sprite = listEntry.IconSprite;
            }
            if (listEntry.BackgroundSprite != null)
            {
                RectTransform parkBannerRect = parkBanner.GetComponent<RectTransform>();
                parkBanner.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = parkBannerRect.rect.width / parkBannerRect.rect.height;
                parkBanner.sprite = listEntry.BackgroundSprite;
            }
        }

        public void UpdateEntry()
        {
            activatedWords = 0;
            if (parkOverviewManager.ParkSettings[wordList].ParkEnabled)
                activatedWords += 1;
            if (parkOverviewManager.ParkSettings[wordList].CoastersEnabled)
                activatedWords += wordList.CoasterTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].FlatridesEnabled)
                activatedWords += wordList.FlatridesTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].CharactersEnabled)
                activatedWords += wordList.CharacterTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EventsEnabled)
                activatedWords += wordList.EventTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].AreasEnabled)
                activatedWords += wordList.AreaTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].DefunctEnabled)
                activatedWords += wordList.DefunctTitles.Length;
            
            if (activatedWords <= 0)
            {
                parkActivatedIndicator.color = Color.red;
            } 
            else if(activatedWords < maxNumberOfWords) 
            {
                parkActivatedIndicator.color = Color.yellow;
            } 
            else {
                parkActivatedIndicator.color = Color.green;
            }
            
            numberOfWordsText.text = "(" + activatedWords + " / " + maxNumberOfWords + " Begriffe)";
        }

        public void OpenDetails()
        {
            parkOverviewManager.OpenDetails(wordList);
        }
    }
}