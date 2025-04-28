using UnityEngine;

namespace Core.Scripts
{
    [CreateAssetMenu()]
    public class AudioRefsSO : ScriptableObject
    {
        public AudioClip BoxSound;
        public AudioClip[] CrowSound;
        public AudioClip[] MenuGunSound;
        public AudioClip[] AAGSound;
        public AudioClip[] HE111Sounds;
        public AudioClip[] JU87Sounds;
        public AudioClip[] TankSounds;
        public AudioClip[] Explosion;
    }
}
