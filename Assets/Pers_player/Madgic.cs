using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madgic : MonoBehaviour
{

    private int now_day = System.DateTime.Now.Day;
    private int now_month = System.DateTime.Now.Month;
    private int now_year = System.DateTime.Now.Year;

    public GameObject gameObject_madgic;
    public int active_day;
    public int active_month;

    void Awake()
    {
        if(active_month == now_month)
            if(now_day < active_day)
                gameObject_madgic.SetActive(false);
        else gameObject_madgic.SetActive(true);
    }


}
