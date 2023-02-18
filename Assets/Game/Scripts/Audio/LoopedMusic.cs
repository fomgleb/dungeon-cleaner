using System;
using UnityEngine;

namespace Game.Scripts.Audio
{
    [Serializable]
    public struct LoopedMusic
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private float startTime;
        public AudioClip Clip => clip;
        public float StartTime => startTime;
    }
}
