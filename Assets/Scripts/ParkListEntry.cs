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
            maxNumberOfWords += 1; //Parkname
            if (listEntry.CoasterTitles.Length > 0)
                maxNumberOfWords += listEntry.CoasterTitles.Length;
            if (listEntry.FlatridesTitles.Length > 0)
                maxNumberOfWords += listEntry.FlatridesTitles.Length;
            if (listEntry.WalkthroughTitles.Length > 0)
                maxNumberOfWords += listEntry.WalkthroughTitles.Length;
            if (listEntry.CharacterTitles.Length > 0)
                maxNumberOfWords += listEntry.CharacterTitles.Length;
            if (listEntry.AreaTitles.Length > 0)
                maxNumberOfWords += listEntry.AreaTitles.Length;
            if (listEntry.EventTitles.Length > 0)
                maxNumberOfWords += listEntry.EventTitles.Length;
            if (listEntry.ShowTitles.Length > 0)
                maxNumberOfWords += listEntry.ShowTitles.Length;
            if (listEntry.ChangedNamesTitles.Length > 0)
                maxNumberOfWords += listEntry.ChangedNamesTitles.Length;
            if (listEntry.DefunctTitles != null)
                maxNumberOfWords += listEntry.DefunctTitles.Length;
            if (listEntry.OthersTitles != null)
                maxNumberOfWords += listEntry.OthersTitles.Length;
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

        private void UpdateEntry()
        {
            activatedWords = 0;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Park])
                activatedWords += 1;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Coaster])
                activatedWords += wordList.CoasterTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Flatride])
                activatedWords += wordList.FlatridesTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Walkthrough])
                activatedWords += wordList.WalkthroughTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Character])
                activatedWords += wordList.CharacterTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Event])
                activatedWords += wordList.EventTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Show])
                activatedWords += wordList.ShowTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Area])
                activatedWords += wordList.AreaTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.ChangedName])
                activatedWords += wordList.ChangedNamesTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Defunct])
                activatedWords += wordList.DefunctTitles.Length;
            if (parkOverviewManager.ParkSettings[wordList].EnabledWords[WordTypes.Other])
                activatedWords += wordList.OthersTitles.Length;
            
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