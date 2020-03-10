using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    Wait,
    Move
}

public class FiniteStateMachine : MonoBehaviour
{
    private States currentState;

    [SerializeField]
    private NavMeshAgent enemy;

    RaycastHit hitInfo = new RaycastHit();

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case States.Wait:
                Wait();
                break;
            case States.Move:
                Move();
                break;
            default:
                Debug.LogError("Invalid State");
                break;
        }
    }

    void Wait()
    {

    }

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                enemy.destination = hitInfo.point;
        }
    }
}
