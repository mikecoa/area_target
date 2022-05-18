using UnityEngine;

public class AssignToggleGroup : MonoBehaviour
{
    private void Awake()
    {
        //Use GetComponent to get this GameObject ToggleGroup
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            //Use GetComponent to get child GameObject toggle
        }
    }
}
