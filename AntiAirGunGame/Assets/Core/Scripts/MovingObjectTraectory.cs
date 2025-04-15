using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

[ExecuteAlways]
public class MovingObjectTraectory : MonoBehaviour
{
    [Header("Trajectory Settings")]
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _endTransform;
    [SerializeField] private float _height = 2f;
    [Space(10)]
    [SerializeField] protected int _segments = 20;
    [Space(30)]
    [SerializeField, Min(0.1f)] private float _duration;
    protected Vector3 _startPoint;
    protected Vector3 _endPoint;
    //private Vector3[] _segmetPoints;

    //protected void MovingObj(Transform objTransform, PathType pathType = PathType.Linear)
    //{
    //    objTransform.DOPath(_segmetPoints, _duration, pathType).OnComplete(() =>
    //    {
    //        Destroy(objTransform.gameObject);
    //    });
    //}

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_startPoint, 0.5f);
        Gizmos.DrawSphere(_endPoint, 0.5f);

        Vector3 topPoint = CalculateParabolaPoint(0.5f, _startPoint, _endPoint, _height);
        Gizmos.DrawSphere(topPoint, 0.5f);

        Vector3 previousPoint = _startPoint;
        //_segmetPoints = new Vector3[_segments+1];
        for (int i = 1; i < _segments+1; i++)
        {
            float t = i / (float)_segments;
            Vector3 currentPoint = CalculateParabolaPoint(t, _startPoint, _endPoint, _height);
            //_segmetPoints[i] = currentPoint;
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
