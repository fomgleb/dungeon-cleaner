using System;
using UnityEngine;

namespace Game.Audio.Scripts
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
