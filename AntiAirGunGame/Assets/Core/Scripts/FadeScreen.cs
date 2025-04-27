using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Scripts
{
    public class FadeScreen : MonoBehaviour
    {
        [SerializeField] private Material _fadeMaterial;
        [SerializeField] public float FadeDuration;
        
        public static FadeScreen Instance { get; private set; }

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

        private void Start()
        {
            _fadeMaterial.SetFloat("_Alpha", 0f);
        }
        

        public void Fade()
        {
            StartCoroutine(IFade());
        }

        private IEnumerator IFade()
        {
            float fadeDuration = FadeDuration * 100f;
            for (int i = 0; i <= fadeDuration; i++)
            {
                _fadeMaterial.SetFloat("_Alpha", i / fadeDuration);
                yield return new WaitForSeconds(.01f);
            }
        }
    }
}
