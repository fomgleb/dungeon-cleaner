using System.Collections.Specialized;
using Game.Dungeon.Scripts;
using UnityEngine;
using UnnamedGame.LivingEntities.Player.Scripts;
using Zenject;

namespace Game.UI.Scripts
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private Animator likerAnimator;
        [SerializeField] private GameObject[] objectsToEnable;

        [Inject] private PlayerInput playerInput;
    
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
            if (playerInput == null) return;   
            foreach (var objectToEnable in objectsToEnable)
                objectToEnable.SetActive(true);
            likerAnimator.SetTrigger(StartLikingTriggerName);
        }
    }
}
