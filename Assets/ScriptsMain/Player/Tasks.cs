using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    public static Tasks instante { get; set; }
    public GameObject taskBord;
    public Text mainTaskText;

    private void Start()
    {
        instante = this;
    }

    public void AddTask(string taskText)
    {
        taskBord.SetActive(true);
        mainTaskText.text = taskText;
    }

    public void DelitTask()
    {
        taskBord.SetActive(false);
        mainTaskText.text = "";
    }
}
