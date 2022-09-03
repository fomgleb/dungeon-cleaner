using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using UnnamedGame.Dungeon.Scripts;
using Zenject;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Animator likerAnimator;
    
    [SerializeField] private GameObject[] objectsToEnable;

    [Inject] private RandomEnemiesSpawner _randomEnemiesSpawner;

    private void OnEnable()
    {
        _randomEnemiesSpawner.SpawnedEnemies.CollectionChanged += OnEnemiesCollectionChanged;
    }

    private void OnDisable()
    {
        _randomEnemiesSpawner.SpawnedEnemies.CollectionChanged -= OnEnemiesCollectionChanged;
    }

    private void OnEnemiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (_randomEnemiesSpawner.SpawnedEnemies.Count > 0) return;
        if (Time.timeSinceLevelLoad < 10) return;
        foreach (var objectToEnable in objectsToEnable)
            objectToEnable.SetActive(true);
        likerAnimator.SetTrigger("StartLiking");
    }
}
