using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts
{
    public class UIElementTrigger : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private bool _stoppable;
        [SerializeField] private float _appearDuration;
        
        private void Start()
        {
            if (_stoppable) return;
            Color color = _image.color;
            color.a = 0f;
            _image.color = color;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (_stoppable)
            {
                InfoSceneMovement.Instance.Stop();
                return;
            }
            StartCoroutine(Appear());
        }

        private IEnumerator Appear()
        {
            _appearDuration *= 100;
            for (int i = 0; i <= _appearDuration; i++)
            {
                Color color = _image.color;
                color.a += i / 100f;
                _image.color = color;
                yield return new WaitForSeconds(i/100f);
            }
        }
    }
}
