using UnityEngine;
using DG.Tweening;


public class BulletTraectory : MovingObjectTraectory
{
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private GameObject _bullet;
    [SerializeField, Min(0.1f)] private float _ofsetTime;
    private bool _isShooting;
    private float _currentOfsetTime;

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.white;
        Gizmos.DrawLine(_startTransform.position, new Vector3(_endTransform.position.x, CalculateLineYPos(_startTransform.position, _gunTransform.position, _endTransform.position), _endTransform.position.z));

    }
    private void Start()
    {
        _isShooting = true;
        _currentOfsetTime = _ofsetTime;
    }

    private void Update()
    {

        Shoot();

        if (_currentOfsetTime > 0)
        {
            _currentOfsetTime -= Time.deltaTime;
        }
        else
        {
            _isShooting = false;
        }
    }

    private float CalculateLineYPos(Vector3 startPoint, Vector3 gunPoint, Vector3 endPoint)
    {
        return ((endPoint.x - startPoint.x) / (gunPoint.x - startPoint.x)) * (gunPoint.y - startPoint.y) + startPoint.y;
    }

    private void Shoot()
    {
        if (!_isShooting)
        {
            Debug.Log("1");
            GameObject currentBullet = Instantiate(_bullet, _startTransform.position, _gunTransform.rotation);
            MovingObj(currentBullet.transform);
            _isShooting = true;
            _currentOfsetTime = _ofsetTime;
        }
    }


}
