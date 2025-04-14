using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[ExecuteAlways]
public class MovingObjectTraectory : MonoBehaviour
{
    [Header("Trajectory Settings")]
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _endTransform;
    [SerializeField] private float _height = 2f;
    [SerializeField] private int _segments = 20;
    protected Vector3 _startPoint;
    protected Vector3 _endPoint;

    private void Update()
    {
        _startPoint = _startTransform.position;
        _endPoint = _endTransform.position;
    }

    protected void OnDrawGizmos()
    {
        if(_startTransform.position.y < _endTransform.position.y)
        {
            _endTransform.position = new Vector3(_endTransform.position.x, _startTransform.position.y, _endTransform.position.z);
            _startPoint = _startTransform.position;
            _endPoint = _endTransform.position;
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_startPoint, 0.1f);
        Gizmos.DrawSphere(_endPoint, 0.1f);

        Vector3 topPoint = CalculateParabolaPoint(0.5f, _startPoint, _endPoint, _height);
        Gizmos.DrawSphere(topPoint, 0.1f);

        Vector3 previousPoint = _startPoint;
        for (int i = 1; i <= _segments; i++)
        {
            float t = i / (float)_segments;
            Vector3 currentPoint = CalculateParabolaPoint(t, _startPoint, _endPoint, _height);
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }

    private Vector3 CalculateParabolaPoint(float t, Vector3 start, Vector3 end, float height)
    {
        float parabolaHeight = Mathf.Sin(t * Mathf.PI) * height;
        return Vector3.Lerp(start, end, t) + Vector3.up * parabolaHeight;
    }
}
