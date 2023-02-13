using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scene_Transition
{
    public class SceneTransition : MonoBehaviour
    {
        public static event Action SceneIsSwitchingEvent;
        
        private event Action startAnimationEnded;
        
        private static SceneTransition instance;
        private static bool playEndAnimation = false;

        [SerializeField] private TMP_Text loadingPercentage;
        [SerializeField] private Image progressBar;
        
        private Animator animator;
        private AsyncOperation sceneLoadingOperation;

        public static void SwitchScene(string sceneName)
        {
            SceneIsSwitchingEvent?.Invoke();
            instance.animator.SetTrigger("Start");
            instance.sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
            instance.sceneLoadingOperation.allowSceneActivation = false;
        }

        private void Awake()
        {
            instance = this;
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (playEndAnimation)
                instance.animator.SetTrigger("End");
        }

        private void Update()
        {
            if (sceneLoadingOperation == null) return;
            loadingPercentage.text = Mathf.RoundToInt(sceneLoadingOperation.progress * 100) + "%";
            progressBar.fillAmount = sceneLoadingOperation.progress;
        }

        public void OnAnimationOver()
        {
            playEndAnimation = true;
            sceneLoadingOperation.allowSceneActivation = true;
        }
    }
}
