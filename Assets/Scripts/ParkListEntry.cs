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
            foreach (WordCategory cat in listEntry.WordCategories)
            {
                maxNumberOfWords += cat.Words.Length;
            }
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
            foreach (WordCategory cat in wordList.WordCategories)
            {
                if (parkOverviewManager.ParkSettings[wordList].EnabledWords[cat.Type])
                {
                    activatedWords += cat.Words.Length;
                }
            }
            
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

        public void ChangeActivation()
        {
            if (activatedWords > 0)
            {
                parkOverviewManager.DeSelectAll(wordList);
            }
            else
            {
                parkOverviewManager.SelectAll(wordList);
            }
        }
    }
}