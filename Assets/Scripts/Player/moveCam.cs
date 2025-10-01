using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCam : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform camPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camPosition.position;
    }
}
