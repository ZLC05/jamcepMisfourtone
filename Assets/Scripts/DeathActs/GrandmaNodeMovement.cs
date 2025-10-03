using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GrandmaNodeMovement : MonoBehaviour
{
    [Header("Put grandmas pathing nodes here in order of what nodes to go to")]
    public List<Transform> nodes;
    private int nodeIndex;


    [Header("Grandma Stats")]

    public bool canGrandmaKill;
    public bool grandaActivated;
    
    public float grandmaSightRadius;
    public float grandmaKillRadius;


    public float grandmaSpeed;
    public float rotationSpeed;


    public LayerMask playerLayer;

    bool isPlayerInRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //runs as update to check if the player is inside of the grandmas radius, if ever set to true, start the grandma walk sequence
        isPlayerInRadius = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), grandmaKillRadius, playerLayer);


        //checks if grandma is activated, if so then it will start the program if the player enters her LOS
        if (grandaActivated)
        {
            if (isPlayerInRadius)
            {
                //this is where you will put the code to start the chat sequence, be sure to add code to pause the player (which already has a bool) and pause grandma until she is done chatting
                
                grandmaSightRadius = grandmaKillRadius;
                canGrandmaKill = true;

                //Node system, will go from node to node and when grandma arrives at a node, it will go to next node and look towards next node
                if (nodeIndex < nodes.Count)
                {
                    Vector3 targetPos = nodes[nodeIndex].position;
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, grandmaSpeed * Time.deltaTime);
                    Vector3 lookDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, rotationSpeed * Time.deltaTime, 0);

                    transform.rotation = Quaternion.LookRotation(lookDirection);

                    //switches to next node
                    if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                    {
                        nodeIndex++;
                    }
                }
                else
                {
                    //when grandma arrives at her destination, she is deacvivated, this is where you can start the chat sequence
                    canGrandmaKill = false;
                    grandaActivated = false;
                }

            }

            //the death conditions
            if (!isPlayerInRadius && canGrandmaKill)
            {
                PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                pm.DIE();
            }
        }


    }

    public void OnDrawGizmos()
    {
        //visuals of grandmas kill radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), grandmaSightRadius);
    }
}
