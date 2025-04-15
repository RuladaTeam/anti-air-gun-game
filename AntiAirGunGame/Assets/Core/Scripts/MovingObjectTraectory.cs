using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;


public class MovingObjectTraectory : MonoBehaviour
{
    [Header("Trajectory Settings")]
    [SerializeField] protected Transform _startTransform;
    [SerializeField] protected Transform _endTransform;
    [SerializeField] private float _height = 2f;
    [Space(10)]
    [SerializeField] protected int _segments = 20;
    [Space(30)]
    [SerializeField, Min(0.1f)] private float _duration;
    private Vector3[] _segmetPoints;

    protected void MovingObj(Transform objTransform, PathType pathType = PathType.Linear)
    {
        objTransform.DOPath(_segmetPoints, _duration, pathType).OnComplete(() =>
        {
            Destroy(objTransform.gameObject);
        });
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_startTransform.position, 0.1f);
        Gizmos.DrawSphere(_endTransform.position, 0.5f);

        Vector3 topPoint = CalculateParabolaPoint(0.5f, _startTransform.position, _endTransform.position, _height);
        Gizmos.DrawSphere(topPoint, 0.5f);
        Vector3 previousPoint = _startTransform.position;
        _segmetPoints = new Vector3[_segments];
        for (int i = 1; i <= _segments; i++)
        {
            float t = i / (float)_segments;
            Vector3 currentPoint = CalculateParabolaPoint(t, _startTransform.position, _endTransform.position, _height);
            _segmetPoints[i-1] = currentPoint;
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
