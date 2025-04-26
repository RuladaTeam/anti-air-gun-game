using System;
using System.Collections;
using UnityEngine;

namespace Core.Scripts
{
    public class InfoSceneMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _accelerationDuration;
        [SerializeField] private float _waitBeforeAcceleration;
        
        public static InfoSceneMovement Instance;

        
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
            StartCoroutine(Accelerate());
        }

        private IEnumerator Accelerate()
        {
            yield return new WaitForSeconds(_waitBeforeAcceleration);
            _accelerationDuration *= 100;
            for (int i = 0; i <= _accelerationDuration; i++)
            {
                _rigidbody.linearVelocity = Vector3.forward * (i/_accelerationDuration);
                FutuRiftCapsuleController.Instance?.SetPitch(FutuRiftCapsuleController.Instance.GetPitch() + .15f);
                yield return new WaitForSeconds(i/100f);
            }
            
            for (int i = 0; i <= _accelerationDuration; i++)
            {
                FutuRiftCapsuleController.Instance?.SetPitch(FutuRiftCapsuleController.Instance.GetPitch() - .1f);
                yield return new WaitForSeconds(i/100f);
            }
        }

        public void Stop()
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }
}
