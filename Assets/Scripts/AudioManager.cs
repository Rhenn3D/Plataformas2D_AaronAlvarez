using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;

    public AudioClipLoadType menuBGM;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }


    void Start()
    {

    }

    void Update()
    {

    }

    public void ReproduceSound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public void ChangeBGM(AudioClip bgmclip)
    {
        _bgmSource.clip = bgmclip;
        _bgmSource.Play();
    }
}
