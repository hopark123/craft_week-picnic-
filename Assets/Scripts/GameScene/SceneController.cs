using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SceneManager
{
    [field: SerializeField]
    public bool goNextScene { get; set; } = false;

    public IEnumerator LoadScenes(string nextScene)
    {
        yield return false;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);

        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            yield return false;
            if (asyncOperation.progress >= 0.9f)
            {
                if (goNextScene)
                {
                    asyncOperation.allowSceneActivation = true;
                    goNextScene = false;
                }
                yield return (true);
            }
        }
    }
}
