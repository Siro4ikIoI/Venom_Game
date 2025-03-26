using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamer_manedger_timer : MonoBehaviour
{
    // позиция камеры
    public GameObject Player_pos;

    public GameObject[] scream_obj;


    [Header("")]

    public float timer_kd;

    private float t_a = 0f;
    private GameObject spawn_obg;
    void Update()
    {
        t_a += Time.deltaTime;
        if (t_a > timer_kd)
        {
            spawn_obg = Instantiate(scream_obj[Random.Range(0, scream_obj.Length)], Player_pos.transform.TransformPoint(Player_pos.transform.localPosition.x, Player_pos.transform.localPosition.y + 4, Player_pos.transform.localPosition.z + 4.5f), new Quaternion(0,0,0,0));
            spawn_obg.gameObject.SetActive(true);
            t_a = 0f;
        }
    }
}
