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
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using Image = UnityEngine.UIElements.Image;

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
    public Transform canvas;
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
    public List<Vector3> posPath;
    public LookAtConstraint currentLookAtConstraint;
    public LayerMask ballsLayerMask;
    //public List<Vector3> turnPos;
    public GameObject turn;
    public List<GameObject> turns;
    //public GameObject corner;

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
        
        /*if (NavMesh.SamplePosition(agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
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
        lineRenderer.SetPositions(path.corners);*/


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

        /*for (int i = 0; i < posPath.Count; i++)
        {
            if (Vector3.Distance(agent.transform.position, posPath[i]) < 1)
            {
                spheres[i].SetActive(false);
            }
        }*/

        /*foreach (Vector3 t in turnPos)
        {
            if (Vector3.Distance(t, agent.transform.position) < 0.5f)
            {
                turns.Add(Instantiate(turn, t + Vector3.back + Vector3.up * 0.5f, Quaternion.identity,canvas));
            }
            else
            {
                turns.Clear();
            }
        }*/
        Collider[] objs;
        objs = Physics.OverlapSphere(agent.transform.position + Vector3.up, 1, ballsLayerMask);
        foreach (Collider c in objs)
        {
            c.gameObject.SetActive(false);
        }
    }
    List<GameObject> spheres = new List<GameObject>();
    private List<GameObject> corners = new List<GameObject>();
    public void FindPath()
    {
        List<Vector3> posCorners = new List<Vector3>();
        posPath = new List<Vector3>();
        //turnPos = new List<Vector3>();
        Vector3 curPos, curUnitVector, vec;
        float dis, intervals;
        DestroySpheres();  
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(hit.position, end.position, NavMesh.AllAreas, path);
        }
        /*lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);*/
        
        foreach(Vector3 c in path.corners)
        {
             posCorners.Add(c);
             //corners.Add(Instantiate(corner, c+Vector3.up*1.25f,Quaternion.identity,canvas));
        }

        intervals = 1;
        curPos = posCorners[0];
        for (int i = 0; i < path.corners.Length-1 ; i++)
        {
            if (Vector3.Distance(curPos, posCorners[i + 1]) >= intervals)
            {
                posPath.Add(posCorners[i]+Vector3.up * 1.25f);
            }
            curPos = posCorners[i];
            vec = posCorners[i+1] - posCorners[i];
            dis = Vector3.Distance(posCorners[i], posCorners[i+1]);
            curUnitVector.x = vec.x / dis * intervals;
            curUnitVector.y = vec.y / dis * intervals;
            curUnitVector.z = vec.z / dis * intervals;
            while (Vector3.Distance(curPos, posCorners[i+1]) >= intervals)
            {
                curPos = curPos + curUnitVector;
                posPath.Add(curPos+Vector3.up * 1.25f);
            }
        }
        foreach (Vector3 p in posPath)
        {
            spheres.Add(Instantiate(prefab, p, Quaternion.identity,canvas));
        }

        int j = 0;
        GameObject t;
        Vector3 eulerAngles;
        //int dir; //-1 left, 1 right, 0 straight
        //Vector3 nextUnitVector;
        foreach (Vector3 p in posCorners)
        {
            /*dir = 0;

            if (j >= 1 && j <= posCorners.Count-2)
            {
                Debug.Log(FindTeta((posCorners[j] - posCorners[j - 1]) * -1, posCorners[j+1] - posCorners[j]));
            }
            else
            {
                Debug.Log("EMEM");
            }*/
            
            
            if (!(j == 0 || j == posCorners.Count-1))
            {
                vec = posCorners[j] - posCorners[j-1];
                dis = Vector3.Distance(posCorners[j-1], posCorners[j]);
                curUnitVector.x = vec.x / dis * intervals;
                curUnitVector.y = vec.y / dis * intervals;
                curUnitVector.z = vec.z / dis * intervals;
                if (Vector3.Distance(posCorners[j-1], posCorners[j]) > intervals)
                {
                    t = Instantiate(turn, p + Vector3.up * 1.25f, Quaternion.identity, canvas);
                    t.transform.position += curUnitVector * 0.8f;
                    turns.Add(t);
                }
            }
            j++;
        }

        for (int i = 0; i < turns.Count - 1; i++)
        {
            turns[i].transform.LookAt(turns[i+1].transform);
            eulerAngles = turns[i].transform.rotation.eulerAngles;
            eulerAngles = new Vector3(0, eulerAngles.y + 90, 0);
            turns[i].transform.rotation = Quaternion.Euler(eulerAngles);
        }
        turns[turns.Count-1].transform.LookAt(end.transform);
        eulerAngles = turns[turns.Count-1].transform.rotation.eulerAngles;
        eulerAngles = new Vector3(0, eulerAngles.y + 90, 0);
        turns[turns.Count-1].transform.rotation = Quaternion.Euler(eulerAngles);
    }

    /*public float FindTeta(Vector3 ba, Vector3 bc)
    {
        return Mathf.Acos(Vector3.Dot(ba, bc) /
                          ((Vector3.Distance(ba, Vector3.zero)) * Vector3.Distance(bc, Vector3.zero)));
    }*/
    
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
        foreach (GameObject g in turns)
        {
            Destroy(g);
        }
        spheres.Clear();
        turns.Clear();
        posPath.Clear();
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
