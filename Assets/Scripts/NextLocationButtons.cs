using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class NextLocationButtons : MonoBehaviour
{
    public Button nicToMike;
    public Button mikeToBen;
    public Button benToNic;
    public GameObject nic;
    public GameObject mike;
    public GameObject ben;
    public GameObject end;
    public CanvasGroup _canvasGroup;
    public ToggleGroup _toggleGroup;
    public Button showLocationButton;
    public Button hideLocationButton;

    public NavMan NavMan;

    // Start is called before the first frame update
    void Start()
    {
        nicToMike.onClick.AddListener(changeToMike);
        mikeToBen.onClick.AddListener(changeToBen);
        benToNic.onClick.AddListener(changeToNic);
    }

    void changeToNic()
    {
        HideLocOnClick();
        end.transform.position = nic.transform.position;
        NavMan.currentLookAtConstraint.constraintActive = false;
    }
    void changeToMike()
    {
        HideLocOnClick();
        end.transform.position = mike.transform.position;
        NavMan.currentLookAtConstraint.constraintActive = false;
    }
    void changeToBen()
    {
        HideLocOnClick();
        end.transform.position = ben.transform.position;
        NavMan.currentLookAtConstraint.constraintActive = false;
    }
    void HideLocOnClick()
    {
        showLocationButton.gameObject.SetActive(true);
        hideLocationButton.gameObject.SetActive(false);
        _canvasGroup.DOFade(0, 1);
        _canvasGroup.blocksRaycasts = true;
        _toggleGroup.SetAllTogglesOff(true);
    }
    
}
