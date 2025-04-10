using System;
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

        private FutuRIFTController _controller;
        
        private void Awake()
        {
            _controller = new FutuRIFTController(new UdpSender(_ipAddress, _port));
        }

        private void Start()
        {
            Debug.Log("pizda " + _controller);
            _controller.Start();
            _controller.Pitch = _initialPitch;
            _controller.Roll = _initialRoll;
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _controller.Pitch += 1;
                Debug.Log("pizda pitch + " + _controller.Pitch);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _controller.Pitch--;
                Debug.Log("pizda pitch + " + _controller.Pitch);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _controller.Roll--;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _controller.Roll++; 
            }
        }

        private void OnDisable()
        {
            if (_controller != null)
            {
                _controller.Stop();
            }
        }
    }
}
