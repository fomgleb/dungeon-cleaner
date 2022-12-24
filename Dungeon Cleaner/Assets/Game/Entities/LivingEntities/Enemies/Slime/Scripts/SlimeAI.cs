using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Entities.LivingEntities.Enemies.Scripts;
using Game.Pause;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Entities.LivingEntities.Enemies.Slime.Scripts
{
    [RequireComponent(typeof(SpawnedObjectFinder))]
    public class SlimeAI : MonoBehaviour, IEnemyAI
    {
        [SerializeField] private float minReloadTime;
        [SerializeField] private float maxReloadTime;
        [SerializeField] private float timeBetweenTargetSearch;
        [SerializeField] private float timeBeforeMakeImpulse;
        [SerializeField] private float rangeOfVision;
        [SerializeField] private LayerMask canSee;

        #region Events
        public event EventHandler<ReadyToMakeImpulseEventArgs> ReadyToMakeImpulseEvent;
        public class ReadyToMakeImpulseEventArgs : EventArgs
        {
            public Vector2 Direction;
            public ReadyToMakeImpulseEventArgs(Vector2 direction)
            {
                Direction = direction;
            }
        }

        public event EventHandler<StartedReloadingEventArgs> StartedReloadingEvent; 
        public class StartedReloadingEventArgs : EventArgs
        {
            public readonly float ReloadTime;
            public StartedReloadingEventArgs(float reloadTime)
            {
                ReloadTime = reloadTime;
            }
        }
        #endregion

        private Vector2 lastTargetPosition;

        [Inject] private Pauser pauser;

        private CancellationTokenSource cancelSearchingTargetToken;

        private SpawnedObjectFinder targetFinder;

        private void Awake()
        {
            cancelSearchingTargetToken = new CancellationTokenSource();
            targetFinder = GetComponent<SpawnedObjectFinder>();
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
                    var targetCollider2D = targetFinder.LightweightGetComponent<Collider2D>();
                    
                    if (targetCollider2D == null) continue;
                    lastTargetPosition = targetCollider2D.bounds.center;
                    await UniTask.Yield();
                }
                else
                    await UniTask.Delay((int)(timeBetweenTargetSearch * 1000)); 
            }
        }
        
        private async void MakeImpulseAndReloadAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested) return;
                
                if (pauser.IsPaused)
                {
                    await UniTask.Yield();
                    continue;
                }
                
                var randomReloadTime = Random.Range(minReloadTime, maxReloadTime);
                
                StartedReloadingEvent?.Invoke(this, new StartedReloadingEventArgs(randomReloadTime));
                
                await UniTask.Delay(TimeSpan.FromSeconds(randomReloadTime));
                if (token.IsCancellationRequested) return;
                
                Vector2 impulseDirection;
                if (lastTargetPosition != Vector2.zero)
                {
                    impulseDirection = (lastTargetPosition - (Vector2)transform.position).normalized;
                    lastTargetPosition = Vector2.zero;
                }
                else
                    impulseDirection = Random.insideUnitCircle.normalized;

                await UniTask.Delay(TimeSpan.FromSeconds(timeBeforeMakeImpulse));
                if (token.IsCancellationRequested) return;

                ReadyToMakeImpulseEvent?.Invoke(this, new ReadyToMakeImpulseEventArgs(impulseDirection));
            }
        }

        private void OnDrawGizmos()
        {
            var targetCollider2D = targetFinder.LightweightGetComponent<Collider2D>();
            
            if (targetCollider2D == null) return;
            var myPosition = transform.position;
            var directionToTarget = (targetCollider2D.bounds.center - myPosition).normalized;
            Gizmos.DrawRay(myPosition, directionToTarget * rangeOfVision);
        }

        private readonly RaycastHit2D[] slimeSees = new RaycastHit2D[10];
        private bool IsSeeTarget()
        {
            var targetCollider2D = targetFinder.LightweightGetComponent<Collider2D>();
            
            if (targetCollider2D == null) return false;
            var myPosition = transform.position;
            var directionToTarget = targetCollider2D.bounds.center - myPosition;
            var size = Physics2D.RaycastNonAlloc(myPosition, directionToTarget, slimeSees, rangeOfVision, canSee);

            return size > 0 && slimeSees[0].collider.CompareTag(targetFinder.SpawnedGameObject.tag);
        }
    }
}