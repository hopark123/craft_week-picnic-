using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private GameObject trigger;
    [SerializeField]
    private GameObject[] cutScenes;
    [SerializeField] 
    private GameObject skipMessage;
    private bool skip;
    public AudioClip[] Clip;

    void Start()
    {
        skipMessage.SetActive(false);
        skip = false;
        for (int i = 0; i < cutScenes.Length; i++)
        {
            cutScenes[i].SetActive(false);
        }
        StartCoroutine(CutScene());
        StartCoroutine(LoadScene());
    }

    public void SkipButton()
    {
        skip = true;
    }

    IEnumerator CutScene()
    {
        int i = 0;
        do
        {
            SoundController.instance.SFXPlay("clip" + i, Clip[i]);
            cutScenes[i].SetActive(true);
            if(i != 2 && i != 3)
                yield return new WaitForSecondsRealtime(2.8f);
            if (i == 2)
                yield return new WaitForSecondsRealtime(4.0f);
            if (i == 3)
                yield return new WaitForSecondsRealtime(1.8f);
            i++;
        }
        while (i < cutScenes.Length);
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
