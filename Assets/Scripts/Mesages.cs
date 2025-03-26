using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mesages : MonoBehaviour
{
    public float speedtxt = 0.1f;   // �������� ��������� ��������
    public float delayBetweenLines = 1.5f; // �������� ����� ����� �������

    public Text Diologtext;
    [SerializeField] public GameObject mesagesPanel;

    private int currentIndex = 0; // ������� ������ ������ �������
    private Coroutine cor;
    private string[] currentDialog; // ������ ������� ������

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

        if (currentDialog != textRU) // ���� ��� ����� ������ � �������� � ����
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

        currentIndex = 0; // ����� ������� ��� ���������� �������
        currentDialog = null; // �������� ������� ������
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
            yield return StartCoroutine(TipLineRU(currentDialog[currentIndex])); // �������� �������� ��� ������ ������
            yield return new WaitForSeconds(delayBetweenLines); // �������� ����� ��������� ������
        }

        StopDiolog(); // ��������� ������ ����� ��������� ������
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
