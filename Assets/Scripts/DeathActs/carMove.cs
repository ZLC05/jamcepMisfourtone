using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class carMove : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Transform> nodes;
    public float speed;
    public float rotationSpeed;
    public int currentNode;

    [Header("Audio")]

    [SerializeField] AudioSource audSource; //Audio source of the car
    [SerializeField] AudioClip[] audClips; //Audio clip array of beep beeps

    void Start()
    {
        if(audSource != null) audSource.PlayOneShot(audClips[Random.Range(0, audClips.Length)]); //Players a random audio clip
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNode < nodes.Count)
        {
            Vector3 targetPos = nodes[currentNode].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            Vector3 lookDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, rotationSpeed * Time.deltaTime, 0);

            transform.rotation = Quaternion.LookRotation(lookDirection);

            //switches to next node
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                currentNode++;
            }
        }
        else if(currentNode >= nodes.Count && currentNode > 0)
        {
            
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerMovement pm = col.gameObject.GetComponent<PlayerMovement>();
            pm.DIE(8);
        }
    }
}
