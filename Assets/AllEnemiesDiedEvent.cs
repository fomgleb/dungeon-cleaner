using Game.Dungeon.Scripts;
using Game.UI.Scripts;
using UnityEngine;

public class AllEnemiesDiedEvent : MonoBehaviour
{
    [SerializeField] private RandomEnemiesSpawner enemiesSpawner;
    [SerializeField] private GameObjectSpawner playerSpawner;
    
    [SerializeField] private DungeonMusicPlayer dungeonMusicPlayer;
    [SerializeField] private WinMenu winMenu;

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
        dungeonMusicPlayer.StopSmoothly();
        if (playerSpawner.SpawnedObject != null)
            winMenu.Show();
    }
}
