using Game.Dungeon.Scripts;
using UnityEngine;

namespace Game.UI.Scripts
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private Animator likerAnimator;
        [SerializeField] private GameObject[] objectsToEnable;
        [SerializeField] private GameObjectSpawner playerSpawner;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;

        private static readonly int StartLikingTriggerName = Animator.StringToHash("StartLiking");

        private void OnEnable()
        {
            enemiesSpawner.AllEnemiesDiedEvent += OnAllEnemiesDied;
        }

        private void OnDisable()
        {
            enemiesSpawner.AllEnemiesDiedEvent -= OnAllEnemiesDied;
        }

        private void OnAllEnemiesDied()
        {
            if (playerSpawner.SpawnedObject == null) return;
            foreach (var objectToEnable in objectsToEnable)
                objectToEnable.SetActive(true);
            likerAnimator.SetTrigger(StartLikingTriggerName);
        }
    }
}
