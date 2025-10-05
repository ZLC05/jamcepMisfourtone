using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Car : MonoBehaviour
{
    [SerializeField] Material[] colors; //Array of materials to choose from 

    // Start is called before the first frame update
    void Start()
    {
        assignColor();
    }

    //Function to assign a random color
    private void assignColor()
    {
        Material[] mats = GetComponent<MeshRenderer>().materials;

        mats[3] = colors[Random.Range(0, colors.Length)];

        GetComponent<MeshRenderer>().materials = mats; //Reasigns the materials
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
