using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private GameObject skipMessage;
    [SerializeField]
    private GameObject trigger;
    private bool skip;

    void Start()
    {
        skip = false;
        skipMessage.SetActive(false);
        StartCoroutine(LoadScene());
    }

    public void SkipButton()
    {
        skip = true;
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
                if (skip || trigger.activeInHierarchy)
                    asyncOperation.allowSceneActivation = true;
            }
        }
        Debug.Log("done");
    }
}
