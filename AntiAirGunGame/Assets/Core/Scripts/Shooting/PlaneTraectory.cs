using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneTraectory : MovingObjectTraectory
{
    [Space(30), Header("Plane Settings")]
    [SerializeField, Range(0, 100)] private float _chanceToDiveOnPlayerOnAttacker;
    [Space(10)]
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _lineDuration;
    [Space(10)]
    [SerializeField, Range(0, 100)] private float _chanceToDiveOnLowHP;
    [SerializeField] private float _radiusOfDive;
    private bool _isDiving = false;
    private PlaneController _currentPlane;

    private void Start()
    {
        Vector3 lineDirection = (startParabolaTransform.position - _spawnTransform.position).normalized;
        if (lineDirection != Vector3.zero)
        {
            _currentPlane = Instantiate(movingObject, _spawnTransform.transform.position, Quaternion.LookRotation(lineDirection), transform).GetComponent<PlaneController>();
        }
        else
        {
            _currentPlane = Instantiate(movingObject, _spawnTransform.transform.position, Quaternion.identity, transform).GetComponent<PlaneController>();
        }

        _currentPlane.planeOnDestroyDelegate = () => { Destroy(gameObject); };

        if (_currentPlane.planeType == PlaneType.bomber)
        {
            _currentPlane.Invoke(nameof(_currentPlane.DropBombs), _lineDuration -  _lineDuration/4);
        }
        _currentPlane.transform.DOMove(startParabolaTransform.position, _lineDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (_currentPlane.planeType == PlaneType.attacker)
            {
                if(Random.Range(0, 100) < _chanceToDiveOnPlayerOnAttacker)
                {
                    endParabolaTransform.position = FindFirstObjectByType<BulletTraectory>().transform.position; //weird
                    CalculateParabola();
                }
                _currentPlane.Invoke(nameof(_currentPlane.DropBombs), parabolaDuration/2);
                MovingObjectOnParabola(_currentPlane.transform); // needs añceleration
                _isDiving = true;
            }
            if(_currentPlane.planeType == PlaneType.bomber)
            {
                _currentPlane.gameObject.SetActive(false);
            }
        });
    }

    private void Update()
    {
        if (_currentPlane != null && !_isDiving)
        {
            if (Random.Range(1, 100) < _chanceToDiveOnLowHP && _currentPlane.IsHealthBelowHalf)
            {
                _isDiving = true;
                float x = Random.Range(-_radiusOfDive, _radiusOfDive);
                float z = Random.Range(-_radiusOfDive, _radiusOfDive);
                Vector3 deltaPos = endParabolaTransform.position - startParabolaTransform.position;
                startParabolaTransform.position = _currentPlane.transform.position;
                endParabolaTransform.position = new Vector3(x + endParabolaTransform.position.x + deltaPos.x, endParabolaTransform.position.y, z + endParabolaTransform.position.z + +deltaPos.z);
                CalculateParabola();
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
