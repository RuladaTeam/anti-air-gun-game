using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Core.Scripts
{
    public class SoundGenerator : MonoBehaviour
    {
        [SerializeField] private TypeOfSound _typeOfSound;
        [SerializeField] private float _noSoundAreaRadius;
        [SerializeField] private float _soundAreaRadius;
        [SerializeField] private float _minDelayTime;
        [SerializeField] private float _maxDelayTime;

        public static event EventHandler<OnCrowSoundEventArgs> OnSoundGenerated;

        public class OnCrowSoundEventArgs : EventArgs
        {
            public Vector3 Position;
            public TypeOfSound SoundType;
        }
        
        private float _timer;
        private float _delayValue;
        
        private void Update()
        {
            if (_timer <= 0f)
            {
                _delayValue = Random.Range(_minDelayTime, _maxDelayTime);
                _timer = _delayValue;
                return;
            }
            
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Vector3 position = GenerateRandomPosition();
                OnSoundGenerated?.Invoke(this, new OnCrowSoundEventArgs
                {
                    Position = position,
                    SoundType = _typeOfSound
                });
            }
        }

        private Vector3 GenerateRandomPosition()
        {
            float xPos = Random.Range(_noSoundAreaRadius, _soundAreaRadius) * (Random.Range(0, 2) == 1 ? 1 : -1);
            float yPos = Random.Range(_noSoundAreaRadius, _soundAreaRadius);
            float zPos = Random.Range(_noSoundAreaRadius, _soundAreaRadius) * (Random.Range(0, 2) == 1 ? 1 : -1);
            return new Vector3(xPos, yPos, zPos);
        }
    }

    public enum TypeOfSound
    {
        Crow, 
        Gun,
        People
    }
}
