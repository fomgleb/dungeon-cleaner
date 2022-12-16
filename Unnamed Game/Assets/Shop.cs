using Game.Entities.Weapon.Knife.Scripts;
using UnityEngine;
using UnnamedGame.Weapon.Scripts;

public class Shop : MonoBehaviour
{
    public int knifeDamagePrice = 10;
    public int knifeRangePrice = 10;

    private Wallet wallet;
    
    private void Start()
    {
        wallet = FindObjectOfType<Wallet>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && wallet.money >= knifeDamagePrice)
        {
            FindObjectOfType<WeaponAttack>().damage++;
            wallet.money -= (uint)knifeDamagePrice;
            knifeDamagePrice *= 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && wallet.money >= knifeRangePrice)
        {
            FindObjectOfType<KnifeAttack>().attackRange++;
            wallet.money -= (uint)knifeRangePrice;
            knifeRangePrice *= 2;
        }
    }
}
