using UnityEngine;

namespace Core.Scripts
{
    public class SkyBoxRotation : MonoBehaviour
    {

        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.3f);
        }
    }
}
