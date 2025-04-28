using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    public class SceneLifetime : MonoBehaviour
    {
        [SerializeField] private float _sceneLifetime;
        [SerializeField] private Scenes _sceneToLoad;
        
        private float _timer;

        private void Update()
        {
            if (_timer < _sceneLifetime)
            {
                _timer += Time.deltaTime;
            }

            if (_timer >= _sceneLifetime)
            {
                GameManager.Instance.ChangeScene(SceneNames.StringSceneNames[_sceneToLoad]);
            }
        }
    }
}
