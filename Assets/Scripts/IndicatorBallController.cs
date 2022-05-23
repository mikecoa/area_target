using UnityEngine;
using UnityEngine.Animations;

public class IndicatorBallController : MonoBehaviour
{
    private LookAtConstraint _lookAtConstraint;
    private Camera _camera;

    private void Awake()
    {
        _lookAtConstraint = GetComponent<LookAtConstraint>();
        _camera = Camera.main;
    }

    private void Start()
    {
        _lookAtConstraint.SetSource(0, new ConstraintSource
        {
            sourceTransform = _camera.transform,
            weight = 1
        });
    }
}
