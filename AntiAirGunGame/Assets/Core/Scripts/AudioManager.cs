using System;
using UnityEngine;

namespace Core.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioRefsSO _audioRefsSO;
        
        private void Start()
        {
            MenuInteraction.OnBoxHover += MenuInteraction_OnBoxHover;
        }

        private void MenuInteraction_OnBoxHover(object sender, MenuInteraction.OnBoxHoverEventArgs e)
        {
            PlaySound(_audioRefsSO.BoxSound, e.Position);
        }

        public static void PlaySound(AudioClip clip, Vector3 position, float volume=1f)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}
