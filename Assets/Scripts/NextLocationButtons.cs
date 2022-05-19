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
    public LookAtConstraint nicCanvas;
    public LookAtConstraint mikeCanvas;
    public LookAtConstraint benCanvas;
    public GameObject start;
    public GameObject end;
    public CanvasGroup _canvasGroup;
    public ToggleGroup _toggleGroup;
    public Button showLocationButton;
    public Button hideLocationButton;

    // private void Awake()
    // {
    //     nicCanvas = GetComponent<LookAtConstraint>();
    //     mikeCanvas = GetComponent<LookAtConstraint>();
    //     benCanvas = GetComponent<LookAtConstraint>();
    //     nicCanvas.constraintActive = false;
    //     mikeCanvas.constraintActive = false;
    //     benCanvas.constraintActive = false;
    // }

    // Start is called before the first frame update
    void Start()
    {
        nicToMike.onClick.AddListener(changeToMike);
        mikeToBen.onClick.AddListener(changeToBen);
        benToNic.onClick.AddListener(changeToNic);
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }

    void changeToNic()
    {
        HideLocOnClick();
        end.transform.position = nic.transform.position;
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }
    void changeToMike()
    {
        HideLocOnClick();
        end.transform.position = mike.transform.position;
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }
    void changeToBen()
    {
        HideLocOnClick();
        end.transform.position = ben.transform.position;
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }
    void HideLocOnClick()
    {
        showLocationButton.gameObject.SetActive(true);
        hideLocationButton.gameObject.SetActive(false);
        _canvasGroup.DOFade(0, 1);
        _canvasGroup.blocksRaycasts = true;
        _toggleGroup.SetAllTogglesOff(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
