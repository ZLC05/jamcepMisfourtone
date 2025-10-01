using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Movement")]

    public float speed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public float drag;

    Vector3 direction; 

    Rigidbody rb;



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

    // Update is called once per frame
    void Update()
    {
        inputs();
        speedControl();
    }

    private void FixedUpdate()
    {
        movePlayer();
    }
}
