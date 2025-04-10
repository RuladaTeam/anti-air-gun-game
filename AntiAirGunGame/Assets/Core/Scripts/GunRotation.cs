using UnityEngine;

namespace Core.Scripts
{
    public class GunRotation : MonoBehaviour
    {
        [SerializeField] private Transform _rotationWheelTransform;
        [SerializeField] private Transform _rotatableTransform;
        [SerializeField] private int _rotationRatio = 20;
        [SerializeField] private float _rotationRollCoefficient = .33f;
        [SerializeField] private float _rotationPitchCoefficient = 0;
        
        private Vector3 _previousEulerAngles;
        private Vector3 _totalRotation;

        private void Start()
        {
            _previousEulerAngles = _rotationWheelTransform.localEulerAngles;
            _totalRotation = _previousEulerAngles;
        }
        
        private void Update()
        {
            Vector3 currentEulerAngles = _rotationWheelTransform.localEulerAngles;

            Vector3 deltaRotation = new Vector3(
                DeltaAngle(_previousEulerAngles.x, currentEulerAngles.x),
                DeltaAngle(_previousEulerAngles.y, currentEulerAngles.y),
                DeltaAngle(_previousEulerAngles.z, currentEulerAngles.z)
            );

            _totalRotation += deltaRotation;
            _previousEulerAngles = currentEulerAngles;
    
            _rotatableTransform.eulerAngles = new Vector3(
                _totalRotation.x, _totalRotation.y/_rotationRatio, _totalRotation.z);
            
            FutuRiftCapsuleController.Instance?.SetPitchAndRoll(
                -deltaRotation.y * _rotationPitchCoefficient, -deltaRotation.y * _rotationRollCoefficient);
        }

        private float DeltaAngle(float previousAngle, float currentAngle)
        {
            float delta = currentAngle - previousAngle;

            if (delta > 180)
                delta -= 360;
            else if (delta < -180)
                delta += 360;

            return delta;
        }
    }
}
