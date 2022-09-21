﻿using System;
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
        public string locationsURL = "https://experiencelogger.sp-universe.com/api/v1/App-ExperienceDatabase-ExperienceLocation.json";
        
        [SerializeField] private List<WordList> wordlists = new List<WordList>();

        [SerializeField] private ParkOverviewManager parkOverviewManager;
        private static WebJSONLoader instance;

        private int loadProgress = 0;
        private string progressStatus = "Loading...";

        public int LoadProgress => loadProgress;
        public string ProgressStatus => progressStatus;

        public static WebJSONLoader Instance => instance;

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
            progressStatus = "Loading Locations...";
            string locationData = "";
            UnityWebRequest www = UnityWebRequest.Get(locationsURL);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("There was an error getting the locations: " + www.error);
            else
            {
                // Show results as text
                locationData = www.downloadHandler.text;
            }

            JSONNode locations = JSON.Parse(locationData);

            Debug.Log(locations["totalSize"]);
            if (locations["totalSize"] != null)
            {
                Debug.Log(locations["totalSize"]);
                loadProgress = (100 / locations["totalSize"]) * 0;
            }

            int itemID = 0;
            foreach (JSONNode location in locations["items"])
            {
                progressStatus = "Loading Location " + location["Title"];
                string experiencesData = "";
                Dictionary<string, WordCategory> categories = new Dictionary<string, WordCategory>();

                UnityWebRequest www2 = UnityWebRequest.Get("https://experiencelogger.sp-universe.com/api/v1/App-ExperienceDatabase-ExperienceLocation/" + location["ID"] + "/Experiences.json");
                yield return www2.SendWebRequest();
                if (www.result == UnityWebRequest.Result.ProtocolError)
                    Debug.Log("There was an error getting the experiences: " + www2.error);
                else
                {
                    experiencesData = www2.downloadHandler.text;
                }

                JSONNode experiences = JSON.Parse(experiencesData);
                categories = new Dictionary<string, WordCategory>();

                progressStatus = "Loading Experiences in " + location["Title"];
                foreach (JSONNode experience in experiences["items"])
                {
                    Debug.Log(experience["Type"]);
                    if (categories.ContainsKey(experience["Type"]))
                    {
                        categories[experience["Type"]].Words.Add(new Word(experience["Title"], null));
                    }
                    else
                    {
                        categories.Add(experience["Type"], new WordCategory(experience["Type"]));
                        categories[experience["Type"]].Words.Add(new Word(experience["Title"], null));
                    }
                }

                List<WordCategory> allCategories = categories.Values.ToList();
                WordList wl = ScriptableObject.CreateInstance<WordList>();
                wl.WordCategories = allCategories;
                wl.Title = location["Title"];
                wl.name = location["Title"];
                progressStatus = "Loading Images for " + location["Title"];
                if (location["LocationIcon"] != null)
                {
                    UnityWebRequest imagerequest = UnityWebRequestTexture.GetTexture(location["LocationIcon"]);
                    yield return imagerequest.SendWebRequest();
                    if (imagerequest.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log(imagerequest.error);
                    }
                    else
                    {
                        Texture2D tex = DownloadHandlerTexture.GetContent(imagerequest);
                        Rect rec = new Rect(0, 0, tex.width, tex.height);
                        Sprite spr = Sprite.Create(tex,rec,new Vector2(0.5f,0.5f),100);
                        wl.IconSprite = spr;
                    }
                }
                if (location["LocationImage"] != null)
                {
                    UnityWebRequest backgroundrequest = UnityWebRequestTexture.GetTexture(location["LocationImage"]);
                    yield return backgroundrequest.SendWebRequest();
                    if (backgroundrequest.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log(backgroundrequest.error);
                    }
                    else
                    {
                        Texture2D tex = DownloadHandlerTexture.GetContent(backgroundrequest);
                        Rect rec = new Rect(0, 0, tex.width, tex.height);
                        Sprite spr = Sprite.Create(tex,rec,new Vector2(0.5f,0.5f),100);
                        wl.BackgroundSprite = spr;
                    }
                }
                wordlists.Add(wl);

                itemID += 1;
                loadProgress = 100 / locations["totalSize"] * itemID;
            }
            
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
    }
}