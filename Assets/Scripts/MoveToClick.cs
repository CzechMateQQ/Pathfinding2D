using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Controls player as well as Enemy (1) since both move towards mouse click
public class MoveToClick : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent player;

    private Vector3 startPos;
    private Vector3 enemyStartPos;

    RaycastHit hitInfo = new RaycastHit();

    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        startPos = player.transform.position;
    }

    void Update()
    {
        player.updateRotation = false;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                player.destination = hitInfo.point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            player.Warp(startPos);
            player.destination = startPos;
        }
    }
}


