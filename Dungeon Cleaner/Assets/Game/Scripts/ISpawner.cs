using System;

namespace Game.Scripts
{
    public interface ISpawner
    {
        public event Action SpawnedEvent;
        public void Spawn();
    }
}