using UnityEngine;

namespace Core.Scripts
{
    public class GunRotation : MonoBehaviour
    {
        [Header("Rotation")]
        [SerializeField] private Transform _rotationWheelTransform;
        [SerializeField] private Transform _rotatableTransform;
        
        [Header("Pitch")]
        [SerializeField] private Transform _pitchWheelTransform;
        [SerializeField] private Transform[] _pitchableTransforms;
        [SerializeField] private float _pitchOnKickBack;
        [SerializeField] private float _kickbackDelay;
            
        [Header("Coefficients")]
        [SerializeField] private int _rotationRatio = 20;
        [SerializeField] private float _pitchRatio = 20;
        [SerializeField] private float _rotationRollCoefficient = .33f;
        [SerializeField] private float _rotationPitchCoefficient;
        [SerializeField] private float _pitchCoefficient = 0.02f;
        
        [Header("BarrelKickback")]
        [SerializeField] private Transform _barrelTransform;
        [SerializeField] private float _barrelKickbackStrength;
        
        [Space(30)] 
        [SerializeField] private BulletTraectory _bulletTrajectory;
        
        private Vector3 _previousEulerAngles;
        private Vector3 _totalRotation;
        
        private Vector3 _totalPitch;
        private Vector3 _previousPitch;

        private float _currentRotationWheelValue;
        private float _currentPitchWheelValue;
        
        private float _totalCapsulePitchValue;
        private float _totalCapsuleRollValue;
        private float _kickbackValue;
        private float _kickbackTimer;
		private float _pitchWithoutKickback;
        private float _initGunRotation;

        private Vector3 _barrelDefaultPosition;
        
        private void Start()
        {
            _initGunRotation = _rotatableTransform.rotation.eulerAngles.y;
            _previousEulerAngles = _rotationWheelTransform.localEulerAngles;
            _totalRotation = _previousEulerAngles;
            
            _previousPitch = _pitchWheelTransform.localEulerAngles;
            _totalPitch = _previousPitch;
        }
        
        private void Update()
        {
            _currentPitchWheelValue = 0;
            _currentRotationWheelValue = 0;
            
            HandlePitch();
            HandleRotation();
            HandleKickback(out bool allowMax);

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Kickback();
            }

            FutuRiftCapsuleController.Instance?.SetPitchAndRoll(
                -_currentPitchWheelValue + _kickbackValue, _currentRotationWheelValue, allowMax);
        }

        private void HandleRotation()
        {
            Vector3 currentEulerAngles = _rotationWheelTransform.localEulerAngles;

            Vector3 deltaRotation = new Vector3(
                DeltaAngle(_previousEulerAngles.x, currentEulerAngles.x),
                DeltaAngle(_previousEulerAngles.y, currentEulerAngles.y),
                DeltaAngle(_previousEulerAngles.z, currentEulerAngles.z)
            );

            _totalRotation += deltaRotation;
            _previousEulerAngles = currentEulerAngles;

            float pitchDirectionCoefficient = deltaRotation.y >= 0 ? -1 : 1;
            _rotatableTransform.eulerAngles = new Vector3(
                _rotatableTransform.eulerAngles.x, (_totalRotation.y + _initGunRotation)/_rotationRatio,
                _rotatableTransform.eulerAngles.z);

            _currentRotationWheelValue += -deltaRotation.y * _rotationRollCoefficient;
            _currentPitchWheelValue += -deltaRotation.y * _rotationPitchCoefficient * pitchDirectionCoefficient;
        }

        private void HandlePitch()
        {
            Vector3 currentPitch = _pitchWheelTransform.localEulerAngles;

            Vector3 deltaPitch = new Vector3(
                DeltaAngle(_previousPitch.x, currentPitch.x),
                DeltaAngle(_previousPitch.y, currentPitch.y),
                DeltaAngle(_previousPitch.z, currentPitch.z)
            );

            if (_totalPitch.z / _pitchRatio <= -32 && deltaPitch.z < 0)
            {
                deltaPitch = new Vector3(0, 0, 0);
            }
            if (_totalPitch.z/_pitchRatio >= 29 && deltaPitch.z > 0)
            {
                deltaPitch = new Vector3(0, 0, 0);
            }

            _totalPitch += deltaPitch;
            _previousPitch = currentPitch;

            foreach (var pitchable in _pitchableTransforms)
            {
                pitchable.localEulerAngles = new Vector3(
                    pitchable.localEulerAngles.x, pitchable.localEulerAngles.y, _totalPitch.z/_pitchRatio);
            }
            
            _currentPitchWheelValue += _totalPitch.z * _pitchCoefficient / 10;
        }

        private void HandleKickback(out bool allowMax)
        {
            allowMax = false;
            float barrelTimeToKickback = _kickbackDelay * 0.3f;
            Vector3 barrelFinalPosition = new Vector3(_barrelDefaultPosition.x - _barrelKickbackStrength,
                _barrelDefaultPosition.y, _barrelDefaultPosition.z);
            if (_kickbackTimer > 0f)
            {
                if (_kickbackDelay - _kickbackTimer <= barrelTimeToKickback)
                {
                    _barrelTransform.localPosition = Vector3.Lerp(
                        _barrelDefaultPosition, barrelFinalPosition, (_kickbackDelay - _kickbackTimer)/barrelTimeToKickback);
                }
                else
                {
                    _barrelTransform.localPosition = Vector3.Lerp(
                        barrelFinalPosition, _barrelDefaultPosition, (_kickbackDelay - barrelTimeToKickback - _kickbackTimer)/(_kickbackDelay-barrelTimeToKickback));
                    _kickbackValue = _pitchOnKickBack - (_kickbackDelay -_kickbackTimer) * 
                        (_pitchOnKickBack - _pitchWithoutKickback)/_kickbackDelay;  
                }
                allowMax = true;
                _kickbackTimer -= Time.deltaTime;
                _currentPitchWheelValue = 0f;   
            }
            else
            {
                _kickbackValue = 0;
            }
        }
        

        public void Kickback()
        {
            _kickbackTimer = _kickbackDelay;
            _kickbackValue = _pitchOnKickBack;
            _pitchWithoutKickback = FutuRiftCapsuleController.Instance.GetPitch();
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
