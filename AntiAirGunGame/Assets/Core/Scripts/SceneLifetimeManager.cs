using UnityEngine;

namespace Core.Scripts
{
    public class SceneLifetimeManager : MonoBehaviour
    {
        [SerializeField] private SceneLifetime[] _lifetimes;
    
        private static int _lifetimeCounter;

        private void Start()
        {
            for (int i = 0; i < _lifetimes.Length; i++)
            {
                if (i == _lifetimeCounter)
                {
                    _lifetimes[i].gameObject.SetActive(true);
                    continue;
                }
                _lifetimes[i].gameObject.SetActive(false);
            }
            
            _lifetimeCounter++;
            
            if (_lifetimeCounter > _lifetimes.Length)
            {
                _lifetimeCounter = 0;
            }
        }
    }
}
