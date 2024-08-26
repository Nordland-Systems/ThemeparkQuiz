using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace ThemeparkQuiz
{
    public class WebJSONLoader : MonoBehaviour
    {
        public static string placesApi = "https://experiencelogger.com/app-api/places/";
        public static string imageByIDApi = "https://experiencelogger.com/app-api/imagebyid/";
        //public string experiencesURL = "https://experiencelogger.com/app-api/experiences/";

        public JSONNode locationsJSON;
        public JSONNode experiencesJSON;

        [SerializeField] private List<WordList> wordlists;

        private int loadProgress = 0;
        private string progressStatus = "Loading...";

        private static WebJSONLoader instance;

        public int LoadProgress => loadProgress;
        public string ProgressStatus => progressStatus;
        public static WebJSONLoader Instance => instance;
        public List<WordList> Wordlists => wordlists;

        private void Awake()
        {
            if (instance != this && instance != null)
            {
                Destroy(this.GameObject());
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.GameObject());
            }

            wordlists = new List<WordList>();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneChangedEvent;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded += OnSceneChangedEvent;
        }

        public void LoadWords()
        {
            StartCoroutine(GetLocationsFromDatabase());
        }

        IEnumerator GetLocationsFromDatabase()
        {
            loadProgress = 0;

            //1. Loading Locations
            loadProgress = 10;
            progressStatus = "Loading Locations...";
            UnityWebRequest www = UnityWebRequest.Get(placesApi);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("There was an error getting the locations: " + www.error);
            }
            else
            {
                // Put Results in JSONNODE
                locationsJSON = JSON.Parse(www.downloadHandler.text);
            }
            Debug.Log(locationsJSON);

            //2. Loading Experiences
            loadProgress = 25;
            progressStatus = "Loading Experiences...";
            /* UnityWebRequest wwwExperiences = UnityWebRequest.Get(experiencesURL);
            yield return wwwExperiences.SendWebRequest();
            if (wwwExperiences.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("There was an error getting the experiences: " + www.error);
            }
            else
            {
                // Put Results in JSONNODE
                experiencesJSON = JSON.Parse(wwwExperiences.downloadHandler.text);
            } */

            //3. Creating WordLists
            int itemID = 0;
            foreach (JSONNode location in locationsJSON["Items"])
            {
                loadProgress = 50 + (50 / locationsJSON["Count"] * itemID);
                progressStatus = "Loading Words in " + location["Title"] + "...";

                string locationIcon = null;
                string locationImage = null;

                //3.1 Load location Image Paths
                if (location["IconID"] != null)
                {
                    locationIcon = location["IconID"];
                }
                if (location["ImageID"] != null)
                {
                    locationImage = location["ImageID"];
                }

                //3.4 Create WordCategory and put experiences inside
                Dictionary<string, WordCategory> categories = new Dictionary<string, WordCategory>();
                foreach (JSONNode experience in location["Experiences"])
                {
                    Debug.Log(experience);
                    if (experience["ExperienceType"] != null)
                    {
                        if (categories.ContainsKey(experience["ExperienceType"]))
                        {
                            if (experience["ExperienceImageID"] != null)
                            {
                                categories[experience["ExperienceType"]].Words.Add(new Word(experience["ExperienceTitle"], experience["ExperienceImageID"]));
                            }
                            else
                            {
                                categories[experience["ExperienceType"]].Words.Add(new Word(experience["ExperienceTitle"], null));
                            }
                        }
                        else
                        {
                            categories.Add(experience["ExperienceType"], new WordCategory(experience["ExperienceType"]));
                            if (experience["ExperienceImageID"] != null)
                            {
                                categories[experience["ExperienceType"]].Words.Add(new Word(experience["ExperienceTitle"], experience["ExperienceImageID"]));
                            }
                            else
                            {
                                categories[experience["ExperienceType"]].Words.Add(new Word(experience["ExperienceTitle"], null));
                            }
                        }
                    }
                }

                //3.5 Convert Categories to List
                List<WordCategory> categoriesList = new List<WordCategory>();
                foreach (KeyValuePair<string, WordCategory> category in categories)
                {
                    categoriesList.Add(category.Value);
                }

                //3.6 Create WordList
                WordList wl = ScriptableObject.CreateInstance<WordList>();
                wl.Title = location["Title"];
                wl.IconPath = locationIcon;
                wl.BackgroundPath = locationImage;
                wl.WordCategories = categoriesList;
                wordlists.Add(wl);

                itemID += 1;
            }

            loadProgress = 90;
            progressStatus = "Finishing up";

            ParkOverviewManager parkOverviewManager = FindObjectOfType<ParkOverviewManager>();
            if (parkOverviewManager != null)
            {
                parkOverviewManager.RepopulateList(wordlists.ToArray());
            }

            loadProgress = 100;
        }

        private void OnSceneChangedEvent(Scene sceneIn, LoadSceneMode mode)
        {
            ParkOverviewManager parkOverviewManager = GameObject.FindObjectOfType<ParkOverviewManager>();
            if (parkOverviewManager != null)
            {
                parkOverviewManager.RepopulateList(wordlists.ToArray());
            }
        }

        public static string GetImagePathFromAPIById(string id)
        {
            string url = imageByIDApi + id;
            Debug.Log("Getting image from: " + url);
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SendWebRequest();
            while (!www.isDone)
            {
                //Wait
            }

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("There was an error getting the image: " + www.error);
                return null;
            }
            else
            {
                Debug.Log("Image: " + www.downloadHandler.text);
                return www.downloadHandler.text;
            }
        }
    }
}
