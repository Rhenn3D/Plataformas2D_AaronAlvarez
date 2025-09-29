using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; } //Esto sirve que para acceder (get) sea público y que cuando quiera cambiarlo es privado (private set)
    private int _stars = 0;
    [SerializeField] private GameObject _pauseCanvas;

    [SerializeField] private InputActionAsset playerInputs;
    private InputAction _pauseInput;
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
        Time.timeScale = 0;
        _pauseCanvas.SetActive(true);
        playerInputs.FindActionMap("Player").Disable();
    }
}
