using DG.Tweening;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("Plane settings")]
    [SerializeField, Min(1)] private int _health;
    [Space(30), Header("Particles settings")]
    [SerializeField] private ParticleSystem _littleSmoke;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _detonation;

    private ParticleSystem _currentParticleSystem = null;

    public bool IsHealthBelowHalf { get; private set; } = false;
    private int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if ((float)value > (float)_health / 2)
            {
                //ChangeParticleSystem(_littleSmoke);
            }
            else if (value <= 0)
            {
                //ChangeParticleSystem(_detonation);
                Destroy(gameObject);
            }
            else
            {
                //ChangeParticleSystem(_smoke);
                IsHealthBelowHalf = true;
            }
            _health = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("damage " + Health);
            Health--;
            //shaking
        }
        else if (other.gameObject.GetComponent<PlaneController>())
        {
            Health = 0;
        }
    }

    private void ChangeParticleSystem(ParticleSystem nextParticleSystem)
    {
        if(_currentParticleSystem != nextParticleSystem)
        {
            _currentParticleSystem = nextParticleSystem;
            _currentParticleSystem.Play();
            //if detonation spawn pieces
        }
    }

    public void DropBombs()
    {
        //afterEndOfTrail
    }

    private void OnDestroy()
    {
        transform.DOKill();
        if(Health > 0)
        {
            Health = 0;
        }
    }
}
