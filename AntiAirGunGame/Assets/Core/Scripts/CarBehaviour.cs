using System.Collections;
using Meta.Net.NativeWebSocket;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Scripts
{
    public class CarBehaviour : MonoBehaviour
    {
        [SerializeField] private float _pitchRangeMin;
        [SerializeField] private float _pitchRangeMax;
        [SerializeField] private float _rollRangeMin;
        [SerializeField] private float _rollRangeMax;
        [Space(30)] 
        [SerializeField] private float _chanceToAccelerate;
        
        private float _currentPitch;
        private float _currentRoll;
        private float _accelerationTimer;
        private bool _flag;
        private float _accelerationValue;
        
        private void Update()
        {
            if (!FutuRiftCapsuleController.Instance) return;
            
            float pitchRange = Random.Range(_pitchRangeMin, _pitchRangeMax);
            float rollRange = Random.Range(_rollRangeMin, _rollRangeMax);
            
            if (Random.Range(0f, 1f) <= .4f)
            {
                _currentPitch = Random.Range(-pitchRange, pitchRange);
                _currentRoll = Random.Range(-rollRange, rollRange);
                _currentRoll += _flag ? -1 * _accelerationValue : _accelerationValue;
            }
            else
            {
                
                _currentPitch = 0;
                _currentRoll = 0;
            }
            
            if (Random.Range(0f, 1f) <= _chanceToAccelerate && _accelerationTimer <= 0f)
            {
                StartCoroutine(Acceleration());
            }
            
            FutuRiftCapsuleController.Instance.SetPitchAndRoll(_currentPitch, _currentRoll);
        }

        private IEnumerator Acceleration()
        {
            bool isFaster = Random.Range(0, 1) == 1;
            _accelerationTimer = Random.Range(1f, 3f);

            while (_accelerationTimer > 0f)
            {
                _accelerationTimer -= Time.deltaTime;
                _accelerationValue += .1f  * (isFaster ? .3f : -.3f);
                yield return new WaitForNextFrameUnit();
            }

            _accelerationValue = 0;
            _flag = !_flag;
        }
    }
}
