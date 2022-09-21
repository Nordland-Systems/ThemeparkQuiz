using ThemeparkQuiz;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Image slider;
    [SerializeField] private TMP_Text text;
    [SerializeField] private string mainMenuScene;
    private int loadingPercent;
    
    private void Update()
    {
        loadingPercent = WebJSONLoader.Instance.LoadProgress;
        slider.fillAmount = loadingPercent * 0.01f;
        text.text = WebJSONLoader.Instance.ProgressStatus;

        if (loadingPercent >= 100)
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}
