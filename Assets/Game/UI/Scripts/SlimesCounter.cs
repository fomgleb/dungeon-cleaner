using System.Collections.Specialized;
using Game.Dungeon.Scripts;
using TMPro;
using UnityEngine;

namespace Game.UI.Scripts
{
    public class SlimesCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;

        private void OnEnable()
        {
            enemiesSpawner.SpawnedEnemies.CollectionChanged += OnEnemiesCollectionChanged;
        }

        private void OnDisable()
        {
            enemiesSpawner.SpawnedEnemies.CollectionChanged -= OnEnemiesCollectionChanged;
        }

        private void OnEnemiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ShowEnemiesCount(enemiesSpawner.SpawnedEnemies.Count);
        }

        public void ShowEnemiesCount(int count)
        {
            text.text = count.ToString();
        }
    }
}
