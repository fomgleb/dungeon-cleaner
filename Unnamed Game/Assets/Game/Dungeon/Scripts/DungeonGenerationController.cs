using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using UnnamedGame.Dungeon.Scripts;

namespace Game.Dungeon.Scripts
{
    public class DungeonGenerationController : MonoBehaviour
    {
        [SerializeField] private DungeonGeneratorBase dungeonGenerator;
        [SerializeField] private RandomTorchesInDungeonGenerator torchesGenerator;
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;
        [SerializeField] private Button nextButton;

        private void OnEnable()
        {
            DungeonGeneratorBase.DungeonDestroyedEvent += OnDungeonDestroyed;
            RandomEnemiesSpawner.SpawnedEnemies.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (RandomEnemiesSpawner.SpawnedEnemies.Count > 0) return;
            if (e.OldItems == null) return;
            nextButton.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            DungeonGeneratorBase.DungeonDestroyedEvent -= OnDungeonDestroyed;
        }

        public void _LoadNextLevel()
        {
            nextButton.gameObject.SetActive(false);
            dungeonGenerator._GenerateDungeon();
            torchesGenerator._GenerateTorches();
            enemiesSpawner._SpawnEnemies();
            GameObject.FindWithTag("Player").transform.position = new Vector3(0.5f, 0.5f);
        }

        private void Start()
        {
            dungeonGenerator._GenerateDungeon();
            torchesGenerator._GenerateTorches();
            enemiesSpawner._SpawnEnemies();
        }

        private void OnDungeonDestroyed() => enemiesSpawner._DespawnEnemies();
    }
}
