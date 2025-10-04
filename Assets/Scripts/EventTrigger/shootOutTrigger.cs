using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootOutTrigger : MonoBehaviour
{
    public GameObject connectedDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        connectedDeath.SetActive(true);
        shootOutDeath sod = connectedDeath.GetComponent<shootOutDeath>();
        sod.startedblastin();
    }
}
