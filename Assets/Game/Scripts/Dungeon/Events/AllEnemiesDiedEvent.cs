using Game.Scripts.Dungeon.Enemies_Spawner;
using Game.Scripts.Dungeon.Menus;
using Game.Scripts.Game_Object;
using UnityEngine;

namespace Game.Scripts.Dungeon.Events
{
    public class AllEnemiesDiedEvent : MonoBehaviour
    {
        [SerializeField] private RandomEnemiesSpawner enemiesSpawner;
        [SerializeField] private GameObjectSpawner playerSpawner;
    
        [SerializeField] private DungeonMusicPlayer dungeonMusicPlayer;
        [SerializeField] private WinMenu winMenu;
        [SerializeField] private Window menuWindow;

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
            {
                winMenu.Show();
                menuWindow.ShowAsync();
            }
        }
    }
}
