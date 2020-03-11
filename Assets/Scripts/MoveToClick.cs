using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Controls player as well as Enemy (1) since both move towards mouse click
public class MoveToClick : MonoBehaviour
{
    public NavMeshAgent player;
    private Vector3 startPos;

    RaycastHit hitInfo = new RaycastHit();

    // Save character starting position
    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        startPos = player.transform.position;
    }

    // Click to move
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

    // Resets character position to start on collision with enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            player.Warp(startPos);
            player.destination = startPos;
        }
    }
}


