using System;

namespace Game.Scripts.Dungeon.Weapon
{
    public interface IWeaponAttack
    {
        public event Action HitEnemyEvent;
        public event Action HitWallEvent;
        
        public void Attack();
    }
}
