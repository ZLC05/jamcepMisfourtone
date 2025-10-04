using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootOutDeath : MonoBehaviour
{
    public Collider death;
    public float shootingIntverval;
    public float shotscooldown;
    public float reloadcooldown;

    public int amountOfBullets;

    public bool isActive;
    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startedblastin()
    {
        InvokeRepeating("bangBang", 0, shootingIntverval);
    }


    void bangBang()
    {
        if (isShooting)
        {
            shootingIntverval = shotscooldown;
            isShooting = false;
            death.enabled = false;
        }
        else if(!isShooting)
        {
            shootingIntverval = reloadcooldown;
            isShooting = true;
            death.enabled = true;

            StartCoroutine(shots());
            
        }
    }

    IEnumerator shots()
    {

        for(int i = 0; i < amountOfBullets; i++)
        {
            Debug.Log("SPAWN BULLET");
            yield return new WaitForSeconds(0.05f);
        }
        StopAllCoroutines();

    }
}
