using UnityEngine;

public class OrbitAroundParent : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private Transform _targetObject;
    [SerializeField] private float _rotationSpeed = 30f;
    [SerializeField] private float _radius = 0.5f;

    private Vector3 _direction;

    private void Start()
    {
        if (_targetObject != null)
        {
            SetDirection(_targetObject.position);
        }
    }

    private void Update()
    {
        if (_parentObject != null && _targetObject != null)
        {
            SetDirection(_targetObject.position);
            Vector3 offset = new Vector3(_direction.x, 0f, _direction.z) * _radius;
            Vector3 newPosition = _parentObject.position + offset;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            Vector3 lookDirection = (_targetObject.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    private void SetDirection(Vector3 targetPosition)
    {
        _direction = (targetPosition - _parentObject.position).normalized;
    }
}