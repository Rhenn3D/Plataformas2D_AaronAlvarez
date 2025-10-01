using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; } //Esto sirve que para acceder (get) sea público y que cuando quiera cambiarlo es privado (private set)
    private int _stars = 0;


    [SerializeField] private InputActionAsset playerInputs;
    private InputAction _pauseInput;
    private bool _isPaused = false;
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

    public void Pause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, false);
            playerInputs.FindActionMap("Player").Enable();
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, true);
            playerInputs.FindActionMap("Player").Disable();
            _isPaused = true;
        }

    }
}
