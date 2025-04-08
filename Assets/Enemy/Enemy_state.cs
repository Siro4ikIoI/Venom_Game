using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    public bool trr;
    private NavMeshPath path;

    public Animator playerAnim;
    public GameObject deathInterface;

    private void Start()
    {
        tr_search = true;
        agent = GetComponent<NavMeshAgent>();
        animator_enemy = Enemy_asset.GetComponent<Animator>();
        path = new NavMeshPath();
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
        print(tr_search);
        print(agent.remainingDistance);
        if (trr) {
            agent.SetDestination(Vector3.zero);
        }
        //Избыточно, но фикс
        if (agent.remainingDistance <= 3 && !agent.pathPending && !tr_search)
        {
            tr_search = true;
            Search();
            print("Дошел");
        }
    }


    private void Atack(RaycastHit h)
    {
        if(h.distance < 1 && h.transform.tag == "Player")
        {
            Player_pos.GetComponent<Player_forward>().standart_speed = 0;
            Player_camera_pos.transform.LookAt(gameObject.transform.position + new Vector3(0,3,0));
            AudioSource_osn.PlayOneShot(Triger_atack_clip);
            AudioSource_step.Pause();
            animator_enemy.SetTrigger("Atack_en");
            deathInterface.SetActive(true);
            playerAnim.SetBool("death", true);
            UnityEngine.Cursor.lockState = CursorLockMode.None; // дата-visibl
        }
        else
        agent.SetDestination(Player_pos.transform.position);
    }

    private void Search()
    {

        if (tr_search)
        {
            int vert_i = 0;
            NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
            vert_i = Random.Range(0, navMeshTriangulation.vertices.Length);

            AudioSource_step.PlayOneShot(Enemy_audio);

            Proverka_Search(navMeshTriangulation.vertices[vert_i]);


        }
        else if (agent.remainingDistance <= 3 && !agent.pathPending) { 
            tr_search = true;
            print("Дошел");
        }

    }

    private void Proverka_Search(Vector3 transform_nav)
    {
        // Проверяем путь
        NavMesh.CalculatePath(transform.position, transform_nav, NavMesh.AllAreas, path);
        print(path.status);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Путь найден! Можно дойти.");
            tr_search = false;
            agent.SetDestination(transform_nav);
        }
        else if (path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
        {
            Debug.Log("Путь частичный! Возможно, нужен прыжок или лестница.");

            Search();
        }

    }

    private void OnEnable()
    {
        AudioSource_step.PlayOneShot(Enemy_audio);
    }


}
