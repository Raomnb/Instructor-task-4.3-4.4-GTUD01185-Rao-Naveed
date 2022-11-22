using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject bombPrefab;
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator enemyAnim;
    private Collider[] colliders;
    int bombcount = 0;
    WaitForSeconds wait = new WaitForSeconds(5);

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // selecting navmesh agent
        StartCoroutine(ChangeDestination()); // setting destination to move
        player = GameObject.Find("Player"); 
        enemyAnim = GetComponent<Animator>();
        enemyAnim.SetFloat("Speed_f", 0.35f);

    }

    // Update is called once per frame
    void Update()
    {

        if (navMeshAgent.destination==transform.position)
        {
            StartCoroutine(ChangeDestination()); // set new destination on reaching the destination
        }
        colliders = Physics.OverlapSphere(transform.position, 5); //make a sphere of 5 units radius
        foreach(Collider hit in colliders)
        {
            Rigidbody playerRb = hit.GetComponent<Rigidbody>();
            if(playerRb !=null)
            {
                if(hit.gameObject.CompareTag("Player"))
                {
                    if (bombcount < 1)
                    {
                        Instantiate(bombPrefab, transform.position + new Vector3(0, 1.8f, 1f), bombPrefab.transform.rotation); // if player is in 5 unit radius throw bomb towards player
                        bombcount++;
                        StartCoroutine(WaitBomb());
                        StartCoroutine(ChangeDestination());
                        break;
                    }
                }
            }
        }
      
    }
    IEnumerator WaitBomb()
    {
        yield return wait;
        bombcount = 0;
    }
    IEnumerator ChangeDestination()
    {
        int randomSelect = Random.Range(0, 6); // to select random destination of enemy
        if(randomSelect==0)
        {
            if(player!=null)
            {
                navMeshAgent.destination = player.transform.position;
                
            }
            
        }
        else if(randomSelect ==1)
        {
            navMeshAgent.destination = new Vector3(0, 0, 0);
        }
        else if(randomSelect == 2)
        {
            navMeshAgent.destination = new Vector3(8, 0, 6);
        }
        else if(randomSelect ==3)
        {
            navMeshAgent.destination = new Vector3(-8, 0, 6);
        }
        else if(randomSelect == 4)
        {
            navMeshAgent.destination = new Vector3(8, 0, -6);
        }
        else if(randomSelect == 5)
        {
            navMeshAgent.destination = new Vector3(-8, 0, -6);
        }
        yield return wait;
    }
}
