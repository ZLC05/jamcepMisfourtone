using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeToDie : MonoBehaviour
{

    public bool pauseTime;

    public float secondsTime;

    public TextMeshProUGUI timeText;

    public BoulderRain br;

    [SerializeField] Dialolgue_SO timeUp_SO; //Scriptable object to show when time is up

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseTime)
        {
            secondsTime = secondsTime -= 1f * Time.deltaTime;

            int min = Mathf.FloorToInt(secondsTime / 60f);

            int sec = Mathf.FloorToInt(secondsTime % 60f);

            string secString;

            if (sec >= 10) secString = sec.ToString();

            else secString = "0" + sec.ToString();

            float ms = secondsTime - Mathf.FloorToInt(secondsTime);

            //string time = string.Format("{0:00}:{1:00}", min, sec);

            string time = $"{min}:{secString}{ms.ToString("F2").Remove(0, 1)}";

            timeText.text = time;

            if (min < 1 && timeText.color == Color.white) //Changes color for urgency
            {
                timeText.color = Color.red;
            }

            if (secondsTime < 0)
            {
                PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                br.startSpawningBoulders();
                pauseTime = true;
                timeText.text = "BOULDER RAIN";

                FindAnyObjectByType<Dialogue_Manager>().startDialogue(timeUp_SO, 0); //Starts reading the time up dialogue
            }
        }
    }

    //Public function to start the timer
    public void startTimer()
    {
        StartCoroutine(timerBlinknStart());
    }

    IEnumerator timerBlinknStart()
    {
        timeText.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(0.15f);

        timeText.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(0.15f);

        timeText.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(0.15f);

        timeText.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(0.15f);

        timeText.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(0.15f);

        pauseTime = false; //Starts the timer
    }
}
