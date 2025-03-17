using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenControl : MonoBehaviour
{
    private Animator anim;
    public GameObject blacout;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void SetScen(int scenIndex)
    {
        SceneManager.LoadScene(scenIndex);
    }

    public void StartAnim()
    {
        anim.SetBool("Set", true);
    }

    public void BlacoutFalse()
    {
        blacout.gameObject.SetActive(false);
    }
}
