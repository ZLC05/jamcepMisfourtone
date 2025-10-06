using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public TimeToDie ttd;

    [Header("Movement")]

    public bool canMove;

    public float speed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public float drag;

    Vector3 direction; 

    Rigidbody rb;

    

    bool touchingGrass;
    [Header("Grass Touch Mechanics")]
    //bigger number, burger radius
    public float touchRadius;

    public LayerMask grassLayer;

    public GameObject grassCheck;

    [Header("Death Stuff (Open Script to Line 40")]

    //Each death messege is orginized
    /*
    ID 0: Grass - You Touched Grass, How could you?
    ID 1: Piano - You heard that loud sound of keys right? welp thats called a piano falling on your head. Look up every now and then
    ID 2: Boulder - hehehehehehheehehhehehhehehhehehhehehehhehhehehhehe
    ID 3: Trash - geeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeet dunked on
    ID 4: Watercooler - The Aura of the Watercooler was to much for you to handle and you perished by being in its presence
    ID 5: Crossfire - 87 bullet holes? Maybe look left or right before walking into an allyway
    ID 6: Grandma - uhh yeah maybe dont leave grandma to get hit by cars and maybe she wont beat you up
    ID 7: Late to work - You were late to work, so a boulder dropped on your head, nice
    ID 8: Car - DRIVING IN MY CAR RIGHT AFTER A ROOT BEER, HEY THAT BUMP IS SHAPED LIKE A DEER
    ID 9: Sewer - They are called manholes for a reason.
    */
    public string[] deathMessege;

    [SerializeField] Dialolgue_SO[] deathMessages_SO; //Array of death message sciptable objects
    [SerializeField] AudioClip[] deathAudios; //Array for death audio sounds

    public GameObject deathcanvasUI;
    public GameObject timerUI;

    //death text
    public TextMeshProUGUI deathMessegeBox;

    [SerializeField] private int devDeathNumber;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        rb.drag = drag;

    }

    private void Awake()
    {
        deathcanvasUI.SetActive(false);
        timerUI.SetActive(true);
    }

    private void inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void movePlayer()
    {
        //find movement directions

        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force);
    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void GrassDetection()
    {
        touchingGrass = Physics.CheckSphere(grassCheck.transform.position, touchRadius, grassLayer);
        if (touchingGrass)
        {
            DIE(0);
        }
    }

    //Public function for starting the game
    public void startGame()
    {
        canMove = true;

        FindFirstObjectByType<PlayerCam>().startGame(); //Unlocks the camera
    }


    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            inputs();
            speedControl();
            GrassDetection();
        }
        

        if (Input.GetKeyDown(KeyCode.F12))
        {
            
            DIE(devDeathNumber);
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    public void DIE(int ID)
    {
        if (!canMove) return; //Avoid dupe dialogue

        Debug.Log("YOU DIED IDIOT");
        //Calls to stop gradma specifically [and the ttk to mute music]
        FindFirstObjectByType<GrandmaNodeMovement>().canGrandmaKill = false;
        FindFirstObjectByType<TimeToDie>().musicFadeOut();

        //Calls to stop all other dialogue

        var dialogues = FindObjectsByType<Dialogue_Object>(FindObjectsSortMode.None);

        foreach (Dialogue_Object dO in dialogues)
        {
            dO.cancelDialogue();
        }

        deathcanvasUI.SetActive(true);
        timerUI.SetActive(false);
        ttd.pauseTime = true;

        //Plays the according audio and dialogue
        if (deathAudios[ID] != null) GetComponent<AudioSource>().PlayOneShot(deathAudios[ID]);
        FindFirstObjectByType<Dialogue_Manager>().startDialogue(deathMessages_SO[ID], 0);

        canMove = false; //No more movement
        rb.constraints = RigidbodyConstraints.FreezeAll; //Freezes the player entirely

        //Unlocks the cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; //Make sure it is visible
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(grassCheck.transform.position, touchRadius);
    }
}
