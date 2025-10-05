using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;

    [SerializeField] float xRotation;
    [SerializeField] float yRotation;

    [SerializeField] bool canMove; //Can the player cam move?


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Public function to lock the cursor and allow camera movement
    public void startGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return; //Return if no movement is allowed

        //get the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
