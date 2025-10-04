using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterCooler : MonoBehaviour
{

    bool isPlayerInRadius;
    bool active;
    public LayerMask playerLayer;
    public float deathRadius;


    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            isPlayerInRadius = Physics.CheckSphere(transform.position, deathRadius, playerLayer);
            if (isPlayerInRadius)
            {
                PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                pm.DIE(4);
                active = false;
            }
        }

    }

    public void OnDrawGizmos()
    {
        //visuals of grandmas kill radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deathRadius);
    }
}
