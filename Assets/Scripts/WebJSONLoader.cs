using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace ThemeparkQuiz
{
    public class WebJSONLoader : MonoBehaviour
    {
        public string apiURL = "https://experiencelog.api.sp-universe.com";
        private string experienceData = "";
        
        private List<WordList> wordlists = new List<WordList>();
        
        public void LoadWords()
        {
            StartCoroutine(GetLocationsFromDatabase());
        }
        
        IEnumerator GetLocationsFromDatabase()
        {
            string locationData = "";
            UnityWebRequest www = UnityWebRequest.Get(apiURL + "/type=ExperienceLocations");
            yield return www.SendWebRequest();
            if (www.error != null)
                Debug.Log("There was an error getting the experiences: " + www.error);
            else
            {
                // Show results as text
                locationData = www.downloadHandler.text;
            }
            Debug.Log(experienceData);

            JSONNode locations = JSON.Parse(locationData);

            foreach (JSONNode location in locations)
            {
                wordlists.Add(new WordList(location["Title"], null, null, GetExperiencesFromDatabase(location["ID"])));
            }
        }

        private IEnumerator List<WordCategory> GetExperiencesFromDatabase(int locationID)
        {
            Dictionary<string, WordCategory> listCategories = new Dictionary<string, WordCategory>();
            
            string experienceData = "";
            UnityWebRequest www = UnityWebRequest.Get(apiURL + "/type=Experience&locationid=" + locationID);
            yield return www.SendWebRequest();
            if (www.error != null)
                Debug.Log("There was an error getting the experiences: " + www.error);
            else
            {
                // Show results as text
                locationData = www.downloadHandler.text;
            }
            Debug.Log(experienceData);

            JSONNode locations = JSON.Parse(locationData);
        }

        private void CreateWordLists(string location)
        {
            
        }
    }
}