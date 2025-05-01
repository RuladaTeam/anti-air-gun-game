using UnityEngine;

namespace Core.Scripts
{
    public class InfoPlayer : MonoBehaviour
    {
        private void Update()
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && 
                OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
            {
                SceneTransitionProvider.Instance.ChangeScene(SceneNames.MAIN_MENU_SCENE_NAME);
            }
        }
    }
}
