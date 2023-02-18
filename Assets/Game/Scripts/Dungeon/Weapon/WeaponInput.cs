using System;
using UnityEngine;

namespace Game.Scripts.Dungeon.Weapon
{
    public class WeaponInput : MonoBehaviour
    {
        public event Action UserSentAttackRequestEvent;
        
        public void SendAttackRequest() => UserSentAttackRequestEvent?.Invoke();
    }
}