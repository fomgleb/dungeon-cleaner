using NTC.Global.Pool;
using UnityEngine;

namespace UnnamedGame.LivingEntities.Scripts
{
    [RequireComponent(typeof(CorpseSpawnerWhenDie))]
    public class DespawnerWhenCorpseSpawned : MonoBehaviour
    {
        private CorpseSpawnerWhenDie _corpseSpawnerWhenDie;
        
        private void Awake()
        {
            _corpseSpawnerWhenDie = GetComponent<CorpseSpawnerWhenDie>();
        }

        private void OnEnable()
        {
            _corpseSpawnerWhenDie.CorpseSpawnedEvent += OnCorpseSpawned;
        }

        private void OnDisable()
        {
            _corpseSpawnerWhenDie.CorpseSpawnedEvent -= OnCorpseSpawned;
        }

        private void OnCorpseSpawned() => Destroy(gameObject);
    }
}
