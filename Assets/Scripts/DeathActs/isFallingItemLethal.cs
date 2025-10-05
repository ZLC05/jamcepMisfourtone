using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFallingItemLethal : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Gravity")]
    public float gravityScale = 1f;

    public static float globalGravity = -9.81f;

    [SerializeField] bool lethal;

    public bool isPiano;

    //    ID 1: Piano
    //    ID 2: Boulder
    //    ID 3: Trash
    public int DeathID;

    Rigidbody rb;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        lethal = true;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);

        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && lethal)
        {
            PlayerMovement pm = col.gameObject.GetComponent<PlayerMovement>();
            pm.DIE(DeathID);
        }

        if (col.gameObject.tag == "Ground")
        {
            lethal = false;
            if (isPiano)
            {
                AudioSource ad = GetComponent<AudioSource>();
                ad.Play();
                Debug.Log("THIS IS A PIANO");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && lethal)
        {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.DIE(DeathID);
        }

        if (collision.gameObject.tag == "Ground")
        {
            if (lethal) lethal = false; //Disables the lethality
        }
    }
}
