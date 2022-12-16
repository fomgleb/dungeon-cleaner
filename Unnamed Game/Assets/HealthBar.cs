using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entities.LivingEntities.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Damageable damageable;

    private Slider slider;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        damageable.HealthChangedEvent += OnHealthChanged;
    }

    private void OnDisable()
    {
        damageable.HealthChangedEvent -= OnHealthChanged;
    }

    private void OnHealthChanged(object sender, Damageable.HealthChangedEventArgs e)
    {
        canvasGroup.alpha = 1;
        slider.value = damageable.Health / damageable.MaxHealth;
    }
}
