using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    public class MenuInteraction : MonoBehaviour
    {
        [Header("Boxes")]
        [SerializeField] private Transform _playBoxTransform;
        [SerializeField] private Transform _infoBoxTransform;
        [Space(10)] 
        [Header("Text")] 
        [SerializeField] private Transform _playText;
        [SerializeField] private Transform _infoText;
        [Space(10)] 
        [Header("Colors")] 
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _hoveredMaterial;

        public static event EventHandler<OnBoxHoverEventArgs> OnBoxHover;

        private bool _isSceneLoading;

        public class OnBoxHoverEventArgs : EventArgs
        {
            public Vector3 Position;
        }
        
        public void HoverPlay()
        {
            Hover(_playBoxTransform, _playText);
        }
        
        public void HoverInfo()
        {
            Hover(_infoBoxTransform, _infoText);
        }
        
        public void UnhoverPlay()
        {
            Unhover(_playBoxTransform, _playText);
        }
        
        public void UnhoverInfo()
        {
            Unhover(_infoBoxTransform, _infoText);
        }

        private void Hover(Transform box, Transform text)
        {
            OnBoxHover?.Invoke(this, new OnBoxHoverEventArgs { Position = transform.position });
            text.GetComponent<Renderer>().material = _hoveredMaterial;
        }

        private void Unhover(Transform box, Transform text)
        {
            text.GetComponent<Renderer>().material = _defaultMaterial;
        }

        public void Play()
        {
            if (_isSceneLoading) return;

            StartCoroutine(ChangeScene(SceneNames.CAR_SCENE_NAME));
            _isSceneLoading = true;
        }

        public void Info()
        {
            if (_isSceneLoading) return;

            //StartCoroutine(ChangeScene(SceneNames));
            _isSceneLoading = true;
        }

        private IEnumerator ChangeScene(string sceneName)
        {
            FadeScreen.Instance.Fade();
            yield return new WaitForSeconds(FadeScreen.Instance.FadeDuration+1);
            SceneManager.LoadScene(sceneName);
        }
    }
}
