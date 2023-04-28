using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace ThemeparkQuiz
{
    public class BackgroundDownloadManager : MonoBehaviour
    {
        private WebJSONLoader webJsonLoader;
        public static BackgroundDownloadManager instance;
        [SerializeField] private Sprite placeholderBackgroundImage;
        [SerializeField] private Sprite placeholderIconImage;
        
        private void Start()
        {
            if(instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            webJsonLoader = WebJSONLoader.Instance;
            
            if(webJsonLoader == null)
            {
                Debug.LogError("WebJSONLoader is null");
                return;
            }
            
            foreach (WordList wl in webJsonLoader.Wordlists)
            {
                if (wl.BackgroundSprite == null || wl.IconSprite == null)
                {
                    StartCoroutine(GetImages(wl));
                }
            }
        }
        
        private IEnumerator GetImages(WordList wordList)
        {
            UnityWebRequest iconrequest = UnityWebRequestTexture.GetTexture(wordList.IconPath);
            yield return iconrequest.SendWebRequest();
            if (iconrequest.isNetworkError || iconrequest.isHttpError)
            {
                Debug.Log("There was an error downloading " + wordList.IconPath + ": " + iconrequest.error);
                wordList.IconSprite = placeholderIconImage;
            }
            else
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(iconrequest);
                Rect rec = new Rect(0, 0, tex.width, tex.height);
                wordList.IconSprite = Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
            }
            
            UnityWebRequest imagerequest = UnityWebRequestTexture.GetTexture(wordList.BackgroundPath);
            yield return imagerequest.SendWebRequest();
            if (imagerequest.isNetworkError || imagerequest.isHttpError)
            {
                Debug.Log("There was an error downloading " + wordList.BackgroundPath + ": " + imagerequest.error);
                wordList.BackgroundSprite = placeholderBackgroundImage;
            }
            else
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(imagerequest);
                Rect rec = new Rect(0, 0, tex.width, tex.height);
                wordList.BackgroundSprite = Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
            }
        }
    }
}