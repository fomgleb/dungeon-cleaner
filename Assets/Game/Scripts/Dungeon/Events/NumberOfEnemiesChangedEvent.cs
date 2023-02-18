using System.Collections.Specialized;
using Game.Scripts.Dungeon.Enemies_Spawner;
using UnityEngine;

namespace Game.Scripts.Dungeon.Events
{
    public class NumberOfEnemiesChangedEvent : MonoBehaviour
    {
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;

        [SerializeField] private SlimesCounter slimesCounter;

        private void OnEnable()
        {
            enemiesSpawner.SpawnedEnemies.CollectionChanged += OnNumberOfEnemiesChanged;
        }

        private void OnDisable()
        {
            enemiesSpawner.SpawnedEnemies.CollectionChanged -= OnNumberOfEnemiesChanged;
        }

        private void OnNumberOfEnemiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            slimesCounter.ShowEnemiesCount(enemiesSpawner.SpawnedEnemies.Count);
        }
    }
}
