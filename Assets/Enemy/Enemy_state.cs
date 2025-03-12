using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_state : MonoBehaviour
{
    [SerializeField] private GameObject Player_pos;

    private float poteryl = 0f;


    private NavMeshAgent agent;
    private bool tr_search = true;
    private void Start()
    {
        tr_search = true;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Player_pos.transform.position - transform.position).normalized, out hit, 25f)) {
            if (hit.transform.tag == "Player") {
                Atack();
                poteryl = 0f;
                tr_search = true;
            }
            else
            {
                poteryl += Time.deltaTime;
                if (poteryl >= 2f)
                {
                    Search();
                }
                else {
                    Atack();
                }
            }
            Debug.DrawRay(transform.position, (Player_pos.transform.position - transform.position).normalized * 25, Color.red);
        }
    }


    private void Atack()
    {
        agent.SetDestination(Player_pos.transform.position);
    }

    private void Search()
    {
        if (tr_search)
        {
            NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
            agent.SetDestination(navMeshTriangulation.vertices[Random.Range(0, navMeshTriangulation.vertices.Length)]);
            tr_search = false;
        }
        else if (agent.remainingDistance <= 3 && !agent.pathPending) { 
            tr_search = true;
        }

    }
}
