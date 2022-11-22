using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovementStickyBomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameObject enemy;
    private GameObject parent;
    private Rigidbody bombRb;
    private Collider[] colliders;
    private bool enemyStick = false;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Player"); // find player 
        enemy = GameObject.FindGameObjectWithTag("Enemy"); // find enemy
        bombRb = GetComponent<Rigidbody>(); // get rigid body of bomb
        bombRb.AddForce(parent.transform.forward * 8, ForceMode.Impulse); //throw bomb in direction where player is looking
        StartCoroutine(Explode()); 
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyStick==true)
        {
            if(enemy!= null)
            {
                transform.position = enemy.transform.position + new Vector3(0, 1.5f, 0); // sticks the bomb with colliding enemy
            }
           
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bombRb.velocity = Vector3.zero;  //if bomb collides with ground stop it there
        }
      
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemyStick = true; // enable sticky property
        }


    }
    IEnumerator Explode()
    {

        WaitForSeconds explosion = new WaitForSeconds(2);
        yield return explosion;
        Instantiate(explosionPrefab, transform.position, transform.rotation); // plays explode particle system
        colliders = Physics.OverlapSphere(transform.position, 3); //make a sphere of 3 units so any thing in 3 units gets the blast impact
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                    PlayerController.lives--; // if player is in 3 units range reduce 1 life
                    if (PlayerController.lives == 0)
                    {
                        Destroy(hit.gameObject); // if player life is 0 destroy player
                    }
                }
                if (hit.gameObject.CompareTag("Enemy"))
                {
                    PlayerController.score += 25; // if enemy is in 3 unit sphere give player score of 25 and destroy enemy
                    Destroy(hit.gameObject);
                }
            }
        }
        enemyStick = false;
        Destroy(gameObject);

    }
    
}