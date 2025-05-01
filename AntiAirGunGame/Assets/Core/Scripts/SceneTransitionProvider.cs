using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    public class SceneTransitionProvider : MonoBehaviour
    {
        public static SceneTransitionProvider Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void ChangeScene(string sceneName)
        {
            StartCoroutine(IChangeScene(sceneName));
        }
        
        private IEnumerator IChangeScene(string sceneName)
        {
            FadeScreen.Instance.Fade();

            yield return new WaitForSeconds(FadeScreen.Instance.FadeDuration+1);
            MovingObjectTraectory[] movingObjects = FindObjectsByType<MovingObjectTraectory>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );
            foreach (var component in movingObjects)
            {
                component.gameObject.transform.DOKill();
                Destroy(component.gameObject);
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
