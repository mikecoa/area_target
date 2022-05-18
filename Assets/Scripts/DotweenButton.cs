using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;

public class DotweenButton : MonoBehaviour
{
    // public Transform nicButton;
    // public Transform mikeButton;
    // public Transform benButton;
    public GameObject end;
    public GameObject start;
    public GameObject showLocationButton;
    public GameObject hideLocationButton;

    [SerializeField] private CanvasGroup _canvasGroup;


    private void Start()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, 0);
    }

    public void ShowLoc()
    {
        showLocationButton.SetActive(false);
        hideLocationButton.SetActive(true);
        
        _canvasGroup.DOFade(1, 1);
        _canvasGroup.blocksRaycasts = true;
        /*nicButton.DOMoveY(972.4f, 1, false);
        mikeButton.DOMoveY(856.4f, 1, false);
        benButton.DOMoveY(738.4f, 1, false);*/
    }

    public void HideLoc()
    {
        showLocationButton.SetActive(true);
        hideLocationButton.SetActive(false);
        _canvasGroup.blocksRaycasts = false;
        end.transform.position = start.transform.position;
        _canvasGroup.DOFade(0, 1);
        /*nicButton.DOMoveY(1150f, 1, false);
        mikeButton.DOMoveY(1150f, 1, false);
        benButton.DOMoveY(1150f, 1, false);*/
        
    }
}
