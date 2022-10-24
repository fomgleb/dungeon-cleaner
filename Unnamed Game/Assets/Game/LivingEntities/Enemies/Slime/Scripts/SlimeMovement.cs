using UnityEngine;

namespace UnnamedGame.LivingEntities.Enemies.Slime.Scripts
{
    [RequireComponent(typeof(SlimeAI))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SlimeMovement : MonoBehaviour
    {
        private SlimeAI _slimeAI;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _slimeAI = GetComponent<SlimeAI>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _slimeAI.ReadyToMakeImpulseEvent += OnReadyToMakeImpulse;
        }

        private void OnDisable()
        {
            _slimeAI.ReadyToMakeImpulseEvent -= OnReadyToMakeImpulse;
        }

        private void OnReadyToMakeImpulse(object sender, SlimeAI.ReadyToMakeImpulseEventArgs e)
        {
            _rigidbody2D.AddForce(e.direction * e.force * 100, ForceMode2D.Impulse);
        }
    }
}