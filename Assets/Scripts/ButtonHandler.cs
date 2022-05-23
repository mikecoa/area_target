using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;


public class ButtonHandler : MonoBehaviour
{
    public GameObject end;
    public GameObject start;
    public GameObject Nicolas;
    public GameObject Michael;
    public GameObject Benedict;

    [SerializeField] private Toggle nicToggle;
    [SerializeField] private Toggle mikeToggle;
    [SerializeField] private Toggle benToggle;

    public NavManager navManager;
    
    private void Start()
    {
        nicToggle.onValueChanged.AddListener(OnNicToggleOnClick);
        mikeToggle.onValueChanged.AddListener(OnMikeToggleOnClick);
        benToggle.onValueChanged.AddListener(OnBenToggleOnClick);
    }

    private void OnNicToggleOnClick(bool isOn)
    {
        if (isOn)
        {
            end.transform.position = Nicolas.transform.position;
            navManager.DestroySpheres();
            navManager.FindPath();
        }
        else
        {
            end.transform.position = start.transform.position;
            navManager.DestroySpheres();
            
        }
    }
    
    private void OnMikeToggleOnClick(bool isOn)
    {
        if (isOn)
        {
            end.transform.position = Michael.transform.position;
            navManager.DestroySpheres();
            navManager.FindPath();
        }
        else
        {
            end.transform.position = start.transform.position;
            navManager.DestroySpheres();
            
        }
    }
    
    private void OnBenToggleOnClick(bool isOn)
    {
        if (isOn)
        {
            end.transform.position = Benedict.transform.position;
            navManager.DestroySpheres();
            navManager.FindPath();
        }
        else
        {
            end.transform.position = start.transform.position;
            navManager.DestroySpheres();
            
        }
    }

    public void changeDestToNic()
    {
        if (end.transform.position != Nicolas.transform.position)
        {
            end.transform.position = Nicolas.transform.position;
        }

        else
        {
            end.transform.position = start.transform.position;
        }
    }
    public void changeDestToMike()
    {
        if (end.transform.position != Michael.transform.position)
        {
            end.transform.position = Michael.transform.position;
        }
        else {
            end.transform.position = start.transform.position;
        }
    }
    public void changeDestToBen()
    {
        if (end.transform.position != Benedict.transform.position)
        {
            end.transform.position = Benedict.transform.position;
        }
        else {
            end.transform.position = start.transform.position;
        }
    }

}
