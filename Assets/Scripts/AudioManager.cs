using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource _bgmSource; // Música de fondo
    public AudioSource _sfxSource; // Efectos de sonido

    void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Música
        if (_bgmSource == null)
        {
            _bgmSource = GetComponent<AudioSource>();
            if (_bgmSource == null)
            {
                _bgmSource = gameObject.AddComponent<AudioSource>();
                Debug.LogWarning("AudioManager: Se creó un AudioSource para BGM automáticamente.");
            }
        }
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;
        _bgmSource.clip = null;

        // SFX
        if (_sfxSource == null)
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length > 0)
            {
                _sfxSource = sources[0] != _bgmSource ? sources[0] : (sources.Length > 1 ? sources[1] : null);
            }

            if (_sfxSource == null)
            {
                _sfxSource = gameObject.AddComponent<AudioSource>();
                Debug.LogWarning("AudioManager: Se creó un AudioSource para SFX automáticamente.");
            }
        }
        _sfxSource.loop = false;
        _sfxSource.playOnAwake = false;
    }

    // -------------------
    // REPRODUCIR EFECTOS
    // -------------------
    public void ReproduceSound(AudioClip clip)
    {
        if (_sfxSource == null || clip == null) return;
        _sfxSource.PlayOneShot(clip); // Se reproduce solo una vez, no hace loop
    }

    // -------------------
    // REPRODUCIR MÚSICA
    // -------------------
    public void ReproduceMusic(AudioClip clip)
    {
        if (_bgmSource == null || clip == null) return;
        if (_bgmSource.clip == clip && _bgmSource.isPlaying) return; // Evita reproducir de nuevo
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void ChangeBGM(AudioClip bgmClip)
    {
        if (_bgmSource == null || bgmClip == null) return;
        _bgmSource.Stop();
        _bgmSource.clip = bgmClip;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        if (_bgmSource != null && _bgmSource.isPlaying)
            _bgmSource.Stop();
    }
}