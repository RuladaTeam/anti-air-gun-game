using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

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
            SceneManager.LoadScene(sceneName);
        }
    }
}
