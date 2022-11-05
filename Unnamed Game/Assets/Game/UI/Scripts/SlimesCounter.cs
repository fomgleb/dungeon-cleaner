using System.Collections.Specialized;
using Game.Dungeon.Scripts;
using TMPro;
using UnityEngine;

namespace Game.UI.Scripts
{
    public class SlimesCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void OnEnable()
        {
            RandomEnemiesSpawner.SpawnedEnemies.CollectionChanged += OnEnemiesCollectionChanged;
        }

        private void OnDisable()
        {
            RandomEnemiesSpawner.SpawnedEnemies.CollectionChanged -= OnEnemiesCollectionChanged;
        }

        private void Start()
        {
            ShowEnemiesCount(RandomEnemiesSpawner.SpawnedEnemies.Count);
        }

        private void OnEnemiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ShowEnemiesCount(RandomEnemiesSpawner.SpawnedEnemies.Count);
        }

        private void ShowEnemiesCount(int count)
        {
            text.text = count.ToString();
        }
    }
}
