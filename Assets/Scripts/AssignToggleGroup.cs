using UnityEngine;
using UnityEngine.UI;

public class AssignToggleGroup : MonoBehaviour
{
    private void Awake()
    {
        //Use GetComponent to get this GameObject ToggleGroup
    }

    [SerializeField] private ToggleGroup _toggleGroup;
    
    private void Start()
    {
        foreach (Transform child in transform)
        {
            _toggleGroup.RegisterToggle(child.GetComponent<Toggle>());
        }
    }
}
