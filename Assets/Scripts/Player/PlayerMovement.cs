using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Movement")]

    public bool canMove;

    public float speed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public float drag;

    Vector3 direction; 

    Rigidbody rb;

    

    bool touchingGrass;
    [Header("Grass Touch Mechanics")]
    //bigger number, burger radius
    public float touchRadius;

    public LayerMask grassLayer;

    public GameObject grassCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        rb.drag = drag;
    }


    private void inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void movePlayer()
    {
        //find movement directions

        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);
    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void GrassDetection()
    {
        touchingGrass = Physics.CheckSphere(grassCheck.transform.position, touchRadius, grassLayer);
        if (touchingGrass)
        {
            DIE();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            inputs();
        }
        speedControl();
        GrassDetection();

    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    public void DIE()
    {
        Debug.Log("YOU DIED IDIOT");
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(grassCheck.transform.position, touchRadius);
    }
}
