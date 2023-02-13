using System;

namespace Game.Entities.Weapon.Scripts
{
    public interface IWeaponAttack
    {
        public event Action HitEnemyEvent;
        public event Action HitWallEvent;
        
        public void Attack();
    }
}
