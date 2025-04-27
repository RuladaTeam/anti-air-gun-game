using UnityEngine;

namespace Core.Scripts
{
    public class SkyBoxRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationCoefficient;
        
        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotationCoefficient);
        }
    }
}
