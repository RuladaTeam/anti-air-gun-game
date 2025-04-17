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

        public static FutuRiftCapsuleController Instance { get; private set; }
        
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

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                Debug.Log("Pizda prikol");
                KickBack();
            }
        }
        
        public void KickBack()
        {
            int pitch = 5;
            _controller.Pitch += pitch;
            //StartCoroutine(KickBackPitch(5));
        }

        private IEnumerator KickBackPitch(int pitch)
        {
            if (pitch < 0) yield break;
            
            Debug.Log("pizda pitch " + _controller.Pitch);
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
            }

            if (pitch is > -13 and < 19)
            {
                _controller.Pitch = pitch;
            }
        }

        public void SetRoll(float roll, bool allowMax = false)
        {
            if (allowMax)
            {
                _controller.Roll = roll;
                return;
            }

            if (roll is > -16 and < 16)
            {
                _controller.Roll = roll;
            }
        }

        public void SetPitchAndRoll(float pitch, float roll, bool allowMax = false)
        {
            if (allowMax)
            {
                _controller.Pitch = pitch;
                _controller.Roll = roll;
                return;
            }
            
            if (pitch is > -13 and < 19)
            {
                _controller.Pitch = pitch;
            }
            if (roll is > -16 and < 16)
            {
                _controller.Roll = roll;
            }
        }
    }
}

// 89572,32