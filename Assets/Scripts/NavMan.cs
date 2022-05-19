using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class NavMan : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public GameObject Nicolas;
    public GameObject Michael;
    public GameObject Benedict;
    public Button nicToMike;
    public Button mikeToBen;
    public Button benToNic;
    public LookAtConstraint nicCanvas;
    public LookAtConstraint mikeCanvas;
    public LookAtConstraint benCanvas;
    public LineRenderer lineRenderer;
    public GameObject agent;
    public NavMeshPath path;
    public float PathYOffset;
    float elapsed;
    public TextMeshProUGUI textMesh;
    private float distance;
    private bool con = true;
    private bool reach = false;
    public float distance_adjust;
    NavMeshHit hit;

    public LookAtConstraint currentLookAtConstraint;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (start.transform.position == end.transform.position) Reset();
        else
        {
            Nicolas.SetActive(false);
            Michael.SetActive(false);
            Benedict.SetActive(false);
            con = true;
        }
        
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 0.1f)
        {
            elapsed -= 0.1f;
            if (NavMesh.SamplePosition(agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
            {
                NavMesh.CalculatePath(hit.position, end.position, NavMesh.AllAreas, path);
            }
            //NavMesh.CalculatePath(agent.transform.position, end.position, NavMesh.AllAreas, path);
            
        }
        if (con)
        {
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
        }
        
        
        distance = Vector3.Distance (agent.transform.position, end.transform.position);
        if (distance < distance_adjust)
        {
            if (Nicolas.transform.position == end.transform.position)
            {
                //Nicolas.SetActive(true);
                nicToMike.gameObject.SetActive(true);
                nicCanvas.constraintActive = true;
                currentLookAtConstraint = nicCanvas;
            }
            else if (Michael.transform.position == end.transform.position)
            {
                //Michael.SetActive(true);
                mikeToBen.gameObject.SetActive(true);
                mikeCanvas.constraintActive = true;
                currentLookAtConstraint = mikeCanvas;
            }
            else if (Benedict.transform.position == end.transform.position)
            {
                //Benedict.SetActive(true);
                benToNic.gameObject.SetActive(true);
                benCanvas.constraintActive = true;
                currentLookAtConstraint = benCanvas;
            }
            lineRenderer.positionCount = 0;
            reach = true;
        }

        if (reach && distance > distance_adjust)
        {
            //reach = false;
            Reset();
        }
    }

    public void Reset()
    {
        Nicolas.SetActive(false);
        Michael.SetActive(false);
        Benedict.SetActive(false);
        nicToMike.gameObject.SetActive(false);
        mikeToBen.gameObject.SetActive(false);
        benToNic.gameObject.SetActive(false);
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }

    public void OnAreaTargetChecked()
    {
        Debug.Log("Yes");
        textMesh.text = "Tracked";
    }

    public void OnAreaTargetLost()
    {
        Debug.Log("No");
        textMesh.text = "Lost Tracked";
    }
    
}
