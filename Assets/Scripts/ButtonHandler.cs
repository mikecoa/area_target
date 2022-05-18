using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class ButtonHandler : MonoBehaviour
{
    public GameObject end;
    public GameObject start;
    public GameObject Nicolas;
    public GameObject Michael;
    public GameObject Benedict;

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
