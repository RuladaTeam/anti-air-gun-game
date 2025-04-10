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
        [SerializeField] private float _initialPitch = 0f;
        [SerializeField] private float _initialRoll = 0f;

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
            _controller.Start();
            _controller.Pitch = _initialPitch;
            _controller.Roll = _initialRoll;
            
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.UpArrow))
            // {
            //     _controller.Pitch += 1;
            // }
            //
            // if (Input.GetKeyDown(KeyCode.DownArrow))
            // {
            //     _controller.Pitch--;
            // }
            //
            // if (Input.GetKey(KeyCode.LeftArrow))
            // {
            //     _controller.Roll--;
            //     _controller.Pitch--;
            // }
            //
            // if (Input.GetKey(KeyCode.RightArrow))
            // {
            //     _controller.Roll++;
            //     _controller.Pitch--;
            // }
            // else
            // {
            //     _controller.Pitch = _initialPitch;
            //     _controller.Roll = _initialRoll;
            // }
        }

        private void OnDisable()
        {
            if (_controller != null)
            {
                _controller.Stop();
            }
        }

        public void SetPitch(float pitch)
        {
            _controller.Pitch = pitch;
        }

        public void SetRoll(float roll)
        {
            _controller.Roll = roll;
        }

        public void SetPitchAndRoll(float pitch, float roll)
        {
            _controller.Pitch = pitch;
            _controller.Roll = roll;
        }
    }
}
