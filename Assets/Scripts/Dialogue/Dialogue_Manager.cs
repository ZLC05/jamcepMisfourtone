using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Manager : MonoBehaviour
{
    //Scriptable object to spawn the dialogue
    [SerializeField] GameObject textObject;

    //Text spawn point
    [SerializeField] GameObject textSpawnPoint;

    //List of text fields to move them up and delete them
    [SerializeField] List<GameObject> textList = new List<GameObject>();

    //Temp test
    [SerializeField] Dialolgue_SO test;

    //Public function to start a new multiline
    public void startDialogue(Dialolgue_SO read_SO, int index)
    {
        //Check if the list is longer than 1 to move prior text objects up
        if (textList.Count >= 1)
        {
            foreach (GameObject priorTextObj in textList)
            {
                priorTextObj.transform.position = new Vector3(priorTextObj.transform.position.x, priorTextObj.transform.position.y + 118, priorTextObj.transform.position.z);
            }
        }

        //Spawn the object
        GameObject textObj = Instantiate(textObject, textSpawnPoint.transform.position, Quaternion.identity, this.transform);

        //Add it to the list
        textList.Add(textObj);

        //Grab the script to set it up
        Dialogue_Object textScript = textObj.GetComponent<Dialogue_Object>();

        //Setup and Start reading text
        textScript.setup(read_SO, this, index);

    }

    //Public function to delete lines from the list after a set time
    public void deleteFromList(GameObject deleteObj)
    {
        textList.Remove(deleteObj);
    }
}
