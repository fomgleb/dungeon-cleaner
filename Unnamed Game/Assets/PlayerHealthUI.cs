using TMPro;
using UnityEngine;
using UnnamedGame.LivingEntities.Scripts;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Damageable playerDamageable;

    private TMP_Text _text; 
    
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        playerDamageable.GotDamageEvent += OnGotDamage;
    }

    private void OnDisable()
    {
        playerDamageable.GotDamageEvent -= OnGotDamage;
    }

    private void OnGotDamage() => _text.text = $"{playerDamageable.Health} / {playerDamageable.MaxHealth}";
}
