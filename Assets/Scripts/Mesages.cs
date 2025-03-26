using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mesages : MonoBehaviour
{
    public float speedtxt = 0.1f;   // Скорость появления символов
    public float delayBetweenLines = 1.5f; // Задержка перед новой строкой

    public Text Diologtext;
    [SerializeField] public GameObject mesagesPanel;

    private int currentIndex = 0; // Текущий индекс строки диалога
    private Coroutine cor;
    private string[] currentDialog; // Храним текущий диалог

    public bool _Dialogs = false;

    public static Mesages instance1 { get; private set; }

    private void Awake()
    {
        instance1 = this;
    }

    private void Start()
    {
        Diologtext.text = string.Empty;
    }

    public void StartDiolog(string[] textRU)
    {
        if (cor != null)
            StopCoroutine(cor);

        if (currentDialog != textRU) // Если это новый диалог — начинаем с нуля
        {
            currentDialog = textRU;
            currentIndex = 0;
        }

        mesagesPanel.SetActive(true);
        cor = StartCoroutine(ShowDialog());
    }

    public void StopDiolog()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }

        currentIndex = 0; // Сброс индекса при завершении диалога
        currentDialog = null; // Обнуляем текущий диалог
        Diologtext.text = string.Empty;
        Diologtext.fontSize = 40;
        mesagesPanel.SetActive(false);
        _Dialogs = false;
    }

    private IEnumerator ShowDialog()
    {
        for (; currentIndex < currentDialog.Length; currentIndex++)
        {
            Diologtext.text = "";
            Diologtext.fontSize = 40;
            yield return StartCoroutine(TipLineRU(currentDialog[currentIndex])); // Вызываем корутину для печати строки
            yield return new WaitForSeconds(delayBetweenLines); // Задержка перед следующей фразой
        }

        StopDiolog(); // Отключаем диалог после последней строки
    }

    private IEnumerator TipLineRU(string line)
    {
        _Dialogs = true;
        foreach (char c in line.ToCharArray())
        {
            Diologtext.text += c;
            if (Diologtext.text.Length >= 140) Diologtext.fontSize = 30;
            yield return new WaitForSeconds(speedtxt);
        }
    }
}
