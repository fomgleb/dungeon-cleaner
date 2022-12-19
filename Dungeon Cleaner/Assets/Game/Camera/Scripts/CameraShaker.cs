using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnnamedGame.Camera.Scripts
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
             _cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public async void ShakeCameraAsync(float intensity, float time)
        {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            for (var elapsedTime = 0f; elapsedTime < time; elapsedTime += Time.deltaTime)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= (intensity / time) * Time.deltaTime;
                await UniTask.Yield();
            }
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }
    }
}
