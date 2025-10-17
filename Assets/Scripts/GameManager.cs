using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; } //Esto sirve que para acceder (get) sea público y que cuando quiera cambiarlo es privado (private set)
    public int _stars = 0;
    public int _coins = 0;
    public int maxStarsNeeded = 3;


    public InputActionAsset playerInputs;
    private InputAction _pauseInput;
    public bool _isPaused = false;
    [SerializeField] private AudioClip victorySFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip primerNivelSFX;
    [SerializeField] private AudioClip MainMenuSFX;
    [SerializeField] private SceneMusicData _sceneMusicData; //Esto esta creado por ChatGPT. En el Script "SceneMusicData" lo explico.



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
        DontDestroyOnLoad(gameObject);


        _pauseInput = InputSystem.actions["Pause"];
    }

    void Start()
    {
        GameObject[] estrellas = GameObject.FindGameObjectsWithTag("Star");
        maxStarsNeeded = estrellas.Length;
        MusicData();


    }
    void Update()
    {
        if (_pauseInput.WasPressedThisFrame())
        {
            Pause();
        }
    }


    public void AddStar()
    {
        _stars++;
        Debug.Log("Estrellas recogicas: " + _stars);
    }
    public void AddCoin()
    {
        _coins++;
        Debug.Log("Monedas recogidas: " + _coins);
    }

    public void UpdateStarText()
    {
        GUIManager.Instance.StarText();
    }
    public void UpdateCoinText()
    {
        GUIManager.Instance.CoinText();
    }

    public void Pause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, false);
            playerInputs.FindActionMap("Player").Enable();
            _isPaused = false;
             
             if (AudioManager.instance != null && AudioManager.instance._bgmSource != null)
            {
                AudioManager.instance._bgmSource.volume = 1f; // Volumen normal
            }
        }
        else
        {
            Time.timeScale = 0;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, true);
            playerInputs.FindActionMap("Player").Disable();
            _isPaused = true;
            if (AudioManager.instance != null && AudioManager.instance._bgmSource != null)
            {
                AudioManager.instance._bgmSource.volume = 0.2f; // Ajusta el volumen que quieras
            }
        }

    }
    public void Victory()
    {
        if (_stars >= maxStarsNeeded)
        {
            AudioManager.instance.StopBGM();
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance.victoryCanvas, true);
            Debug.Log("¡Has ganado!");
            playerInputs.FindActionMap("Player").Disable();
            Time.timeScale = 0;
            AudioManager.instance.ReproduceSound(victorySFX);
            ResetGame();


        }
    }

    public void ResetGame()
    {
        _stars = 0;
        _coins = 0;
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
        ResetGame();
    }

    public void MusicData()
        {
            PlaySceneMusic(SceneManager.GetActiveScene().name);
        }

    private void OnEnable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.name);
    }

    private void PlaySceneMusic(string sceneName)
    {
        if (_sceneMusicData == null || _sceneMusicData.sceneMusics.Length == 0) return;

        foreach (var entry in _sceneMusicData.sceneMusics)
        {
            if (entry == null) continue;
            if (entry.sceneName == sceneName && entry.musicClip != null)
            {
                AudioManager.instance.ReproduceMusic(entry.musicClip);
                return;
            }
        }

        // Si la escena no tiene música asignada
        AudioManager.instance.StopBGM();
    }
}
