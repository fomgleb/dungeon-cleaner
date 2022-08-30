using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnnamedGame.LivingEntities.Enemies.Scripts;
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

        public event EventHandler<ReadyToMakeImpulseEventArgs> ReadyToMakeImpulseEvent;
        public class ReadyToMakeImpulseEventArgs : EventArgs
        {
            public Vector2 direction;
            public float force;
        }

        private Vector2 _lastTargetPosition;
        private Collider2D _targetCollider;

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            _targetCollider = GetComponent<EnemyMeleeAttack>().Player.GetComponent<Collider2D>();
            SearchPlayerAsync(_cancellationTokenSource.Token);
            MakeImpulseAndReloadAsync(_cancellationTokenSource.Token);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }

        private async void SearchPlayerAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested) return;
                if (IsSeeTarget())
                {
                    _lastTargetPosition = _targetCollider.bounds.center;
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
                if (token.IsCancellationRequested) return;
                Vector2 impulseDirection;
                if (_lastTargetPosition != Vector2.zero)
                {
                    impulseDirection = (_lastTargetPosition - (Vector2)transform.position).normalized;
                    _lastTargetPosition = Vector2.zero;
                }
                else
                    impulseDirection = Random.insideUnitCircle.normalized; 
                ReadyToMakeImpulseEvent?.Invoke(this, new ReadyToMakeImpulseEventArgs()
                {
                    direction = impulseDirection,
                    force = Random.Range(minForce, maxForce)
                });

                await UniTask.Delay((int)(Random.Range(minReloadTime, maxReloadTime) * 1000));
            }
        }

        private void OnDrawGizmos()
        {
            var myPosition = transform.position;
            var directionToTarget = _targetCollider.bounds.center - myPosition;
            Gizmos.DrawLine(myPosition, directionToTarget * 100);
        }

        private bool IsSeeTarget()
        {
            var myPosition = transform.position;
            var directionToTarget = _targetCollider.bounds.center - myPosition;
            var hit = Physics2D.Raycast(myPosition, directionToTarget * 100);
            if (hit.collider == null) return false;
            if (hit.collider.CompareTag("Player")) return true;
            return false;
        }
    }
}