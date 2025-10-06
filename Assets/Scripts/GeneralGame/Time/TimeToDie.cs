using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeToDie : MonoBehaviour
{

    public bool pauseTime;

    public float secondsTime;

    public TextMeshProUGUI timeText;

    public BoulderRain br;

    [SerializeField] Dialolgue_SO timeUp_SO; //Scriptable object to show when time is up

    [SerializeField] AudioSource ttdAudSource; //Audio source for ticks
    [SerializeField] AudioSource music; //Audio source for the music

    [SerializeField] bool muteMusic = false; //Default false, mutes music

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

                InvokeRepeating("audioTick", 1, 1);
            }

            if (secondsTime < 0)
            {
                PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                br.startSpawningBoulders();
                pauseTime = true;
                timeText.text = "BOULDER RAIN";

                FindAnyObjectByType<Dialogue_Manager>().startDialogue(timeUp_SO, 0); //Starts reading the time up dialogue
                stopTick();
            }
        }
    }

    //Function to stop the ticking
    public void stopTick()
    {
        CancelInvoke("audioTick");
    }

    //Function to tick
    private void audioTick()
    {
        ttdAudSource.Play();
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

        //Starts and fades the audio in if music is not muted
        if (!muteMusic)
        {
            music.Play();
            StartCoroutine(musicFadeIn());
        }
        
    }

    //Ienumerator to phase the audio in
    IEnumerator musicFadeIn()
    {
        music.volume += 0.05f;

        yield return new WaitForSeconds(0.1f); //Wait

        if (music.volume < 0.25f)
        {
            StartCoroutine (musicFadeIn());
        }
        else
        {
            music.volume = 0.25f; //Auto assigns
        }
    }

    //Pubilc void to mute music
    public void musicMute(Image btnImg)
    {
        if (muteMusic)
        {
            muteMusic = false;
            btnImg.color = Color.white;
        }
        else if (!muteMusic)
        {
            muteMusic= true;
            btnImg.color = Color.green;
        }
    }

    //Public function for audio fade out
    public void musicFadeOut()
    {
        StartCoroutine(musicFade());
    }

    IEnumerator musicFade()
    {
        music.volume -= 0.05f;

        yield return new WaitForSeconds(0.1f); //Wait

        if (music.volume > 0f)
        {
            StartCoroutine(musicFade());
        }
        else
        {
            music.volume = 0; //Auto assigns
            music.Stop();
        }
    }
}
