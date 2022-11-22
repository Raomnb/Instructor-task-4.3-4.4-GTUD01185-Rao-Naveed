using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            
            PlayerController.score += 25; // if enemy collides with mine increase player score
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            PlayerController.lives--; // if player collides reduce one life
            
            if (PlayerController.lives == 0)
            {
                Destroy(collision.gameObject);
               
            }
            Destroy(gameObject);
        }
        
    }
   
}
