using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootOutDeath : MonoBehaviour
{
    public Collider death;
    public float shootingIntverval;

    public int amountOfBullets;

    public bool isActive;
    bool isShooting;

    [SerializeField] bool swapped;

    public GameObject bullet;
    public GameObject bulletSpawn, boulderspawn;
    public float bulletForce;

    [Header("Audio")]

    [SerializeField] AudioSource audSource;
    [SerializeField] AudioClip[] bulletSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            StopAllCoroutines();
            CancelInvoke();
        }
    }


    public void startedblastin()
    {
        bangBang();
    }


    void bangBang()
    {
        if (isShooting) return; //Return so this does not stack - Ink

            //start shooting
            death.enabled = true;
            isShooting = true;
            StartCoroutine(shots());
            Debug.Log(this.gameObject.name + "Has started shooting");
    }

    IEnumerator shots()
    {

        for(int i = 0; i < amountOfBullets; i++)
        {
            //spawn bullets here
            int rand = Random.Range(0, 100);
            Vector3 spawnTransform = new Vector3(bulletSpawn.transform.position.x, bulletSpawn.transform.position.y + Random.Range(-2, 2f), bulletSpawn.transform.position.z + Random.Range(-2f, 2f));


            if(rand == 23)
            {
                GameObject bul = Instantiate(boulderspawn, spawnTransform, bulletSpawn.transform.rotation);
                Destroy(bul, 7f);
                Rigidbody rb = bul.GetComponent<Rigidbody>();

                if (!swapped)
                {
                    rb.AddForce(bul.transform.right * bulletForce, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(bul.transform.right * -bulletForce, ForceMode.Impulse);
                }
            }
            else
            {
                GameObject bul = Instantiate(bullet, spawnTransform, bulletSpawn.transform.rotation);
                Destroy(bul, 7f);
                Rigidbody rb = bul.GetComponent<Rigidbody>();

                if (!swapped)
                {
                    rb.AddForce(bul.transform.right * bulletForce, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(bul.transform.right * -bulletForce, ForceMode.Impulse);
                }
            }



            audSource.PlayOneShot(bulletSounds[Random.Range(0, bulletSounds.Length)]);

            yield return new WaitForSeconds(0.15f);
            
        }

        //stopped shooting
        death.enabled = false;
        isShooting = false;
        Debug.Log(this.gameObject.name + "Has stopped shooting");

        StopAllCoroutines();

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {


            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            
            pm.DIE(5);
        }
    }
}
