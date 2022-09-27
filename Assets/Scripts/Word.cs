using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace ThemeparkQuiz
{
    [Serializable]
    public class Word
    {
        public string content;
        public string backgroundImageLink;
        public Sprite backgroundImage;

        public string Content
        {
            get => content;
            set => content = value;
        }

        public Sprite BackgroundImage
        {
            get => backgroundImage;
            set => backgroundImage = value;
        }

        public Word(String content, String backgroundImageLink)
        {
            this.content = content;
            this.backgroundImageLink = backgroundImageLink;
        }

        public IEnumerator LoadImage()
        {
            if (backgroundImageLink != null)
            {
                UnityWebRequest imagerequest = UnityWebRequestTexture.GetTexture(backgroundImageLink);
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
                    backgroundImage = spr;
                }
            }
        }
    }
}