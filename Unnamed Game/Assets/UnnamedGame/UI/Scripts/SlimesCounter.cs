using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnnamedGame.Dungeon.Scripts;
using Zenject;

public class SlimesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [Inject] private RandomEnemiesSpawner _randomEnemiesSpawner;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        _randomEnemiesSpawner.SpawnedEnemies.CollectionChanged -= OnEnemiesCollectionChanged;
    }

    private void Start()
    {
        _randomEnemiesSpawner.SpawnedEnemies.CollectionChanged += OnEnemiesCollectionChanged;
        ShowEnemiesCount(_randomEnemiesSpawner.SpawnedEnemies.Count);
    }

    private void OnEnemiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        ShowEnemiesCount(_randomEnemiesSpawner.SpawnedEnemies.Count);
    }

    private void ShowEnemiesCount(int count)
    {
        text.text = count.ToString();
    }
}
