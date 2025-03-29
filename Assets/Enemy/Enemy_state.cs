using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_state : MonoBehaviour
{
    [SerializeField] private GameObject Player_pos;
    [SerializeField] private GameObject Player_camera_pos;
    [SerializeField] private GameObject Enemy_asset;
    public LayerMask layerMask;

    public AudioSource AudioSource_step;
    public AudioSource AudioSource_osn;
    public AudioClip Triger_atack_clip;
    public AudioClip Enemy_audio;


    private float poteryl = 0f;

    private Animator animator_enemy;
    private NavMeshAgent agent;
    private bool tr_search = true;
    private void Start()
    {
        tr_search = true;
        agent = GetComponent<NavMeshAgent>();
        animator_enemy = Enemy_asset.GetComponent<Animator>();
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Player_pos.transform.position - transform.position).normalized, out hit, 25f, layerMask)) {
            if (hit.transform.tag == "Player") {
                Atack(hit);
                poteryl = 0f;
                tr_search = true;
            }
            else
            {
                poteryl += Time.deltaTime;
                if (poteryl >= 4f)
                {
                    Search();
                }
                else {
                    Atack(hit);
                }
            }
            Debug.DrawRay(transform.position, (Player_pos.transform.position - transform.position).normalized * 25, Color.red);
        }
    }


    private void Atack(RaycastHit h)
    {
        if(h.distance < 1 && h.transform.tag == "Player")
        {
            Player_pos.GetComponent<Player_forward>().Speed_Player = 0;
            Player_camera_pos.transform.LookAt(gameObject.transform.position + new Vector3(0,3,0));
            AudioSource_osn.PlayOneShot(Triger_atack_clip);
            AudioSource_step.Pause();
            animator_enemy.SetTrigger("Atack_en");
        }
        else
        agent.SetDestination(Player_pos.transform.position);
    }

    private void Search()
    {
        if (tr_search)
        {
            NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
            agent.SetDestination(navMeshTriangulation.vertices[Random.Range(0, navMeshTriangulation.vertices.Length)]);
            AudioSource_step.PlayOneShot(Enemy_audio);
            tr_search = false;
        }
        else if (agent.remainingDistance <= 3 && !agent.pathPending) { 
            tr_search = true;
        }

    }
}
