using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class spawnFallingItem : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Base Item Stats")]

    public GameObject fallingItem;
    public GameObject itemSpawn;

    [Header("If the Item is Thrown Stats")]
    public bool throwItem;
    public float throwForce;

    public bool triggered = false;

    public float itemDeathTime;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !triggered)
        {
            spawnItem();
            triggered = true;
        }
    }


    private void spawnItem()
    {
        Debug.Log("Spawned Item");
        GameObject item = Instantiate(fallingItem, itemSpawn.transform.position, Quaternion.identity);

        Rigidbody itemRb = item.GetComponent<Rigidbody>();

        if (throwItem)
        {
            Debug.Log("added force");
            itemRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
        


        Destroy(item, itemDeathTime);
    }
}
