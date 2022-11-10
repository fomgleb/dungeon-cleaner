using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MainMenuMusicPlayer : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mainMenuMusic;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playButton.onClick.AddListener(Disappear);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        // playButton.onClick.RemoveListener(Disappear);
    }

    private void Start()
    {
        PlayMainMenuMusic();
    }

    private void PlayMainMenuMusic()
    {
        audioSource.clip = mainMenuMusic;
        // audioSource.time = Mathf.Lerp(0, mainMenuMusic.length, Random.Range(0f, 1f));
        audioSource.Play();
    }

    private void Disappear()
    {
        animator.SetTrigger("Disappear");
    }
}
