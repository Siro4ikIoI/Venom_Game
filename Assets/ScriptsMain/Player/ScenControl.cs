using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenControl : MonoBehaviour
{
    private Animator anim;
    public GameObject blacout;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void SetScen(int scenIndex)
    {
        SceneManager.LoadScene(scenIndex);
    }

    //�����
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // ����, ���� ����� ���������� �� 90%
        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log($"��������: {asyncLoad.progress * 100}%");
            yield return null;
        }

    }

    public void StartAnim()
    {
        anim.SetBool("Set", true);
    }

    public void BlacoutFalse()
    {
        blacout.gameObject.SetActive(false);
    }
}
