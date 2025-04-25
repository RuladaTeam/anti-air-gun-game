using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlaneTraectory : MovingObjectTraectory
{
    private enum PlaneType
    {
        bomber, attacker
    }

    [Space(30), Header("Plane Settings")]
    [SerializeField] private PlaneType _planeType;
    [Space(10)]
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _lineDuration;
    [Space(10)]
    [SerializeField, Range(1, 100)] private float _chanceToDiveOnLowHP;
    [SerializeField] private float _radiusOfDive;
    private GameObject _currentPlane;

    private void Start()
    {
        //if (_planeType == PlaneType.attacker)
        //{
        //    endParabolaTransform.
        //}
        Vector3 lineDirection = (startParabolaTransform.position - _spawnTransform.position).normalized;
        if (lineDirection != Vector3.zero)
        {
            _currentPlane = Instantiate(movingObject, _spawnTransform.transform.position, Quaternion.LookRotation(lineDirection), transform);
        }
        else
        {
            _currentPlane = Instantiate(movingObject, _spawnTransform.transform.position, Quaternion.identity, transform);
        }



        _currentPlane.transform.DOMove(startParabolaTransform.position, _lineDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (_planeType == PlaneType.attacker)
            {
                MovingObjectOnParabola(_currentPlane.transform); // needs añceleration
            }
        });
    }

    private void Update()
    {
        if (_currentPlane != null)
        {
            if (Random.Range(0, 100) < _chanceToDiveOnLowHP && _currentPlane.GetComponent<PlaneController>().IsHealthBelowHalf)
            {
                float x = Random.Range(-_radiusOfDive, _radiusOfDive);
                float y = Random.Range(-_radiusOfDive, _radiusOfDive);
                endParabolaTransform.position = new Vector3(x, y, endParabolaTransform.position.z);
                MovingObjectOnParabola(_currentPlane.transform);
            }
        }
    }

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_spawnTransform.position, 0.5f);
        Gizmos.DrawLine(_spawnTransform.position, startParabolaTransform.position);
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(endParabolaTransform.position, _radiusOfDive);
    }
}
