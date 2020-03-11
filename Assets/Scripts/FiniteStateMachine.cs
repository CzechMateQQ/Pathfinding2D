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

    public NavMeshAgent enemy;
    public NavMeshAgent player;
    public Sprite litFlame;
    public GameObject flame;

    private SpriteRenderer myFlameSR;
    private Vector3 enemyStart;
    private float patrolA;
    private float patrolB;

    RaycastHit hitInfo = new RaycastHit();

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemyStart = enemy.transform.position;
        patrolA = enemyStart.x;
        patrolB = enemyStart.x + 1;
        myFlameSR = flame.GetComponent<SpriteRenderer>();
    }

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

    private int direction = 0;

    void Patrol()
    {
        enemy.updateRotation = false;

        switch (direction)
        {
            case 0:
                enemy.destination = new Vector3(patrolB, enemyStart.y, enemyStart.z);
                break;
            case 1:
                enemy.destination = new Vector3(patrolA, enemyStart.y, enemyStart.z);
                break;
        }

        if (enemy.transform.position.x <= patrolA)
            direction = 0;

        if (enemy.transform.position.x >= patrolB)
            direction = 1;
    }

    void Move()
    {
        enemy.updateRotation = false;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                enemy.destination = player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.Warp(enemyStart);
            enemy.destination = enemyStart;
        }
    }
}
