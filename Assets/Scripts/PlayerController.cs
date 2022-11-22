using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public GameObject[] bombPrefab;
    public static int lives = 5;
    public static int score = 0;
    public static bool mines = false;
    public static bool sticky = false;
    public static bool multi = false;
    public static bool nopower = true;
    private int bombcount = 0;
    private float timer = 0;
    private int stickyCount = 0;

 
  

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // get rigid body of player
        playerAnim = GetComponent<Animator>(); // get animator of player
    }

    // Update is called once per frame
    void Update()
    {
        bombcount = FindObjectsOfType<BombMovement>().Length; // get number of bombs in scene
        if(multi ==  true)
        {
            timer += Time.deltaTime;
        }
        float verticalInput = Input.GetAxis("Vertical"); // get up down keys as vertical input
        float horizontalInput = Input.GetAxis("Horizontal"); // get left right keys as horizontal input
        if(verticalInput != 0)
        {
            transform.Translate(Vector3.forward * 2 * verticalInput * Time.deltaTime); //move player forward backwards
            playerAnim.SetFloat("Speed_f", 0.45f); // set walking animation
        } 
        else
        {
            playerAnim.SetFloat("Speed_f", 0f); // set idle animation
        }
        transform.Rotate(Vector3.up * 50 * horizontalInput * Time.deltaTime); // rotate player using left right keys
        if (Input.GetKeyDown(KeyCode.Space) && nopower == true && bombcount == 0)
        {
            Instantiate(bombPrefab[0], transform.position + new Vector3(0, 1.8f, 0.5f), bombPrefab[0].transform.rotation); // throw normal bomb if no normal bomb is in scene
        }
        else if (Input.GetKeyDown(KeyCode.Space) && multi == true)
        {
            Instantiate(bombPrefab[0], transform.position + new Vector3(0, 1.8f, 0.5f), bombPrefab[0].transform.rotation); // throws multiple bombs 
            if (timer > 10) 
            {
                multi = false; // if 10 seconds are passed stop throwing multi bombs
                timer = 0;
                nopower = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && sticky == true)
        {
            Instantiate(bombPrefab[1], transform.position + new Vector3(0, 1.8f, 0.5f), bombPrefab[1].transform.rotation); // if sticky power is enabled throw sticky bombs
            stickyCount++;
            if (stickyCount > 3)
            {
                sticky = false;
                stickyCount = 0;
                nopower = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && mines == true)
        {
            Instantiate(bombPrefab[2], transform.position + new Vector3(0, 0.2f, -2.5f), bombPrefab[2].transform.rotation); // if mines power is true throw mine
            mines = false;
            nopower = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Lives"))
        {
            lives++; // if lives poweruop picked add 1 life
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Mines"))
        {
            mines = true; // if mines powerup picked enable mines powerup
            nopower = false;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Sticky"))
        {
            sticky = true; // if sticky powerup is picked enable sticky bombs
            nopower = false;
            Destroy(other.gameObject);
        }
        else if( other.gameObject.CompareTag("Multi"))
        {
            multi = true; // if multi powerup is picked enable multiple bombs
            nopower = false;
            Destroy(other.gameObject);
        }
        else
        {
            nopower = true;
        }
    }
    
    

}
