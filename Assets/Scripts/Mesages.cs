using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Diologi : MonoBehaviour
{
    public string[] textRU;
    public string[] textEN;
    public float speedtxt = 0.1f;   // Скорость появления символов
    public float delayBetweenLines = 1.5f; // Задержка перед новой строкой

    public Text Diologtext;
    [SerializeField] public GameObject Text;
    [SerializeField] private GameObject Panl;

    private int texnam1 = 0;
    private Coroutine cor;

    public static Diologi instance1 { get; private set; }

    private void Awake()
    {
        instance1 = this;
    }

    private void Start()
    {
        Diologtext.text = string.Empty;
        StartDiolog();
    }

    public void StartDiolog()
    {
        if (cor != null)
            StopCoroutine(cor);

        Text.SetActive(true);
        Panl.SetActive(true);

        cor = StartCoroutine(ShowDialog());
    }

    public void StopDiolog()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }

        Diologtext.text = string.Empty;
        Text.SetActive(false);
        Panl.SetActive(false);
    }

    private IEnumerator ShowDialog()
    {
        for (int i = texnam1; i < textRU.Length; i++)
        {
            Diologtext.text = "";
            yield return StartCoroutine(TipLineRU(textRU[i])); // Вызываем корутину для печати строки
            yield return new WaitForSeconds(delayBetweenLines); // Задержка перед следующей фразой
        }

        StopDiolog(); // Отключаем диалог после последней строки
    }

    private IEnumerator TipLineRU(string line)
    {
        foreach (char c in line.ToCharArray())
        {
            Diologtext.text += c;
            yield return new WaitForSeconds(speedtxt);
        }
    }
}
