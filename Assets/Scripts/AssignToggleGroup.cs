using UnityEngine;
using UnityEngine.UI;

public class AssignToggleGroup : MonoBehaviour
{
    private ToggleGroup _toggleGroup;
    
    private void Awake()
    {
        //Use GetComponent to get this GameObject ToggleGroup
        _toggleGroup = GetComponent<ToggleGroup>();
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Toggle>().group = _toggleGroup;
        }
    }
}
