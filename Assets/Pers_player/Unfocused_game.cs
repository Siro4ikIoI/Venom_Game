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
                if (audio != null) audio.GetComponent<AudioSource>().Stop();
                Time.timeScale = 0;
            }
        }
        else
        {
            foreach (GameObject audio in osn_audio)
            {
                if (audio != null) audio.GetComponent<AudioSource>().Play();
                Time.timeScale = 1;
            }
        }
    }
}
