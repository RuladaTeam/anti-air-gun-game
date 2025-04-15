using UnityEngine;
using DG.Tweening;

[ExecuteAlways]
public class BulletTraectory : MovingObjectTraectory
{
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private GameObject _bullet;
    [SerializeField, Min(0.1f)] private float _ofsetTime;
    private bool _isShooting;
    private float _currentOfsetTime;

    private void Update()
    {
        //Shoot();
    }

    private void OnDrawGizmos()
    {
        //base.OnDrawGizmos();
        Gizmos.color = Color.white;
        Gizmos.DrawLine(_startPoint, new Vector3(_endPoint.x, CalculateLineYPos(_startPoint, _gunTransform.position, _endPoint), _endPoint.z));

    }

    private float CalculateLineYPos(Vector3 startPoint, Vector3 gunPoint, Vector3 endPoint)
    {
        return ((endPoint.x - startPoint.x)/(gunPoint.x - startPoint.x))*(gunPoint.y - startPoint.y) + startPoint.y;
    }

    //private void Shoot()
    //{
    //    if (!_isShooting)
    //    {
    //        GameObject currentBullet = Instantiate(_bullet, _gunTransform);
    //        MovingObj(currentBullet.transform);
    //        _isShooting = true;
    //        _currentOfsetTime = _ofsetTime;
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if (_currentOfsetTime>0)
    //    {
    //        _currentOfsetTime -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        _isShooting = false;
    //    }
    //}
}
