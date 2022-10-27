using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnnamedGame.LivingEntities.Enemies.Scripts;
using UnnamedGame.Pause;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.LivingEntities.Enemies.Slime.Scripts
{
    public class SlimeAI : MonoBehaviour, IEnemyAI
    {
        [SerializeField] private float minReloadTime;
        [SerializeField] private float maxReloadTime;
        [SerializeField] private float timeBetweenTargetSearch;
        [SerializeField] private float timeBeforeMakeImpulse;
        [SerializeField] private float rangeOfVision;
        [SerializeField] private LayerMask canSee;
        [SerializeField] private GameObject targetPrefab;

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

        private GameObject targetGameObject;
        public GameObject TargetGameObject
        {
            get
            {
                if (targetGameObject != null) return targetGameObject;
                targetGameObject = GameObject.FindWithTag(targetPrefab.tag);
                return targetGameObject;
            }
        }
        
        private Collider2D targetCollider;
        public Collider2D TargetCollider
        {
            get
            {
                if (targetCollider != null) return targetCollider;
                if (TargetGameObject == null) return null;
                
                targetCollider = TargetGameObject.GetComponent<Collider2D>();
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