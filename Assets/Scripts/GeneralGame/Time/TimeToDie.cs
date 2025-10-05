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

            string time = string.Format("{0:00}:{1:00}", min, sec);

            timeText.text = time;

            if (secondsTime < 0)
            {
                PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                br.startSpawningBoulders();
                pauseTime = true;
                timeText.text = "BOULDER RAIN";
            }
        }
    }
}
