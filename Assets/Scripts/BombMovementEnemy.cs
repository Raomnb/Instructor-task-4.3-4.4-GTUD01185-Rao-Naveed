using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovementEnemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameObject parent;
    private Rigidbody bombRb;
    private Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Player"); // find player
        bombRb = GetComponent<Rigidbody>(); // get rigid body of bomb
        bombRb.AddForce(parent.transform.position-transform.position*2, ForceMode.Impulse);//throw bomb in direction where player is looking
        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bombRb.velocity = Vector3.zero;
        }


    }
    IEnumerator Explode()
    {
        WaitForSeconds explosion = new WaitForSeconds(2);
        yield return explosion;
        Instantiate(explosionPrefab, transform.position, transform.rotation);// plays explode particle system
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
                    Destroy(hit.gameObject); // if enemy is in 3 units sphere range destroy it
                }
            }
        }
        Destroy(gameObject);

    }
    
}
