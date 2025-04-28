using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Core.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioRefsSO _audioRefsSO;
        
        private void Start()
        {
            MenuInteraction.OnBoxHover += MenuInteraction_OnBoxHover;
            SoundGenerator.OnSoundGenerated += SoundGenerated_OnSoundGenerated;
            BulletTraectory.OnAAGShoot += BulletTraectory_OnAAGShoot;
        }

        private void BulletTraectory_OnAAGShoot(object sender, EventArgs e)
        {
            PlaySound(_audioRefsSO.AAGSound, Vector3.zero);
        }

        private void SoundGenerated_OnSoundGenerated(object sender, SoundGenerator.OnCrowSoundEventArgs e)
        {
            AudioClip[] clips = _audioRefsSO.CrowSound;
            switch (e.SoundType)
            {
                case TypeOfSound.Crow:
                    clips = _audioRefsSO.CrowSound;
                    break;
                case TypeOfSound.People:
                    // clips = _audioRefsSO.People
                    break;
                case TypeOfSound.Gun:
                    clips = _audioRefsSO.MenuGunSound;
                    break;
            }
            
            PlaySound(clips, e.Position);
        }

        private void MenuInteraction_OnBoxHover(object sender, MenuInteraction.OnBoxHoverEventArgs e)
        {
            PlaySound(_audioRefsSO.BoxSound, e.Position, .1f);
        }

        private static void PlaySound(AudioClip clip, Vector3 position, float volume=1f)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
        private static void PlaySound(AudioClip[] clip, Vector3 position, float volume=1f)
        {
            AudioSource.PlayClipAtPoint(clip[Random.Range(0, clip.Length)], position, volume);
        }

        private void OnDisable()
        {
            MenuInteraction.OnBoxHover -= MenuInteraction_OnBoxHover;
            SoundGenerator.OnSoundGenerated -= SoundGenerated_OnSoundGenerated;           
            BulletTraectory.OnAAGShoot -= BulletTraectory_OnAAGShoot;
        }
    }
}
