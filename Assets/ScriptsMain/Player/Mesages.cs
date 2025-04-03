using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
    public float speedtxt = 0.1f;
    public Text DialogText;
    [SerializeField] private GameObject messagesPanel;

    private int currentIndex = 0;
    private Coroutine cor;
    private string[] currentDialog;

    public bool _Dialogs = false;
    private bool waitingForClick = false; // Флаг ожидания клика

    public static Messages instance1 { get; set; }

    private string taskText;
    private string tagObj = "---";
    private GameObject[] dependentTriggers;

    private void Awake()
    {
        instance1 = this;
    }

    private void Start()
    {
        DialogText.text = string.Empty;
    }

    public void StartDialog(string[] textRU, string task, string tag, GameObject[] triggers)
    {
        if (cor != null)
            StopCoroutine(cor);

        if (currentDialog != textRU)
        {
            taskText = task;
            currentDialog = textRU;
            if (tagObj != null) tagObj = tag;
            dependentTriggers = triggers;
            currentIndex = 0;
        }

        messagesPanel.SetActive(true);
        cor = StartCoroutine(ShowDialog());
    }

    public void StopDialog()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }

        currentIndex = 0;
        currentDialog = null;
        DialogText.text = string.Empty;
        DialogText.fontSize = 40;
        messagesPanel.SetActive(false);
        _Dialogs = false;
        waitingForClick = false;
    }

    private IEnumerator ShowDialog()
    {
        for (; currentIndex < currentDialog.Length; currentIndex++)
        {
            DialogText.text = "";
            yield return StartCoroutine(TypeLineRU(currentDialog[currentIndex]));

            // Ждём клика перед показом следующего предложения
            waitingForClick = true;
            yield return new WaitUntil(() => IsScreenTouched());
            waitingForClick = false;
        }

        if (taskText != "") Tasks.instante.AddTask(taskText);
        if (tagObj != null) ArrowPointer.instante.SetObj(tagObj);
        StopDialog();
        if (dependentTriggers != null)
        {
            for (int i = 0; i < dependentTriggers.Length; i++) dependentTriggers[i].SetActive(true);
        }
    }

    private IEnumerator TypeLineRU(string line)
    {
        _Dialogs = true;
        foreach (char c in line.ToCharArray())
        {
            DialogText.text += c;
            yield return new WaitForSeconds(speedtxt);
        }
    }

    private void Update()
    {
        // Если ждём клика и игрок нажал кнопку или тапнул по экрану, продолжаем диалог
        if (waitingForClick && IsScreenTouched())
        {
            waitingForClick = false;
        }
    }

    private bool IsScreenTouched()
    {
        return Input.GetMouseButtonDown(0) || Input.touchCount > 0;
    }
}
