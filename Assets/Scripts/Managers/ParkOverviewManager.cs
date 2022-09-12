using System;
using System.Collections.Generic;
using System.Linq;
using ThemeparkQuiz;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParkOverviewManager : MonoBehaviour
{
    [Header("Park List")]
    [SerializeField] private WordList[] parks;
    [SerializeField] private Transform parkListHolder;
    [SerializeField] private GameObject parkEntryPrefab;
    [Header("Park Details")]
    [SerializeField] private GameObject parkDetails;
    [SerializeField] private TMP_Text parkDetailsTitle;
    [SerializeField] private GameObject parkDetailsPrefab;
    [SerializeField] private Transform parkDetailsHolder;
    
    private Dictionary<WordList, ParkSettings> parkSettings;
    private WordList openDetails;
    private List<Toggle> openDetailToggles;

    public Dictionary<WordList, ParkSettings> ParkSettings
    {
        get => parkSettings;
        set => parkSettings = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        parkSettings = new Dictionary<WordList, ParkSettings>();
        openDetailToggles = new List<Toggle>();
        PopulateList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (parkDetails.activeInHierarchy)
            {
                parkDetails.SetActive(false);
            }
            else
            {
                SceneManager.LoadScene(GameScenes.MAINMENU);
            }
        }
    }

    private void PopulateList()
    {
        foreach (WordList p in parks)
        {
            ParkListEntry ple = Instantiate(parkEntryPrefab, parkListHolder).GetComponent<ParkListEntry>();
            ple.PopulateEntry(p, this);
            parkSettings.Add(p,new ParkSettings(p));
        }
    }

    public void OpenDetails(WordList listEntry)
    {
        openDetails = listEntry;
        foreach (Transform child in parkDetailsHolder)
        {
            Destroy(child.gameObject);
        }

        foreach (WordCategory cat in listEntry.WordCategories)
        {
            ParkDetailsEntry details =
                Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
            details.PopulateDetail(listEntry, cat.Type, parkSettings[listEntry], this);
            openDetailToggles.Add(details.DetailActive);
        }
        
        parkDetailsTitle.text = listEntry.Title;
        parkDetails.SetActive(true);
    }

    public void SelectAll()
    {
        if (!parkDetails.activeInHierarchy)
        {
            WordList[] settingKeys = parkSettings.Keys.ToArray();
            foreach (WordList setting in settingKeys)
            {
                string[] enabledWords = parkSettings[setting].EnabledWords.Keys.ToArray();
                foreach (string entry in enabledWords)
                {
                    parkSettings[setting].EnabledWords[entry] = true;
                }
            }
        }
        else
        {
            SelectAll(openDetails);
            
            foreach (Toggle tgl in openDetailToggles)
            {
                tgl.isOn = true;
            }
        }
    }

    public void SelectAll(WordList park)
    {
        string[] enabledWords = parkSettings[park].EnabledWords.Keys.ToArray();
        foreach (string entry in enabledWords)
        {
            parkSettings[park].EnabledWords[entry] = true;
        }
    }
    
    public void DeSelectAll()
    {
        if (!parkDetails.activeInHierarchy)
        {
            WordList[] settingKeys = parkSettings.Keys.ToArray();
            foreach (WordList setting in settingKeys)
            {
                string[] enabledWords = parkSettings[setting].EnabledWords.Keys.ToArray();
                foreach (string entry in enabledWords)
                {
                    parkSettings[setting].EnabledWords[entry] = false;
                }
            }
        }
        else
        {
            DeSelectAll(openDetails);
            
            foreach (Toggle tgl in openDetailToggles)
            {
                tgl.isOn = false;
            }
        }
    }

    public void DeSelectAll(WordList park)
    {
        string[] enabledWords = parkSettings[park].EnabledWords.Keys.ToArray();
        foreach (string entry in enabledWords)
        {
            parkSettings[park].EnabledWords[entry] = false;
        }
    }

    public void StartGame()
    {
        GameManager.Parks = parks;
        GameManager.ParkSettings = parkSettings;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene(GameScenes.GAMESCENE);
    }
}
