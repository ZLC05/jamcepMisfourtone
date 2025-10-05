using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    //Dialogue References
    [SerializeField] Dialolgue_SO read_SO;

    //Other effects
    [SerializeField] TimeToDie ttk;

    //Trigger function
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindFirstObjectByType<Dialogue_Manager>().startDialogue(read_SO, 0); //Starts the appropriate dialogue

            if (ttk != null) ttk.startTimer(); //If this is not null, start the timer

            Destroy(this.gameObject); //Destroy this trigger after
        }
    }
}
