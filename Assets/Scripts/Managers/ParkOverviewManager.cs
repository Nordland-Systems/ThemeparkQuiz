using System;
using System.Collections.Generic;
using ThemeparkQuiz;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Dictionary<WordList, ParkSettings> ParkSettings
    {
        get => parkSettings;
        set => parkSettings = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        parkSettings = new Dictionary<WordList, ParkSettings>();
        PopulateList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(GameScenes.MAINMENU);
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
        foreach (Transform child in parkDetailsHolder)
        {
            Destroy(child.gameObject);
        }

        foreach (WordCategory cat in listEntry.WordCategories)
        {
            ParkDetailsEntry details =
                Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
            details.PopulateDetail(listEntry, cat.Type, parkSettings[listEntry], this);
        }

        parkDetailsTitle.text = listEntry.Title;

        parkDetails.SetActive(true);
    }

    public void SelectAll()
    {
        foreach (KeyValuePair<WordList, ParkSettings> setting in parkSettings)
        {
            foreach (KeyValuePair<WordTypes, bool> enabledWords in setting.Value.EnabledWords)
            {
                setting.Value.EnabledWords[enabledWords.Key] = true;
            }
        }
    }
    
    public void DeSelectAll()
    {
        foreach (KeyValuePair<WordList, ParkSettings> setting in parkSettings)
        {
            foreach (KeyValuePair<WordTypes, bool> enabledWords in setting.Value.EnabledWords)
            {
                setting.Value.EnabledWords[enabledWords.Key] = false;
            }
        }
    }

    public void StartGame()
    {
        GameManager.Parks = parks;
        GameManager.ParkSettings = parkSettings;
        SceneManager.LoadScene(GameScenes.GAMESCENE);
    }
}
