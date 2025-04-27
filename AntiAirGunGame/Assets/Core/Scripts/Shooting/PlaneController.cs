using DG.Tweening;
using UnityEngine;

public enum PlaneType
{
    bomber, attacker
}

[RequireComponent(typeof(MovingObjectTraectory))]
public class PlaneController : MonoBehaviour
{
    public delegate void PlaneOnDestroyDelegate();

    [Header("Plane settings")]
    [SerializeField] private PlaneType _planeType;
    [SerializeField, Min(1)] private int _health;
    [Space(30), Header("Particles settings")]
    [SerializeField] private ParticleSystem _littleSmoke;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _detonation;
    private ParticleSystem _currentParticleSystem = null;
    private BombsTrajectory _bombsTrajectory;

    public PlaneType planeType
    {
        get => _planeType;
        private set { }
    }
    public PlaneOnDestroyDelegate planeOnDestroyDelegate {set; private get; }
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
        _bombsTrajectory = GetComponent<BombsTrajectory>();
        _bombsTrajectory.SpawnBombs();
    }

    private void OnDestroy()
    {
        transform.DOKill();
        if(Health > 0)
        {
            Health = 0;
        }
        planeOnDestroyDelegate();
    }
}
