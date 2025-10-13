using UnityEngine;

public class Star : MonoBehaviour, IInteractable
{


    [SerializeField] private AudioClip _starSFX;
    //GameManager _gameManager;


    void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interact()
    {
        //_gameManager.AddStar();

        GameManager.instance.AddStar();
        AudioManager.instance.ReproduceSound(_starSFX);
        Destroy(gameObject, 0.5f);


    }
}
