using UnityEngine;

[ExecuteAlways]
public class BulletTraectory : MovingObjectTraectory
{
    [SerializeField] private Transform _gunTransform;

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.white;
        Gizmos.DrawLine(_startPoint, new Vector3(_endPoint.x, CalculateLineYPos(_startPoint, _gunTransform.position, _endPoint), _endPoint.z));

    }

    private float CalculateLineYPos(Vector3 startPoint, Vector3 gunPoint, Vector3 endPoint)
    {
        return ((endPoint.x - startPoint.x)/(gunPoint.x - startPoint.x))*(gunPoint.y - startPoint.y) + startPoint.y;
    }

}
