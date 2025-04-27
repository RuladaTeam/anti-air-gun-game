using DG.Tweening;
using System.Collections.Generic;
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
    [SerializeField] private GameObject _detonation;
    [SerializeField] private GameObject _hit;
    [SerializeField] private List<GameObject> debris;
    private int _startHealth;
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
            _health = value;
            if ((float)_health > (float)_startHealth / 2)
            {
                ChangeParticleSystem(_littleSmoke);
            }
            else if (_health <= 0)
            {
                Instantiate(_detonation, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
                foreach (GameObject item in debris)
                {
                    Instantiate(item, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0)).GetComponent<MovingObjectTraectory>().MovingObjectOnParabola();
                }
                Destroy(gameObject);
            }
            else
            {
                ChangeParticleSystem(_smoke);
                IsHealthBelowHalf = true;
            }

        }
    }

    private void Start()
    {
        _startHealth = Health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Health--;
            Instantiate(_hit, other.transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem>().Play();
            other.transform.DOKill();
            Destroy(other.gameObject);
            //shaking
        }
        else if (other.gameObject.GetComponent<PlaneController>())
        {
            Health = 0;
        }
    }

    private void ChangeParticleSystem(ParticleSystem nextParticleSystem)
    {
        if (_currentParticleSystem != nextParticleSystem)
        {
            _currentParticleSystem = nextParticleSystem;
            _currentParticleSystem.Play();
            //if detonation spawn pieces
        }
    }

    public void DropBombs()
    {
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
        if(planeOnDestroyDelegate != null)
        {
            planeOnDestroyDelegate();
        }
    }
}
