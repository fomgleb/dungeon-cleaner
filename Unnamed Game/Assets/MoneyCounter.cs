using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    private Wallet wallet;
    
    private void Start()
    {
        wallet = GameObject.FindWithTag("Player").GetComponent<Wallet>();
    }

    private void Update()
    {
        text.text = wallet.money.ToString();
    }
}
