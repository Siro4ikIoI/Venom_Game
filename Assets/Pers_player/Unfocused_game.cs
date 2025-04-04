using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unfocused_game : MonoBehaviour
{
    public GameObject[] osn_audio;
    void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            foreach (GameObject audio in osn_audio)
            {
                audio.SetActive(false);
                Time.timeScale = 0;
            }
        }
        else
        {
            foreach (GameObject audio in osn_audio)
            {
                audio.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }
}
