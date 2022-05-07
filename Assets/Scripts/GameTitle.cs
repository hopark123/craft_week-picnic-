using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitle : MonoBehaviour
{
    public void GameStart()
    {
        if (!GameModel.FirstStart)
        {
            GameModel.Load("GameScene" + (GameModel.StageNumber + 1));
        }
        else
        {
            GameModel.FirstStart = false;
            GameModel.Load("OpeningScene");
        }
    }
}
