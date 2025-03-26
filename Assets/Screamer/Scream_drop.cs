using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream_drop : MonoBehaviour
{
    public bool no_fizik;


    [Header("Ёффекты")]

    public AudioClip audioClip;
    public AudioSource audioSource;


    private bool tr_one_col = true;
    private float t_a = 0;

    void Update()
    {
        if (!tr_one_col) t_a += Time.deltaTime;
        if (t_a > 7) Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (no_fizik)
        {
            audioSource.PlayOneShot(audioClip);
            tr_one_col = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (tr_one_col)
        {
            audioSource.PlayOneShot(audioClip);
            tr_one_col = false;
        }
    }
}
