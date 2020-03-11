using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    Patrol,
    Move
}

public class FiniteStateMachine : MonoBehaviour
{
    private States currentState;

    public NavMeshAgent enemyOne;
    public NavMeshAgent player;
    public Sprite litFlame;
    public GameObject flame;

    private SpriteRenderer myFlameSR;
    private Vector3 enemyOneStart;
    private float patrolA;
    private float patrolB;

    private int direction = 0;

    RaycastHit hitInfo = new RaycastHit();

    // Establish enemy start position and patrol points
    void Start()
    {
        enemyOne = GetComponent<NavMeshAgent>();
        enemyOneStart = enemyOne.transform.position;
        patrolA = enemyOneStart.x;
        patrolB = enemyOneStart.x + 1;
        myFlameSR = flame.GetComponent<SpriteRenderer>();
    }

    // Decision for current state
    void Update()
    {
        if (myFlameSR.sprite == litFlame)      
            currentState = States.Move;       
        else
            currentState = States.Patrol;

        switch (currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Move:
                Move();
                break;
            default:
                Debug.LogError("Invalid State");
                break;
        }
    }

    void Patrol()
    {
        enemyOne.updateRotation = false;

        switch (direction)
        {
            case 0:
                enemyOne.destination = new Vector3(patrolB, enemyOneStart.y, enemyOneStart.z);
                break;
            case 1:
                enemyOne.destination = new Vector3(patrolA, enemyOneStart.y, enemyOneStart.z);
                break;
        }

        if (enemyOne.transform.position.x <= patrolA)
            direction = 0;

        if (enemyOne.transform.position.x >= patrolB)
            direction = 1;
    }

    void Move()
    {
        enemyOne.updateRotation = false;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                enemyOne.destination = player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyOne.Warp(enemyOneStart);
            enemyOne.destination = enemyOneStart;;
        }
    }
}
