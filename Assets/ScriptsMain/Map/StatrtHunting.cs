using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
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
    public bool isCountingMeneger = true;

    public GameObject[] blackoutDependentTriggers;

    public static StartHunting Instance { get; set; }

    public NavMeshSurface surface;
    public GameObject Player_pos;
    public GameObject Venom_pos;
    public GameObject screamer_m;

    private void Start()
    {
        surface.UpdateNavMesh(surface.navMeshData);
        //StartCoroutine(UpdateNavMeshs());//кор
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

        if (audioBlackout != null && audioBlackout.GetComponent<AudioSource>()?.isPlaying == !true) audioBlackout.SetActive(false);
    }

    //private IEnumerator UpdateNavMeshs()
    //{
    //    yield return new WaitForSeconds(4f);
    //    AsyncOperation asyncOp = surface.UpdateNavMesh(surface.navMeshData);
    //
    //    while (!asyncOp.isDone)
    //    {
    //        Debug.Log($"Прогресс генерации NavMesh: {asyncOp.progress * 100}%");
    //        yield return null; // Ожидаем завершения без зависания
    //    }
    //
    //    Debug.Log("NavMesh построен!");
    //}
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
        Venom_pos.SetActive(true);
        //Venom_pos.transform.position = Player_pos.transform.TransformPoint(Player_pos.transform.localPosition.x, Player_pos.transform.localPosition.y, Player_pos.transform.localPosition.z + 4.5f);
        screamer_m.SetActive(true);


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
        if (audioBlackout != null) audioBlackout.SetActive(true);
        if (audioBlackout != null) audioBlackout.GetComponent<AudioSource>()?.Play();

        if (audioBlackoutTheme != null) audioBlackoutTheme.SetActive(true);
        if (audioBlackoutTheme != null) audioBlackoutTheme.GetComponent<AudioSource>()?.Play();

        if (audioSiren != null) audioSiren.SetActive(false);

        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogDensity = 0.04f;

        isCountingMeneger = false;
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

            if (audioMain != null) audioMain.SetActive(false);
            if (audioSiren != null) audioSiren.SetActive(true);
            if (audioSiren != null) audioSiren.GetComponent<AudioSource>()?.Play();

            GameObject[] allObjects = FindObjectsOfType<GameObject>(); // Получаем все объекты в сцене

            foreach (GameObject obj in allObjects)
            {
                foreach (Transform child in obj.transform) // Проверяем детей первого уровня
                {
                    if (child.CompareTag("B_triger")) // Если тег совпадает
                    {
                        child.gameObject.SetActive(true); // Активируем объект
                    }
                }
            }

            isCounting = false;
        }
    }
}
