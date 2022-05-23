using System.Collections;
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

    private void PopulateList()
    {
        foreach (WordList p in parks)
        {
            ParkListEntry ple = Instantiate(parkEntryPrefab, parkListHolder).GetComponent<ParkListEntry>();
            ple.PopulateEntry(p, this);
            parkSettings.Add(p,new ParkSettings(true,
                p.CoasterTitles.Length != 0, 
                p.FlatridesTitles.Length != 0,
                p.CharacterTitles.Length != 0,
                p.EventTitles.Length != 0,
                p.AreaTitles.Length != 0
                ));
        }
    }

    public void OpenDetails(WordList listEntry)
    {
        foreach (Transform child in parkDetailsHolder)
        {
            Destroy(child.gameObject);
        }
        
        ParkDetailsEntry detailsA = Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
        detailsA.PopulateDetail(listEntry,WordTypes.Park, parkSettings[listEntry], this);

        if (listEntry.CoasterTitles.Length != 0)
        {
            ParkDetailsEntry details = Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
            details.PopulateDetail(listEntry,WordTypes.Coaster, parkSettings[listEntry], this);
        }
        if (listEntry.FlatridesTitles.Length != 0)
        {
            ParkDetailsEntry details = Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
            details.PopulateDetail(listEntry,WordTypes.Flatride, parkSettings[listEntry], this);
        }
        if (listEntry.CharacterTitles.Length != 0)
        {
            ParkDetailsEntry details = Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
            details.PopulateDetail(listEntry,WordTypes.Character, parkSettings[listEntry], this);
        }
        if (listEntry.EventTitles.Length != 0)
        {
            ParkDetailsEntry details = Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
            details.PopulateDetail(listEntry,WordTypes.Event, parkSettings[listEntry], this);
        }
        if (listEntry.AreaTitles.Length != 0)
        {
            ParkDetailsEntry details = Instantiate(parkDetailsPrefab, parkDetailsHolder).GetComponent<ParkDetailsEntry>();
            details.PopulateDetail(listEntry,WordTypes.Area, parkSettings[listEntry], this);
        }

        parkDetailsTitle.text = listEntry.Title;

        parkDetails.SetActive(true);
    }

    public void SelectAll()
    {
        foreach (KeyValuePair<WordList, ParkSettings> setting in parkSettings)
        {
            setting.Value.ParkEnabled = true;
            setting.Value.CoastersEnabled = true;
            setting.Value.FlatridesEnabled = true;
            setting.Value.CharactersEnabled = true;
            setting.Value.EventsEnabled = true;
            setting.Value.AreasEnabled = true;
        }
    }
    
    public void DeSelectAll()
    {
        foreach (KeyValuePair<WordList, ParkSettings> setting in parkSettings)
        {
            setting.Value.ParkEnabled = false;
            setting.Value.CoastersEnabled = false;
            setting.Value.FlatridesEnabled = false;
            setting.Value.CharactersEnabled = false;
            setting.Value.EventsEnabled = false;
            setting.Value.AreasEnabled = false;
        }
    }

    public void StartGame()
    {
        GameManager.Parks = parks;
        GameManager.ParkSettings = parkSettings;
        SceneManager.LoadScene(GameScenes.GAMESCENE);
    }
}
