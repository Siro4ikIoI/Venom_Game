using System.Collections;
using UnityEngine;
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
    private bool waitingForClick = false; // ���� �������� �����

    public static Messages instance1 { get; set; }

    private string taskText;

    private void Awake()
    {
        instance1 = this;
    }

    private void Start()
    {
        DialogText.text = string.Empty;
    }

    public void StartDialog(string[] textRU, string task)
    {
        if (cor != null)
            StopCoroutine(cor);

        if (currentDialog != textRU)
        {
            taskText = task;
            currentDialog = textRU;
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
            DialogText.fontSize = 40;
            yield return StartCoroutine(TypeLineRU(currentDialog[currentIndex]));

            // ��� ����� ����� ������� ���������� �����������
            waitingForClick = true;
            yield return new WaitUntil(() => IsScreenTouched());
            waitingForClick = false;
        }

        Tasks.instante.AddTask(taskText);
        StopDialog();
    }

    private IEnumerator TypeLineRU(string line)
    {
        _Dialogs = true;
        foreach (char c in line.ToCharArray())
        {
            DialogText.text += c;
            if (DialogText.text.Length >= 140) DialogText.fontSize = 30;
            yield return new WaitForSeconds(speedtxt);
        }
    }

    private void Update()
    {
        // ���� ��� ����� � ����� ����� ������ ��� ������ �� ������, ���������� ������
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
