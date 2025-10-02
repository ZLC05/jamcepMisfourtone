using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFallingItemLethal : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Gravity")]
    public float gravityScale = 1f;

    public static float globalGravity = -9.81f;

    bool lethal;

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
            Debug.Log("DIE");
        }

        if (col.gameObject.tag == "Ground")
        {
            lethal = false;
            Debug.Log("Turned off Lethality");
        }
    }
}
