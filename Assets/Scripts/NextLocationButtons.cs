using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NextLocationButtons : MonoBehaviour
{
    public Button nicToMike;
    public Button mikeToBen;
    public Button benToNic;
    public GameObject nic;
    public GameObject mike;
    public GameObject ben;
    public GameObject start;
    public GameObject end;
    public CanvasGroup _canvasGroup;
    public ToggleGroup _toggleGroup;
    public Button showLocationButton;
    public Button hideLocationButton;
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
    }
    void changeToMike()
    {
        HideLocOnClick();
        end.transform.position = mike.transform.position;
    }
    void changeToBen()
    {
        HideLocOnClick();
        end.transform.position = ben.transform.position;
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
