using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace ThemeparkQuiz
{
    public class WebJSONLoader : MonoBehaviour
    {
        public string locationsURL = "https://experiencelogger.sp-universe.com/api/v1/App-ExperienceDatabase-ExperienceLocation.json";
        
        private List<WordList> wordlists = new List<WordList>();
        
        public void LoadWords()
        {
            StartCoroutine(GetLocationsFromDatabase());
        }
        
        IEnumerator GetLocationsFromDatabase()
        {
            string locationData = "";
            UnityWebRequest www = UnityWebRequest.Get(locationsURL);
            yield return www.SendWebRequest();
            if (www.error != null)
                Debug.Log("There was an error getting the locations: " + www.error);
            else
            {
                // Show results as text
                locationData = www.downloadHandler.text;
            }
            Debug.Log(locationData);

            JSONNode locations = JSON.Parse(locationData);

            foreach (JSONNode location in locations)
            {
                string experiencesData = "";
                wordlists.Add(new WordList(location["Title"], null, null, new List<WordCategory>()));
                
                UnityWebRequest www2 = UnityWebRequest.Get("https://experiencelogger.sp-universe.com/api/v1/App-ExperienceDatabase-ExperienceLocation.json/" + location["ID"] + "/Experiences");
                yield return www.SendWebRequest();
                if (www2.error != null)
                    Debug.Log("There was an error getting the experiences: " + www2.error);
                else
                {
                    experiencesData = www2.downloadHandler.text;
                }
                Debug.Log(experiencesData);
                
            }
        }
    }
}