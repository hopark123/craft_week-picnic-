using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadManager : MonoBehaviour
{
    [SerializeField]
    private Slider progressBar;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadScenes");
    }

    IEnumerator LoadScenes()
    {
        float timer = 0f;

        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GameModel.sceneController.dest);

        asyncOperation.allowSceneActivation = false;
        timer += Time.deltaTime;

        while (!asyncOperation.isDone)
        {
            yield return null;
            if (asyncOperation.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, asyncOperation.progress, timer);
                if (progressBar.value >= asyncOperation.progress) { timer = 0f; }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1.1f, timer);
                if (progressBar.value > 0.999f)
                {
                    asyncOperation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
