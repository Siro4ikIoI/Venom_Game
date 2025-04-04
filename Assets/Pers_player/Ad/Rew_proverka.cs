using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Rew_proverka : MonoBehaviour
{
    public static int id_rew = 0;
    public GameObject restart_panel;

    public void Proverka_ad()
    {
        if (id_rew == 1)
        {
            Time.timeScale = 1;
        }
        id_rew = 0;
    }

    // Меню возрождения

    public void no_rew()
    {
        restart_panel.SetActive(false);
        //Что сделать при рестрате
    }

    public void rew_restart()
    {
        id_rew = 1;
        YandexGame.RewVideoShow(id_rew);
        restart_panel.SetActive(false);
    }
}
