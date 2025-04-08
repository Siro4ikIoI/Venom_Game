using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Rew_proverka : MonoBehaviour
{
    public static int id_rew = 0;
    public GameObject restart_panel;
    public GameObject black_panel;
    public GameObject Player_reset_pos;
    public GameObject venom_pos;
    public Animator playerAnim;
    public void Proverka_ad()
    {
        if (id_rew == 1)
        {
            DontDestroyOnLoad(Player_reset_pos);
            Time.timeScale = 1;
        }
        id_rew = 0;
    }

    // Меню возрождения

    public void no_rew()
    {
        restart_panel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        //Что сделать при рестрате
    }

    public void rew_restart()
    {
        id_rew = 1;
        YandexGame.RewVideoShow(id_rew);
        Cursor.lockState = CursorLockMode.Locked;
        restart_panel.SetActive(false);
        black_panel.SetActive(false);
        Player_reset_pos.GetComponent<Player_forward>().standart_speed = 5; // как в инспекторе
        venom_pos.transform.position = new Vector3(13.51012f, -16.9516f, -2.08f);
        playerAnim.SetBool("death", false);
    }
}
