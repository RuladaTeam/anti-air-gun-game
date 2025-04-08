using UnityEngine;

namespace FutuRIFT
{
    public class SimpleController : MonoBehaviour
    {
        [SerializeField] private string ipAddress = "127.0.0.1";
        [SerializeField] private int port = 6065;

        private FutuRIFTController _controller;

        private void Awake()
        {
            var sender = new UdpSender(ipAddress, port);
            _controller = new FutuRIFTController(sender);
        }

        private void Update()
        {
            var euler = transform.eulerAngles;
            _controller.Pitch = euler.x > 180 ? euler.x - 360 : euler.x;
            _controller.Roll = euler.z > 180 ? euler.z - 360 : euler.z;
        }

        private void OnEnable()
        {
            _controller?.Start();
        }

        private void OnDisable()
        {
            _controller?.Stop();
        }
    }
}