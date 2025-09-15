using System.Collections.Generic;
using UnityEngine;

public class Repaso : MonoBehaviour
{

    [SerializeField]private int _variableInt = 10;
    public float variableFloat = 3.5f;

    public string vairableString = "Hola Mundo";
    public bool variableBool = false;
    public int[] arrayInt = new int[5] {12, 4, 8, 9, 8};
    public List<int> listInt = new List<int>(9) {8, 9, 0};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int numero = 9;
        if (numero == 7)
        {
            //
            //
        }
        else if (numero == 3)
        {
            //
        }
        else
        {

        }
        for (int i = 0; i < 5; i++)
        {

        }
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
