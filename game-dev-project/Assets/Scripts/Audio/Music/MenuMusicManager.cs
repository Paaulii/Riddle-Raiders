using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: make this singleton be able to use not only in menu
public class MenuMusicManager : Singleton<MenuMusicManager>
{
    [SerializeField] private AudioClip menuMusic;

    AudioSource audioSource;
    private Scene activeScene;
    private static MenuMusicManager menuMusicManagerInctance;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GlobalVolumeManager.GetMusicVolume();
    }
    
    private void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        if (activeScene.name.StartsWith("Level_"))
        {
            Destroy(gameObject);
        }
        audioSource.volume = GlobalVolumeManager.GetMusicVolume();
    }

    public void PlayMusic()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();

    }
}
