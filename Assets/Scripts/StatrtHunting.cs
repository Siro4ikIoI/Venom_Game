using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartHunting : MonoBehaviour
{
    [Header("Настройки таймера")]
    public float timeRemaining = 60f;
    public Text timerText;

    [Header("Игровые объекты")]
    public GameObject[] timerObjects;
    public GameObject audioBlackout;
    public GameObject audioBlackoutTheme;
    public GameObject audioMain;
    public GameObject audioSiren;

    private GameObject venomOpen;
    public bool isCounting = false;

    public static StartHunting Instance { get; set; }

    private void Start()
    {
        Instance = this;
        // Поиск объекта "Laser" внутри "Map_Venom(Clone)"
        GameObject venomObject = GameObject.Find("Map_Venom(Clone)");
        if (venomObject != null)
        {
            Transform laserTransform = venomObject.transform.Find("Laser");
            if (laserTransform != null)
            {
                venomOpen = laserTransform.gameObject;
            }
        }
    }

    private void Update()
    {
        TryStartCountdown();
    }

    private IEnumerator StartCountdown()
    {
        isCounting = true;

        while (timeRemaining > 0)
        {
            timeRemaining--;
            UpdateTimerText();
            yield return new WaitForSeconds(1f);
        }

        TimerEnded();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"00:{timeRemaining:00}";
        }
    }

    private void TimerEnded()
    {
        Debug.Log("Таймер истек! Происходит событие...");

        if (venomOpen != null)
        {
            venomOpen.SetActive(false);
        }

        foreach (GameObject obj in timerObjects)
        {
            obj.SetActive(false);
        }

        PlayBlackoutEffects();
    }

    private void PlayBlackoutEffects()
    {
        if (audioBlackout != null) audioBlackout.GetComponent<AudioSource>()?.Play();
        if (audioBlackoutTheme != null) audioBlackoutTheme.GetComponent<AudioSource>()?.Play();
        if (audioSiren != null) audioSiren.GetComponent<AudioSource>()?.Stop();

        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogDensity = 0.04f;
    }

    public void TryStartCountdown()
    {
        if (isCounting)
        {
            foreach (GameObject obj in timerObjects)
            {
                obj.SetActive(true);
            }

            timerObjects[0].SetActive(true);
            StartCoroutine(StartCountdown());

            if (audioMain != null) audioMain.GetComponent<AudioSource>()?.Stop();
            if (audioSiren != null) audioSiren.GetComponent<AudioSource>()?.Play();

            isCounting = false;
        }
    }
}
