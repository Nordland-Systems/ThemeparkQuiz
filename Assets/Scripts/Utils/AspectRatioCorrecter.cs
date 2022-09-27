using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioCorrecter : MonoBehaviour
{
    [SerializeField] private Image imageToCorrect;
    [SerializeField] private AspectRatioFitter aspectRatioFitter;

    private void OnValidate()
    {
        if (imageToCorrect != null)
        {
            CorrectAspectRatio(imageToCorrect);
        }
    }

    void Update()
    {
        if (imageToCorrect != null)
        {
            CorrectAspectRatio(imageToCorrect);
        }
    }
    
    private void CorrectAspectRatio(Image image)
    {
        var ratio = image.sprite.rect.width / image.sprite.rect.height;
        aspectRatioFitter.aspectRatio = ratio;
    }
}
