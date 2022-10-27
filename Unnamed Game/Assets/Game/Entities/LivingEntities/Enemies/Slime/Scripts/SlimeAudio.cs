using UnityEngine;

namespace Game.LivingEntities.Enemies.Slime.Scripts
{
    [RequireComponent(typeof(SlimeMovement))]
    public class SlimeAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip[] hitTheWallSounds;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private AudioSource hitWallAudioSource;

        private SlimeMovement slimeMovement;

        private void Awake()
        {
            slimeMovement = GetComponent<SlimeMovement>();
        }

        private void OnEnable()
        {
            slimeMovement.TouchedOtherColliderEvent += OnTouchedOtherCollider;
        }

        private void OnDisable()
        {
            slimeMovement.TouchedOtherColliderEvent -= OnTouchedOtherCollider;
        }

        private void OnTouchedOtherCollider(Collision2D collision) 
        {
            var otherColliderLayer = 1 << collision.collider.gameObject.layer;
            if (otherColliderLayer == wallLayer)
            {
                hitWallAudioSource.pitch = Random.Range(0.9f, 1.1f);
                hitWallAudioSource.PlayOneShot(hitTheWallSounds[Random.Range(0, hitTheWallSounds.Length)]);
            }
        }
    }
}