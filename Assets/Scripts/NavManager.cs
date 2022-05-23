using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class NavManager : MonoBehaviour
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
    public GameObject prefab;
    public Button findPathButton;
    //float elapsed;
    public TextMeshProUGUI textMesh;
    private float distance;
    private bool reach = false;
    public float distance_adjust;
    NavMeshHit hit;

    public LookAtConstraint currentLookAtConstraint;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        findPathButton.onClick.AddListener(FindPath);
        //elapsed = 0.0f;
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }

    // Update is called once per frame
    public void Update()
    {
        //List<Vector3> corners = new List<Vector3>();
        if (start.transform.position == end.transform.position) Reset();
        
        // Update the way to the goal every second.
        /*elapsed += Time.deltaTime;
        if (elapsed > 0.1f)
        {
            elapsed -= 0.1f;
            if (NavMesh.SamplePosition(agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
            {
                NavMesh.CalculatePath(hit.position, end.position, NavMesh.AllAreas, path);
            }
        }*/
        
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(hit.position, end.position, NavMesh.AllAreas, path);
        }
        lineRenderer.positionCount = path.corners.Length;
        // lineRenderer.positionCount = 2;
        //
        // foreach(Vector3 c in path.corners)
        // {
        //     corners.Add(c + Vector3.up * 1);
        // }
        // lineRenderer.SetPositions(corners.ToArray());
        lineRenderer.SetPositions(path.corners);


        distance = Vector3.Distance (agent.transform.position, end.transform.position);
        if (distance < distance_adjust)
        {
            if (Nicolas.transform.position == end.transform.position)
            {
                nicToMike.gameObject.SetActive(true);
                nicCanvas.constraintActive = true;
                currentLookAtConstraint = nicCanvas;
            }
            else if (Michael.transform.position == end.transform.position)
            {
                mikeToBen.gameObject.SetActive(true);
                mikeCanvas.constraintActive = true;
                currentLookAtConstraint = mikeCanvas;
            }
            else if (Benedict.transform.position == end.transform.position)
            {
                benToNic.gameObject.SetActive(true);
                benCanvas.constraintActive = true;
                currentLookAtConstraint = benCanvas;
            }
            lineRenderer.positionCount = 0;
            reach = true;
        }

        if (reach && distance > distance_adjust) Reset();
    }
    List<GameObject> spheres = new List<GameObject>();
    public void FindPath()
    {
        List<Vector3> posCorners = new List<Vector3>();
        List<Vector3> posPath = new List<Vector3>();
        Vector3 curPos, curUnitVector, vec;
        float dis, intervals;
        DestroySpheres();
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(hit.position, end.position, NavMesh.AllAreas, path);
        }
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
        
        foreach(Vector3 c in path.corners)
        {
             posCorners.Add(c);
        }

        intervals = 1;
        for (int i = 0; i < path.corners.Length-1 || (i + 1) != path.corners.Length; i++)
        {
            curPos = posCorners[i];
            vec = posCorners[i+1] - posCorners[i];
            dis = Vector3.Distance(posCorners[i], posCorners[i+1]);
            curUnitVector.x = vec.x / dis * intervals;
            curUnitVector.y = vec.y / dis * intervals;
            curUnitVector.z = vec.z / dis * intervals;
            while (Vector3.Distance(curPos, posCorners[i+1]) >= intervals)
            {
                curPos = curPos + curUnitVector;
                posPath.Add(curPos);
            }
        }
        foreach (Vector3 p in posPath)
        {
            spheres.Add(Instantiate(prefab, p, Quaternion.identity));
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

    public void DestroySpheres()
    {
        foreach (GameObject g in spheres)
        {
            Destroy(g);
        }
    }
    public void OnAreaTargetChecked()
    {
        textMesh.text = "Tracked";
    }

    public void OnAreaTargetLost()
    {
        textMesh.text = "Lost Tracked";
    }
    
}
