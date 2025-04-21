using UnityEngine;


public class PlaneController : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField, Range(1, 100)] float _chanceToDiveOnLowHP;
    [SerializeField] private ParticleSystem _littleSmoke;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _detonation;
    private ParticleSystem _currentParticleSystem = null;

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
                ChangeParticleSystem(_littleSmoke); ;
            }
            else if (value <= 0)
            {
                ChangeParticleSystem(_detonation);
            }
            else
            {
                ChangeParticleSystem(_smoke);
                if (Random.Range(1, 100) < _chanceToDiveOnLowHP)
                {
                    //diveToRandomAngle
                }
            }
            _health = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            _health--;
        }
        else if (other.gameObject.GetComponent<PlaneController>())
        {
            _health = 0;
        }
    }

    private void ChangeParticleSystem(ParticleSystem nextParticleSystem)
    {
        if(_currentParticleSystem != nextParticleSystem)
        {
            _currentParticleSystem = nextParticleSystem;
            _currentParticleSystem.Play();
        }
    }

    private void DropBombs()
    {
        //afterEndOfTrail
    }
}
