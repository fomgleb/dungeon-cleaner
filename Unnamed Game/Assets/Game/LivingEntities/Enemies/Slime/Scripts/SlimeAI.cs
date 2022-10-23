using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnnamedGame.LivingEntities.Enemies.Scripts;
using UnnamedGame.LivingEntities.Player.Scripts;
using UnnamedGame.Pause;
using Zenject;
using Random = UnityEngine.Random;

namespace UnnamedGame.LivingEntities.Enemies.Slime.Scripts
{
    [RequireComponent(typeof(EnemyMeleeAttack))]
    public class SlimeAI : MonoBehaviour, IEnemyAI
    {
        [SerializeField] private float minReloadTime;
        [SerializeField] private float maxReloadTime;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float timeBetweenPlayerSearch;
        [SerializeField] private float rangeOfVision;
        [SerializeField] private LayerMask canSee;
        [SerializeField] private GameObject targetPrefab;

        public event EventHandler<ReadyToMakeImpulseEventArgs> ReadyToMakeImpulseEvent;
        public class ReadyToMakeImpulseEventArgs : EventArgs
        {
            public Vector2 direction;
            public float force;
        }

        private Collider2D targetCollider;
        private Collider2D TargetCollider
        {
            get
            {
                if (targetCollider != null) return targetCollider;
                
                var targetGameObject =  GameObject.FindWithTag(targetPrefab.tag);
                if (targetGameObject == null)
                    return null;
                targetCollider = targetGameObject.GetComponent<Collider2D>();

                return targetCollider;
            }
        }

        private Vector2 lastTargetPosition;

        [Inject] private Pauser pauser;

        private CancellationTokenSource cancelSearchingTargetToken;

        private void Awake()
        {
            cancelSearchingTargetToken = new CancellationTokenSource();
        }

        private void Start()
        {
            SearchTargetAsync(cancelSearchingTargetToken.Token);
            MakeImpulseAndReloadAsync(cancelSearchingTargetToken.Token);
        }

        private void OnDestroy()
        {
            cancelSearchingTargetToken.Cancel();
        }

        private async void SearchTargetAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested) return;

                if (pauser.IsPaused)
                {
                    await UniTask.Yield();
                    continue;
                }
                
                if (IsSeeTarget())
                {
                    if (TargetCollider == null) continue;
                    lastTargetPosition = TargetCollider.bounds.center;
                    await UniTask.Yield();
                }
                else
                    await UniTask.Delay((int)(timeBetweenPlayerSearch * 1000)); 
            }
        }
        
        private async void MakeImpulseAndReloadAsync(CancellationToken token)
        {
            while (true)
            {
                await UniTask.Delay((int)(Random.Range(minReloadTime, maxReloadTime) * 1000));
                
                if (pauser.IsPaused)
                    continue;
                
                if (token.IsCancellationRequested) return;
                
                Vector2 impulseDirection;
                if (lastTargetPosition != Vector2.zero)
                {
                    impulseDirection = (lastTargetPosition - (Vector2)transform.position).normalized;
                    lastTargetPosition = Vector2.zero;
                }
                else
                    impulseDirection = Random.insideUnitCircle.normalized;
                ReadyToMakeImpulseEvent?.Invoke(this, new ReadyToMakeImpulseEventArgs()
                {
                    direction = impulseDirection,
                    force = Random.Range(minForce, maxForce)
                });
            }
        }

        private void OnDrawGizmos()
        {
            if (targetCollider == null) return;
            var myPosition = transform.position;
            var directionToTarget = targetCollider.bounds.center - myPosition;
            Gizmos.DrawLine(myPosition, directionToTarget * rangeOfVision);
        }

        private readonly RaycastHit2D[] slimeSees = new RaycastHit2D[10];
        private bool IsSeeTarget()
        {
            if (TargetCollider == null) return false;
            var myPosition = transform.position;
            var directionToTarget = TargetCollider.bounds.center - myPosition;
            var size = Physics2D.RaycastNonAlloc(myPosition, directionToTarget, slimeSees, rangeOfVision, canSee);

            if (size <= 0)
                return false;

            if (slimeSees[0].collider.CompareTag(TargetCollider.tag))
                return true;
            else
                return false;
            
            // if (slimeSees.All(hit => hit.collider.CompareTag(TargetCollider.tag)))
            //     return true;
            // else
            //     return false;
        }
    }
}