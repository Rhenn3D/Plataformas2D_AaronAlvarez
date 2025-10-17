using UnityEngine;

public class Star : MonoBehaviour, IInteractable
{


    [SerializeField] private AudioClip _starSFX;
    private BoxCollider2D _boxCollider;
    //GameManager _gameManager;


    void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Interact()
    {
        //_gameManager.AddStar();

        _boxCollider.enabled = false;
        GameManager.instance.AddStar();
        GameManager.instance.Victory();
        AudioManager.instance.ReproduceSound(_starSFX);
        GUIManager.Instance.StarText();
        //GameManager.instance.Victory();
        Destroy(gameObject);

    }
}
