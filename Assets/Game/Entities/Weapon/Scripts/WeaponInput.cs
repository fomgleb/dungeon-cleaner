using System;
using UnityEngine;

namespace Game.Entities.Weapon.Scripts
{
    public class WeaponInput : MonoBehaviour
    {
        public event Action UserSentAttackRequestEvent;
        
        public void SendAttackRequest() => UserSentAttackRequestEvent?.Invoke();
    }
}