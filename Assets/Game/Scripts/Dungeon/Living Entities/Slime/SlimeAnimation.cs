using UnityEngine;

namespace Game.Entities.LivingEntities.Enemies.Slime.Scripts
{
    [RequireComponent(typeof(SlimeAI))]
    [RequireComponent(typeof(Animator))]
    public class SlimeAnimation : MonoBehaviour
    {
        private SlimeAI slimeAI;
        private Animator animator;

        private float defaultAnimatorSpeed;

        private void Awake()
        {
            slimeAI = GetComponent<SlimeAI>();
            animator = GetComponent<Animator>();
            defaultAnimatorSpeed = animator.speed;
        }

        private void OnEnable()
        {
            slimeAI.StartedReloadingEvent += OnStartedReloading;
            slimeAI.ReadyToMakeImpulseEvent += OnReadyToMakeImpulse;
        }
        
        private void OnDisable()
        {
            slimeAI.StartedReloadingEvent -= OnStartedReloading;
            slimeAI.ReadyToMakeImpulseEvent -= OnReadyToMakeImpulse;
        }

        private void OnStartedReloading(object sender, SlimeAI.StartedReloadingEventArgs e)
        {
            animator.speed = 1 / e.ReloadTime;
            animator.SetTrigger("Reload");
        }
        
        private void OnReadyToMakeImpulse(object sender, SlimeAI.ReadyToMakeImpulseEventArgs e)
        {
            animator.speed = defaultAnimatorSpeed;
            animator.SetTrigger("Make Impulse");
        }
    }
}
