using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialolgue_SO : ScriptableObject
{
    public string[] lines; //Lines of dialogue

    public float[] timeBetweenLines; //Amount of time between reading lines
}
