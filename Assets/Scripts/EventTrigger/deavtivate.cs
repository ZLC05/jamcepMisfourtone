using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deavtivate : MonoBehaviour
{

    public shootOutDeath[] allDeath;
    public shootOutTrigger[] allTrigger;

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
        if (other.gameObject.tag == "Player")
        {
            foreach (var d in allDeath)
            {
                d.isActive = false;
            }

            foreach (var d in allTrigger)
            {
                d.isactive = false;
            }
        }
    }
}
