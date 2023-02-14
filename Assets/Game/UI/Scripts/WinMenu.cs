using System.Collections.Specialized;
using Game.Dungeon.Scripts;
using UnityEngine;

namespace Game.UI.Scripts
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private Animator likerAnimator;
        [SerializeField] private GameObject[] objectsToEnable;
        [SerializeField] private GameObjectSpawner playerSpawner;

        private static readonly int StartLikingTriggerName = Animator.StringToHash("StartLiking");

        private void OnEnable()
        {
            RandomEnemiesSpawner.SpawnedEnemies.CollectionChanged += OnEnemiesCollectionChanged;
        }

        private void OnDisable()
        {
            RandomEnemiesSpawner.SpawnedEnemies.CollectionChanged -= OnEnemiesCollectionChanged;
        }

        private void OnEnemiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (RandomEnemiesSpawner.SpawnedEnemies.Count > 0) return;
            if (e.OldItems == null) return;
            if (playerSpawner.SpawnedObject == null) return;
            foreach (var objectToEnable in objectsToEnable)
                objectToEnable.SetActive(true);
            likerAnimator.SetTrigger(StartLikingTriggerName);
        }
    }
}
