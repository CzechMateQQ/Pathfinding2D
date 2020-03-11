using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Only controls one of the enemies as the other utilizes player controller script
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent enemy;

    [SerializeField]
    private NavMeshAgent enemyTwo;

    [SerializeField]
    private NavMeshAgent player;

    private Vector3 enemyStart;
    private Vector3 enemyTwoStart;

    RaycastHit hitInfo = new RaycastHit();

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemyStart = enemy.transform.position;

        enemyTwo = GetComponent<NavMeshAgent>();
        enemyTwoStart = enemyTwo.transform.position;
    }

    void Update()
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

            enemyTwo.Warp(enemyTwoStart);
            enemyTwo.destination = enemyTwoStart;
        }
    }
}
