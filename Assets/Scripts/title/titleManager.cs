using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleManager : MonoBehaviour
{
    public void GameStart()
    {
        if (GameManager.openingShow)
            GameManager.Load("GameScene " + (GameManager.stageNumber + 1));
        else
        {
            GameManager.openingShow = true;
            GameManager.Load("OpeningScene");
        }
    }
}
