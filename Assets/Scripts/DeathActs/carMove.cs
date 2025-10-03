using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class carMove : MonoBehaviour
{
    // Start is called before the first frame update


    public float speed;
    public GameObject endPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.DIE(8);
        }
    }
}
