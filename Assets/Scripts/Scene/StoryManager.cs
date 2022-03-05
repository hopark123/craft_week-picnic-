using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public string nextScene;
    public GameObject timelineKey;
    public GameObject skipMessage;
    private bool mouseClick;

    void Start()
    {
        mouseClick = false;
        timelineKey.SetActive(false);
        skipMessage.SetActive(false);
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClick = true;
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);

        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            yield return null;
            if (asyncOperation.progress >= 0.9f)
            {
                if (!skipMessage.activeInHierarchy)
                    skipMessage.SetActive(true);
                if (mouseClick || timelineKey.activeInHierarchy)
                    asyncOperation.allowSceneActivation = true;
            }
        }
        Debug.Log("done");
    }
}
