using System.Collections;
using UnityEngine;
using FutuRIFT;


namespace Core.Scripts
{
    public class FutuRiftCapsuleController : MonoBehaviour
    {
        [Header("Connection Parameters")]
        [SerializeField] private string _ipAddress = "127.0.0.1";
        [SerializeField] private int _port = 6065;
        [Space(3f)]
        [Header("Initial Settings")]
        [SerializeField] private float _initialPitch;
        [SerializeField] private float _initialRoll;
        [SerializeField] private float _maxPitchOffset;
        [SerializeField] private float _maxRollOffset;
        
        public static FutuRiftCapsuleController Instance { get; private set; }

        private const float MAX_ROLL_VALUE = 18F;
        private const float MIN_ROLL_VALUE = -18F;
        private const float MAX_PITCH_VALUE = 21F;
        private const float MIN_PITCH_VALUE = -15F;

        
        private FutuRIFTController _controller;
        
        
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
            _controller = new FutuRIFTController(new UdpSender(_ipAddress, _port));
        }

        private void Start()
        {
            _controller?.Start();
            if (_controller != null)
            {
                _controller.Pitch = _initialPitch;
                _controller.Roll = _initialRoll;
            }
        }
        
        private void OnDisable()
        {
            _controller?.Stop();
        }

        public void SetPitch(float pitch, bool allowMax = false)
        {
            if (allowMax)
            {
                _controller.Pitch = pitch;
                return;
            }

            pitch = pitch > (MAX_PITCH_VALUE - _maxPitchOffset) ? (MAX_PITCH_VALUE - _maxPitchOffset) : 
                pitch < (MIN_PITCH_VALUE + _maxPitchOffset) ? (MIN_PITCH_VALUE + _maxPitchOffset) : pitch;
            _controller.Pitch = pitch;

        }

        public void SetRoll(float roll, bool allowMax = false)
        {
            if (allowMax)
            {
                _controller.Roll = roll;
                return;
            }
            
            roll = roll > (MAX_ROLL_VALUE - _maxRollOffset) ? (MAX_ROLL_VALUE - _maxRollOffset) : 
                roll < (MIN_ROLL_VALUE + _maxRollOffset) ? (MIN_ROLL_VALUE + _maxRollOffset) : roll;
            _controller.Roll = roll;

        }

        public void SetPitchAndRoll(float pitch, float roll, bool allowMax = false)
        {
            if (allowMax)
            {
                _controller.Pitch = pitch;
                _controller.Roll = roll;
                return;
            }

            pitch = pitch > (MAX_PITCH_VALUE - _maxPitchOffset) ? (MAX_PITCH_VALUE - _maxPitchOffset) : 
                pitch < (MIN_PITCH_VALUE + _maxPitchOffset) ? (MIN_PITCH_VALUE + _maxPitchOffset) : pitch;
            roll = roll > (MAX_ROLL_VALUE - _maxRollOffset) ? (MAX_ROLL_VALUE - _maxRollOffset) : 
                roll < (MIN_ROLL_VALUE + _maxRollOffset) ? (MIN_ROLL_VALUE + _maxRollOffset) : roll;

            _controller.Pitch = pitch;
            _controller.Roll = roll;
        }

        public float GetPitch()
        {
            return _controller.Pitch;
        }
    }
}
