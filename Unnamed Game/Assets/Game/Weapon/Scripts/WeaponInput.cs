using System;
using UnityEngine;

namespace UnnamedGame.Weapon.Scripts
{
    public class WeaponInput : MonoBehaviour
    {
        public event Action UserSentAttackRequestEvent;
        
        public void SendAttackRequest() => UserSentAttackRequestEvent?.Invoke();
    }
}