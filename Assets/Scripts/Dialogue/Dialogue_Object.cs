using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class Dialogue_Object : MonoBehaviour
{
    //Text field
    [SerializeField] TMP_Text displayText; //Text that will be altered

    //References
    private Dialolgue_SO dialogue_SO; //Reference to the dialogue object
    private Dialogue_Manager dialogue_Manager; //Reference to the dialogue manager

    //Values
    private int index; //Index of the current line

    //Audio
    [SerializeField] AudioClip[] audClips; //Audio clips for the dialogue sounds
    [SerializeField] AudioSource audSource; //Audio source reference

    //Special fonts
    [SerializeField] TMP_FontAsset[] styleArray; //Array of all font styles

    //Public function for assigning values
    public void setup(Dialolgue_SO read_SO, Dialogue_Manager DM, int newIndex)
    {
        dialogue_SO = read_SO; //Assigns the scriptable object
        dialogue_Manager = DM; //Assigns the dialogue manager
        index = newIndex; //Assigns the index to read

        //Hide all text, assign it, and start the typewriter
        displayText.maxVisibleCharacters = 0;
        displayText.text = dialogue_SO.lines[index];

        displayText.font = styleArray[dialogue_SO.styleIndex];

        StartCoroutine(typeWriter());
    }

    //Typewriter
    IEnumerator typeWriter()
    {
        displayText.maxVisibleCharacters++; //Adds one to max visible characters

        //Plays a sound
        audSource.pitch = Random.Range(0.99f, 1.02f);
        audSource.PlayOneShot(audClips[Random.Range(0, audClips.Length)]);

        yield return new WaitForSeconds(0.025f); //Wait

        if (displayText.maxVisibleCharacters != displayText.text.Length) //Redo the typewriter if it hasn't finished all text
        {
            StartCoroutine(typeWriter()); 
        }
        else
        {
            yield return new WaitForSeconds(dialogue_SO.timeBetweenLines[index]); //Wait between lines

            //Start next line if the index isn't the last line
            if (index != dialogue_SO.lines.Length - 1)
            {
                dialogue_Manager.startDialogue(dialogue_SO, index + 1);
            }

            yield return new WaitForSeconds(1f); //1 Second delay before fadeout

            StartCoroutine(fadeOut());
        }
    }

    //Fadeout
    IEnumerator fadeOut()
    {
        displayText.color = new Color(1, 1, 1, displayText.color.a - 0.1f);

        yield return new WaitForSeconds(0.05f);

        if (displayText.color.a <= 0)
        {
            dialogue_Manager.deleteFromList(this.gameObject);

            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine (fadeOut());
        }
    }

    //Public function to call to for stopping the coroutine
    public void cancelDialogue()
    {
        StopCoroutine(typeWriter());

        StartCoroutine(fadeOut());
    }
}
