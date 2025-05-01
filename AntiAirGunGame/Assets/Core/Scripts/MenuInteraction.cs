using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    public class MenuInteraction : MonoBehaviour
    {
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
            Hover(_playText);
        }
        
        public void HoverInfo()
        {
            Hover(_infoText);
        }
        
        public void UnhoverPlay()
        {
            Unhover(_playText);
        }
        
        public void UnhoverInfo()
        {
            Unhover(_infoText);
        }

        private void Hover(Transform text)
        {
            OnBoxHover?.Invoke(this, new OnBoxHoverEventArgs { Position = transform.position });
            text.GetComponent<Renderer>().material = _hoveredMaterial;
        }

        private void Unhover(Transform text)
        {
            text.GetComponent<Renderer>().material = _defaultMaterial;
        }

        public void Play()
        {
            if (_isSceneLoading) return;

            SceneTransitionProvider.Instance.ChangeScene(SceneNames.CAR_SCENE_NAME);
            _isSceneLoading = true;
        }

        public void Info()
        {
            if (_isSceneLoading) return;

            SceneTransitionProvider.Instance.ChangeScene(SceneNames.INFO_SCENE_NAME);
            _isSceneLoading = true;
        }
    }
}
