using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    private Animator playerAnim;
    private GameObject playerInterface;
    public GameObject playerInterfaceEscape;

    private void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Canvos").GetComponent<Animator>();
        playerInterface = GameObject.FindGameObjectWithTag("BlOu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Побег");
            playerInterface.SetActive(true);
            playerAnim.SetBool("escape", true);
        }
    }

    public void InterfaceActive()
    {
        playerInterfaceEscape.SetActive(true);
    }
}
