using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Architecture.Camera.Scripts
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        
        private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
             cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public async void ShakeCameraAsync(float intensity, float time)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            for (var elapsedTime = 0f; elapsedTime < time; elapsedTime += Time.deltaTime)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= (intensity / time) * Time.deltaTime;
                await UniTask.Yield();
            }
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }
    }
}
