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

    bool triggered = false;
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
        GameObject item = Instantiate(fallingItem, itemSpawn.transform.position, Quaternion.identity);

        Destroy(item, 3f);
    }
}
