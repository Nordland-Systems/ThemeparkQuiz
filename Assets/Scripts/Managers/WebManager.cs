using Unity.VisualScripting;
using UnityEngine;

namespace ThemeparkQuiz
{
    public class WebManager : MonoBehaviour
    {
        private static WebManager instance;

        public static WebManager Instance => instance;

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

        private void Start()
        {
            WebJSONLoader.Instance.LoadWords();
            Debug.Log("Loading Words...");
        }
    }
}