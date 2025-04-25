using UnityEngine;
using DG.Tweening;
using System.Collections;
using Core.Scripts;


public class BulletTraectory : MovingObjectTraectory
{
    [SerializeField] private Transform _gunTransform;
    [SerializeField, Min(0.1f)] private float _ofsetTime;

    [SerializeField] private GunRotation _gunrotation;
    [SerializeField] private float _lifeTime;
    private bool _isShooting;
    private float _currentOfsetTime;

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.white;
        Gizmos.DrawLine(startParabolaTransform.position, new Vector3(endParabolaTransform.position.x, CalculateLineYPos(startParabolaTransform.position, _gunTransform.position, endParabolaTransform.position), endParabolaTransform.position.z));

    }
    private void Start()
    {
        _isShooting = true;
        _currentOfsetTime = _ofsetTime;
    }

    private void Update()
    {

        if (_currentOfsetTime > 0)
        {
            _currentOfsetTime -= Time.deltaTime;
        }
        else
        {
            _isShooting = false;
        }
        
        if (OVRInput.Get(OVRInput.Button.One))
        {
            Shoot();
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
            GameObject currentBullet = Instantiate(movingObject, startParabolaTransform.position, _gunTransform.rotation);
            MovingObjectOnParabola(currentBullet.transform);
            StartCoroutine(DestroingBullet(currentBullet));
            _isShooting = true;
            _currentOfsetTime = _ofsetTime;
            _gunrotation.Kickback();
        }
    }

    private IEnumerator DestroingBullet(GameObject currentBullet)
    {
        yield return new WaitForSeconds(_lifeTime);
        if(currentBullet != null)
            currentBullet.transform.DOKill();
        Destroy(currentBullet);
    }
}
