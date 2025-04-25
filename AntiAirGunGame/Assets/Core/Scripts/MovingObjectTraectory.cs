using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using System.Collections;


public abstract class MovingObjectTraectory : MonoBehaviour
{
    [Header("Trajectory Settings")]
    [SerializeField] private float _height = 2f;
    [SerializeField] protected GameObject movingObject;
    [SerializeField] protected Transform startParabolaTransform;
    [SerializeField] protected Transform endParabolaTransform;
    [Space(10)]
    [SerializeField, Min(0.1f)] private float _parabolaDuration;
    [SerializeField] protected int segments= 20;
    private Vector3[] _segmetPoints;

    public GameObject CurrentMovingObject { get; private set; }

    protected void MovingObjectOnParabola(Transform objTransform, PathType pathType = PathType.Linear)
    {
        objTransform.DOKill();
        Vector3 previousPosition = objTransform.position;
        objTransform.DOPath(_segmetPoints, _parabolaDuration, pathType, PathMode.Full3D, 10, Color.red).SetEase(Ease.Linear).OnUpdate(() =>
        {
            Vector3 movementDirection = (objTransform.position - previousPosition).normalized;

            if (movementDirection != Vector3.zero)
            {
                objTransform.rotation = Quaternion.LookRotation(movementDirection);
            }

            previousPosition = objTransform.position;
        }
        ).OnComplete(() =>
        {
            Destroy(objTransform.gameObject);

        });

    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startParabolaTransform.position, 0.1f);
        Gizmos.DrawSphere(endParabolaTransform.position, 0.5f);

        Vector3 topPoint = CalculateParabolaPoint(0.5f, startParabolaTransform.position, endParabolaTransform.position, _height);
        Gizmos.DrawSphere(topPoint, 0.5f);
        Vector3 previousPoint = startParabolaTransform.position;
        _segmetPoints = new Vector3[segments];
        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 currentPoint = CalculateParabolaPoint(t, startParabolaTransform.position, endParabolaTransform.position, _height);
            _segmetPoints[i-1] = currentPoint;
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }

    private Vector3 CalculateParabolaPoint(float t, Vector3 start, Vector3 end, float height)
    {
        float parabolaHeight = Mathf.Sin(t * Mathf.PI) * height;
        return Vector3.Lerp(start, end, t) + transform.up * parabolaHeight;
    }
}
