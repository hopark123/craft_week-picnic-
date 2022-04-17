using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SceneManager
{
    public string dest = null;

    public void CallLoadScene()
    {
        LoadScene("LoadingScene");
    }
}
