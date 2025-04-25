using System;
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
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _hoveredColor;

        public static event EventHandler<OnBoxHoverEventArgs> OnBoxHover;

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
            //todo lightning around box
            OnBoxHover?.Invoke(this, new OnBoxHoverEventArgs { Position = transform.position });
            text.GetComponent<Renderer>().material.SetColor("_BaseColor", _hoveredColor);
        }

        private void Unhover(Transform box, Transform text)
        {
            //todo lightning around box
            //todo sound
            text.GetComponent<Renderer>().material.SetColor("_BaseColor", _defaultColor);
        }

        public void Play()
        {
            //todo fade and sound
            SceneManager.LoadScene(SceneNames.CAR_SCENE_NAME);
        }

        public void Info()
        {
            
        }
    }
}
