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
using System.Linq;
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
    public TextMeshProUGUI textMesh;
    private float distance;
    private bool reach = false;
    public float distance_adjust;
    NavMeshHit hit;
    public List<Vector3> posPath;
    public LookAtConstraint currentLookAtConstraint;
    public LayerMask ballsLayerMask;
    public GameObject turn;
    public List<GameObject> turns;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        count = 0;
        findPathButton.onClick.AddListener(FindPath);
        nicCanvas.constraintActive = false;
        mikeCanvas.constraintActive = false;
        benCanvas.constraintActive = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (start.transform.position == end.transform.position) Reset();
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
            DestroySpheres();
        }

        if (reach && distance > distance_adjust) Reset();


        try
        {
            if (temps > spheres.Count) temps = spheres.Count;
            if (spheres.Count > 0 && temps < spheres.Count && temps > 0)
            {
                if (test < pathCount.Count-1 && spheres[temps - 1].activeSelf == false)
                {
                    s = temps;
                    temps += pathCount[test + 1];
                    for (int j = s; j < temps; j++)
                    {
                        spheres[j].SetActive(true);
                    }

                    test++;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Collider[] objs;
        objs = Physics.OverlapSphere(agent.transform.position + Vector3.up, 1, ballsLayerMask);
        foreach (Collider c in objs)
        {
            c.gameObject.SetActive(false);
            count++;
        }

<<<<<<< HEAD
=======
        if (spheres.Count > 0 && count <= spheres.Count - 1 && spheres[count].active == false)
        {
            spheres[count].active = true;
        }
        
>>>>>>> parent of 71e7d14 (s)
        foreach(GameObject t in turns.ToList())
        {
            if (Vector3.Distance(agent.transform.position, t.transform.position) < 3)
            {
                t.SetActive(true);
            }
            if (Vector3.Distance(agent.transform.position, t.transform.position) < 1.5f)
            {
                turns.Remove(t);
                Destroy(t);
            }
        }
    }

    private List<GameObject> spheres = new List<GameObject>();

    public void FindPath()
    {
        count = 0;
        List<Vector3> posCorners = new List<Vector3>();
        posPath = new List<Vector3>();
        Vector3 curPos, curUnitVector, vec;
        float dis, intervals;
        DestroySpheres();
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(hit.position, end.position, NavMesh.AllAreas, path);
        }

        foreach (Vector3 c in path.corners)
        {
            posCorners.Add(c);
        }

        intervals = 1;
        curPos = posCorners[0];
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            if (Vector3.Distance(curPos, posCorners[i + 1]) >= intervals)
            {
                posPath.Add(posCorners[i] + Vector3.up * 1.25f);
            }

            curPos = posCorners[i];
            vec = posCorners[i + 1] - posCorners[i];
            dis = Vector3.Distance(posCorners[i], posCorners[i + 1]);
            curUnitVector.x = vec.x / dis * intervals;
            curUnitVector.y = vec.y / dis * intervals;
            curUnitVector.z = vec.z / dis * intervals;
            while (Vector3.Distance(curPos, posCorners[i + 1]) >= intervals)
            {
                curPos = curPos + curUnitVector;
                posPath.Add(curPos + Vector3.up * 1.25f);
            }
        }
<<<<<<< HEAD
        counts[counts.Count - 1] -= 1;
        pathCount = retList(counts);
        counts.RemoveAt(count);
        s = 0;
        temps = pathCount[0];
        test = 0;

        // for (int i = 0; i < pathCount.Count; i++)
        // {
        //     Debug.Log(pathCount[i]);
        // }
        
        foreach (Vector3 p in posPath)
        {
            GameObject s = Instantiate(prefab, p, Quaternion.identity, canvas);
            s.SetActive(false);
            spheres.Add(s);
=======

        foreach (Vector3 p in posPath)
        {
            spheres.Add(Instantiate(prefab, p, Quaternion.identity, canvas));
>>>>>>> parent of 71e7d14 (s)
        }

        foreach (GameObject s in spheres)
        {
            s.SetActive(false);
        }
        spheres[0].SetActive(true);
        foreach (GameObject s in spheres)
        {
            s.SetActive(false);
        }

        int j = 0;
        GameObject t;
        Vector3 eulerAngles;
        foreach (Vector3 p in posCorners)
        {
            if (!(j == 0 || j == posCorners.Count - 1))
            {
                vec = posCorners[j] - posCorners[j - 1];
                dis = Vector3.Distance(posCorners[j - 1], posCorners[j]);
                curUnitVector.x = vec.x / dis * intervals;
                curUnitVector.y = vec.y / dis * intervals;
                curUnitVector.z = vec.z / dis * intervals;
                if (Vector3.Distance(posCorners[j - 1], posCorners[j]) > intervals)
                {
                    t = Instantiate(turn, p + Vector3.up * 1.25f, Quaternion.identity, canvas);
                    t.transform.position += curUnitVector * 0.8f;
                    t.SetActive(false);
                    turns.Add(t);
                }
            }

            j++;
        }

        if (turns.Count < 1) return;
        {
            for (int i = 0; i < turns.Count - 1; i++)
            {
                turns[i].transform.LookAt(turns[i + 1].transform);
                eulerAngles = turns[i].transform.rotation.eulerAngles;
                eulerAngles = new Vector3(0, eulerAngles.y + 90, 0);
                turns[i].transform.rotation = Quaternion.Euler(eulerAngles);
            }

            turns[turns.Count - 1].transform.LookAt(end.transform);
            eulerAngles = turns[turns.Count - 1].transform.rotation.eulerAngles;
            eulerAngles = new Vector3(0, eulerAngles.y + 90, 0);
            turns[turns.Count - 1].transform.rotation = Quaternion.Euler(eulerAngles);
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
