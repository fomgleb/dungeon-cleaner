using Game.Scripts.Dungeon.Cave;
using Game.Scripts.Dungeon.Torches;
using Game.Scripts.Tiles;
using UnityEngine;

namespace Game.Scripts.Main_Menu
{
    public class MainMenuGenerationController : MonoBehaviour
    {
        [SerializeField] private SimpleRandomWalkDungeonGenerator rightCaveGenerator;
        [SerializeField] private SimpleRandomWalkDungeonGenerator leftCaveGenerator;
        [SerializeField] private RandomTorchesInDungeonGenerator torchesGenerator;
        [SerializeField] private TilesSquareSpawner floorDestroyer;
        [SerializeField] private TilesSquareSpawner wallShadowDestroyer;
        [SerializeField] private TilesSquareSpawner foundationGenerator;

        private void Start()
        {
            _GenerateMainMenu();
        }

        public void _GenerateMainMenu()
        {
            rightCaveGenerator._GenerateDungeon();
            leftCaveGenerator._GenerateDungeon();
            floorDestroyer._Spawn();
            wallShadowDestroyer._Spawn();
            foundationGenerator._Spawn();
            torchesGenerator._GenerateTorches();
        }
    }
}
