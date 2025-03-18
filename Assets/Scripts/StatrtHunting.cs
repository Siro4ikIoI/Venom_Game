using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI; // Для отображения таймера в UI

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public Text timerText;
    public bool _error = false;
    public GameObject[] timer;
    public GameObject audioBlackout;
    public GameObject audioBlackoutTheam;
    public GameObject audioMian;
    public GameObject audioSiren;
    private GameObject venomOpen;

    private void Start()
    {
        GameObject venomObject = GameObject.Find("Map_Venom(Clone)");
        venomOpen = venomObject.transform.Find("Laser")?.gameObject;
    }

    private void Update()
    {
        Error();
    }

    private IEnumerator StartCountdown()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= 1;
            if (timerText != null && timeRemaining > 9)
                timerText.text = "00:" + timeRemaining.ToString("0");
            if (timerText != null && timeRemaining <= 9)
                timerText.text = "00:0" + timeRemaining.ToString("0");
            yield return new WaitForSeconds(1f);
        }

        TimerEnded();
    }

    private void TimerEnded()
    {
        Debug.Log("Таймер истек! Происходит событие...");
        venomOpen.SetActive(false);
        foreach(GameObject objs in timer) objs.SetActive(false);
        audioBlackout.GetComponent<AudioSource>().Play();
        audioBlackoutTheam.GetComponent<AudioSource>().Play();
        audioSiren.GetComponent<AudioSource>().Stop();
    }

    public void Error()
    {
        if (_error)
        {
            foreach (GameObject objs in timer) objs.SetActive(true);
            StartCoroutine(StartCountdown());
            audioMian.GetComponent<AudioSource>().Stop();
            audioSiren.GetComponent<AudioSource>().Play();
            _error = false;
        } 
    }
}
